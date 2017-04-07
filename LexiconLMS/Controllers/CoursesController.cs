using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Web.Security;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LexiconLMS.Models;

namespace LexiconLMS.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class CoursesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Courses
        public ActionResult Index( string searchString)
        {
           
            var kurs = from m in db.Courses
                            select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                kurs = kurs.Where(s => s.Name.Contains(searchString)
                                              || s.Description.Contains(searchString));

                return View(kurs);
            }

            
            return View(db.Courses.ToList());
        }

  
        // GET: Courses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: Courses/Create
        [Authorize(Roles ="Teacher")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult Create([Bind(Include = "CourseID,Name,Description,StartDate,EndDate")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();

                TempData["successmessage"] = "Kursen " + course.Name + " har lagts till!";
                return RedirectToAction("Index");
            }

            return View(course);
        }

        // GET: Courses/Edit/5
        [Authorize(Roles = "Teacher")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult Edit([Bind(Include = "CourseID,Name,Description,StartDate,EndDate")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                TempData["successmessage"] = "Kursen " + course.Name + " har ändrats!";
                return RedirectToAction("Index");
            }
            return View(course);
        }

        // GET: Courses/Delete/5
        [Authorize(Roles = "Teacher")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
            db.SaveChanges();
            TempData["successmessage"] = "Kursen " + course.Name + " har tagits bort!";
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
