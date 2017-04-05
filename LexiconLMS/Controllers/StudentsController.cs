using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LexiconLMS.Models;
using Microsoft.AspNet.Identity;
using System.IO;
using System.Net.Mime;

namespace LexiconLMS.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(string searchString)
        {
            
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
            var courseid = currentUser.CourseId;
            ViewBag.coursename = db.Courses.Where(b => b.CourseID == courseid).Select(b => b.Name).SingleOrDefault();
            var course = db.Courses.Where(x => x.CourseID == courseid).FirstOrDefault();
            return View(course);
        }
        // GET: StudentsUsers
        public ActionResult Studentmodules()
        {
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
            var courseid = currentUser.CourseId;
            setCourseInfo(courseid);

            var modules = db.Modules.Where(x => x.CourseId == courseid);
            return View(modules.ToList());
        }
        // GET: StudentsUsers
        public ActionResult Studentlist()
        {
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
            var courseid = currentUser.CourseId;
            setCourseInfo(courseid);

            var role = db.Roles.SingleOrDefault(m => m.Name == "Student").Id;
            var students = db.Users.Where(u => u.Roles.Any(r => r.RoleId == role)).Where(x => x.CourseId == courseid);
            return View(students.ToList());
        }
        public ActionResult Activitylist()
        {
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
            var courseid = currentUser.CourseId;
            setCourseInfo(courseid);
            ViewBag.coursename = db.Courses.Where(b => b.CourseID == courseid).Select(b => b.Name).SingleOrDefault();
            var modules = db.Modules.Where(x => x.CourseId == courseid).Select(v => v.ModuleID);
            var activities = db.Activities.Where(p => modules.Contains(p.ModuleId));
            return View(activities.ToList());
        }
        public ActionResult ActivityFilter(int? id)
        {
            var oneActivity = db.Modules.Where(v => v.ModuleID == id).ToList();

            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
            var courseid = currentUser.CourseId;
            setCourseInfo(courseid);

            foreach (var item in oneActivity)
            {
                if (oneActivity.Count() == 1)
                {
                    ViewBag.oneElement = item.Name;
                }
            }

            IQueryable<Activity> activity = db.Activities.Where(x => x.ModuleId == id);
            return View(activity.ToList());
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
        private void setCourseInfo(int? courseId)
        {
            if (courseId != null)
            {
                ViewBag.coursename = db.Courses.Where(b => b.CourseID == courseId).Select(b => b.Name).SingleOrDefault();
                ViewBag.coursedescription = db.Courses.Where(b => b.CourseID == courseId).Select(b => b.Description).SingleOrDefault();
                ViewBag.coursestartdate = db.Courses.Where(b => b.CourseID == courseId).Select(b => b.StartDate).SingleOrDefault();
                ViewBag.courseenddate = db.Courses.Where(b => b.CourseID == courseId).Select(b => b.EndDate).SingleOrDefault();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
