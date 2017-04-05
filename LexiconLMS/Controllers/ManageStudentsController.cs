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
                TempData["successmessage"] = "Information om eleven " + student.Fullname + " har ändrats!";
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
            TempData["successmessage"] = "Eleven " + student.Fullname + " har tagits bort!";
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
