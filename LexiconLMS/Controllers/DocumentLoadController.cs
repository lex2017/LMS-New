using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LexiconLMS.Models;

namespace Lexicon_LMS.Controllers
{
    public class TestDocumentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TestDocuments
        public ActionResult Index()
        {
            var documents = db.Documents.Include(d => d.Activity).Include(d => d.ApplicationUser).Include(d => d.Course).Include(d => d.Module);
            return View(documents.ToList());
        }

        // GET: TestDocuments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // GET: TestDocuments/Create
        public ActionResult Create()
        {
            ViewBag.ActivityId = new SelectList(db.Activities, "ActivityId", "Type");
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "FirstName");
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Name");
            ViewBag.GroupId = new SelectList(db.Modules, "ModuleId", "TeacherId");
            return View();
        }

        // POST: TestDocuments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DocumentId,ApplicationUserId,GroupId,CourseId,ActivityId,Title,FileName,Description,TimeStamp")] Document document)
        {
            if (ModelState.IsValid)
            {
                db.Documents.Add(document);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ActivityId = new SelectList(db.Activities, "ActivityId", "Type", document.ActivityId);
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "FirstName", document.ApplicationUserId);
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Name", document.CourseId);
            ViewBag.GroupId = new SelectList(db.Modules, "GroupId", "TeacherId", document.ModuleId);
            return View(document);
        }

        // GET: TestDocuments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActivityId = new SelectList(db.Activities, "ActivityId", "Type", document.ActivityId);
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "FirstName", document.ApplicationUserId);
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Name", document.CourseId);
            ViewBag.GroupId = new SelectList(db.Modules, "GroupId", "TeacherId", document.ModuleId);
            return View(document);
        }

        // POST: TestDocuments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DocumentId,ApplicationUserId,GroupId,CourseId,ActivityId,Title,FileName,Description,TimeStamp")] Document document)
        {
            if (ModelState.IsValid)
            {
                db.Entry(document).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ActivityId = new SelectList(db.Activities, "ActivityId", "Type", document.ActivityId);
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "FirstName", document.ApplicationUserId);
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Name", document.CourseId);
            ViewBag.GroupId = new SelectList(db.Modules, "GroupId", "TeacherId", document.ModuleId);
            return View(document);
        }

        // GET: TestDocuments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // POST: TestDocuments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Document document = db.Documents.Find(id);
            db.Documents.Remove(document);
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
    }
}
