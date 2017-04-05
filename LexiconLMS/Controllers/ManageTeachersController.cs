using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LexiconLMS.Models;

namespace LexiconLMS.Controllers
{
    public class ManageTeachersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Students
        public ActionResult Index(string searchString)
        {

            var role = db.Roles.SingleOrDefault(m => m.Name == "Teacher").Id;
            var teachers = db.Users.Where(u => u.Roles.Any(r => r.RoleId == role));
            if (!String.IsNullOrEmpty(searchString))
            {
                teachers = teachers.Where(s => s.FirstName.Contains(searchString)
                                              || s.LastName.Contains(searchString) || s.UserName.Contains(searchString));

                return View(teachers.ToList());
            }
            return View(teachers.ToList());
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
            ApplicationUser teacher = db.Users.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            //ViewBag.CourseId = new SelectList(db.Courses, "CourseID", "Name", teacher.CourseId);
            return View(teacher);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,CourseId,Email,PhoneNumber")] ApplicationUser teacher)
        {
            if (ModelState.IsValid)
            {
                teacher.PasswordHash = db.Users.AsNoTracking().FirstOrDefault(z => z.Id == teacher.Id).PasswordHash;
                teacher.SecurityStamp = db.Users.AsNoTracking().FirstOrDefault(z => z.Id == teacher.Id).SecurityStamp;
                teacher.UserName = teacher.Email;
                db.Entry(teacher).State = EntityState.Modified;
                db.SaveChanges();
                TempData["successmessage"] = "Information om läraren " + teacher.Fullname + " har ändrats!";
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseID", "Name", teacher.CourseId);
            return View(teacher);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser teacher = db.Users.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser teacher = db.Users.Find(id);
            db.Users.Remove(teacher);
            db.SaveChanges();
            TempData["successmessage"] = "Läraren " + teacher.Fullname + " har tagits bort!";
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
    }
}
