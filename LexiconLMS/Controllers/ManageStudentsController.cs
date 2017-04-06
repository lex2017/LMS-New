using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LexiconLMS.Models;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.IO;
using System.Net.Mime;

namespace LexiconLMS.Controllers
{
    public class ManageStudentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Students
        public ActionResult Index(string searchString)
        {
            //var student = from m in db.Users
            //              select m;
            var role = db.Roles.SingleOrDefault(m => m.Name == "Student").Id;
            var students = db.Users.Where(u => u.Roles.Any(r => r.RoleId == role));

            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.FirstName.Contains(searchString)
                                              || s.LastName.Contains(searchString) || s.UserName.Contains(searchString));

                return View(students.ToList());
            }

            return View(students.ToList());
        }

        public ActionResult DocumentFilter(int? courseid, int? modulid, int? activityid)
        {
            if (courseid != null && modulid == null && activityid == null)
            {
                IQueryable<Document> document = db.Documents.Where(x => x.CourseId == courseid);
                ViewBag.courseid = courseid;
                ViewBag.coursename = db.Courses.Where(v => v.CourseID == courseid).Select(x => x.Name).SingleOrDefault().ToString();
                TempData["courseid"] = courseid;
                return View("Index", document.ToList());

            }
            else if (courseid != null && modulid != null && activityid == null)
            {
                IQueryable<Document> document = db.Documents.Where(z => z.CourseId == courseid && z.ModuleId == modulid);
                ViewBag.courseid = courseid;
                ViewBag.modulid = modulid;
                ViewBag.coursename = db.Courses.Where(v => v.CourseID == courseid).Select(x => x.Name).SingleOrDefault().ToString();
                ViewBag.modulname = db.Modules.Where(v => v.ModuleID == modulid).Select(x => x.Name).SingleOrDefault().ToString();
                TempData["courseid"] = courseid;
                TempData["modulid"] = modulid;
                return View("Index", document.ToList());
            }
            else
            {
                IQueryable<Document> document = db.Documents.Where(z => z.CourseId == courseid && z.ModuleId == modulid || z.ActivityId == activityid);
                ViewBag.courseid = courseid;
                ViewBag.modulid = modulid;
                ViewBag.activityid = activityid;
                ViewBag.activity = activityid; // For show in view
                ViewBag.coursename = db.Courses.Where(v => v.CourseID == courseid).Select(x => x.Name).SingleOrDefault().ToString();
                ViewBag.modulname = db.Modules.Where(v => v.ModuleID == modulid).Select(x => x.Name).SingleOrDefault().ToString();
                ViewBag.activityname = db.Activities.Where(v => v.ActivityId == activityid).Select(x => x.Name).SingleOrDefault().ToString();

                TempData["courseid"] = courseid;
                TempData["modulid"] = modulid;
                TempData["activityid"] = activityid;

                return View("Index", document.ToList());

            }
        }

        [HttpPost]
        public ActionResult Upload([Bind(Include = "DocumentId")] Document document, HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    document.UserId = User.Identity.GetUserId();
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Files"), fileName);
                    file.SaveAs(path);
                    document.DeadlineDate = DateTime.Now;
                    document.TimeStamp = DateTime.Now;
                    //document.FileName = document.DocumentId + " " + fileName;
                    document.FileName = fileName;
                    document.FilePath = path;
                    document.CourseId = Convert.ToInt32(TempData["courseid"]);
                    if (TempData["modulid"] != null)
                    {
                        document.ModuleId = Convert.ToInt32(TempData["modulid"]);
                    }
                    if (TempData["activityid"] != null)
                    {
                        document.ActivityId = Convert.ToInt32(TempData["activityid"]);
                    }
                    db.Documents.Add(document);
                    db.SaveChanges();
                    return RedirectToAction("DocumentFilter", new { courseid = TempData["courseid"], modulid = TempData["modulid"], activityid = TempData["activityid"] });
                }
                ViewBag.Message = "Upload successful";
                return RedirectToAction("DocumentFilter", new { courseid = TempData["courseid"], modulid = TempData["modulid"], activityid = TempData["activityid"] });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                ViewBag.Message = "Upload failed";
                return RedirectToAction("DocumentFilter", new { courseid = TempData["courseid"], modulid = TempData["modulid"], activityid = TempData["activityid"] });
                //  return RedirectToAction("Index");
            }

        }

        public FilePathResult GetFileFromDisk(int documenid)
        {
            string fileName = db.Documents.Where(z => z.DocumentId == documenid).Select(x => x.FileName).SingleOrDefault();
            //int lengthToRemove = documenid.ToString().Length;
            //fileName = fileName.Remove(0,lengthToRemove).Trim();
            return File("~/Files/" + fileName, MediaTypeNames.Text.Plain, fileName);
        }



        // GET: Students/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser student = db.Users.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

 
        // GET: Students/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser student = db.Users.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseID", "Name", student.CourseId);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,CourseId,Email,PhoneNumber")] ApplicationUser student)
        {
            if (ModelState.IsValid)
            {
                student.PasswordHash = db.Users.AsNoTracking().FirstOrDefault(z => z.Id == student.Id).PasswordHash;
                student.SecurityStamp = db.Users.AsNoTracking().FirstOrDefault(z => z.Id == student.Id).SecurityStamp;
                student.UserName = student.Email;
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseID", "Name", student.CourseId);
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser student = db.Users.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser student = db.Users.Find(id);
            db.Users.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }





        //public ActionResult Create()
        //{
        //    ViewBag.CourseId = new SelectList(db.Courses, "CourseID", "Name");
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "FirstName,LastName,CourseId,Email,PhoneNumber")] ApplicationUser applicationUser)
        //{
        //    if (ModelState.IsValid)
        //    {

        //        applicationUser.UserName = applicationUser.Email;
        //        db.Users.Add(applicationUser);
        //        db.SaveChanges();
        //        var userStore = new UserStore<ApplicationUser>(db);
        //        var userManager = new UserManager<ApplicationUser>(userStore);
        //        userManager.AddToRole(applicationUser.Id, "Student");
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.CourseId = new SelectList(db.Courses, "CourseID", "Name", applicationUser.CourseId);
        //    return View(applicationUser);
        //}


    }
}
