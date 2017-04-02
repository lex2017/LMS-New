using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LexiconLMS.Models;
using System.IO;

namespace LexiconLMS.Controllers
{
    public class DocumentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Documents
        public ActionResult Index(int? mid, int? cid, int? aid)
        {
            var documents = db.Documents.Include(d => d.Activity).Include(d => d.Course).Include(d => d.Module);
            var docs = documents.Where(d => d.CourseId == cid && d.ModuleId == mid && d.ActivityId == aid);
            return View(documents.ToList());
        }

        // GET: Documents/Details/5
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

        // GET: Documents/Create
        public ActionResult Create(int? mId, int? cId, int? aId)
        {
            //ViewBag.ActivityId = new SelectList(db.Activities, "ActivityId", "Name");
            //ViewBag.CourseId = new SelectList(db.Courses, "CourseID", "Name");
            //ViewBag.ModuleId = new SelectList(db.Modules, "ModuleID", "Name");
            Document document = new Document();
            document.ActivityId = aId;
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "FirstName");
            document.CourseId = cId;
            document.ModuleId = mId;
            return View();
        }

        // POST: Documents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DocumentId,ApplicationUserId,DocumentName,Description,Deadline,TimeStamp,ModuleId,CourseId,ActivityId")] Document document, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    document.DocumentName = System.IO.Path.GetFileName(upload.FileName);
                    upload.SaveAs(Path.Combine(Server.MapPath("~/LMS_Documents"), upload.FileName));
                    
                }
                db.Documents.Add(document);
                db.SaveChanges();
                return RedirectToAction("Index", new { mId = document.ModuleId, cId = document.CourseId, aId = document.ActivityId } );

            }

            ViewBag.ActivityId = new SelectList(db.Activities, "ActivityId", "Type", document.ActivityId);
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "FirstName", document.ApplicationUserId);
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Name", document.CourseId);
            ViewBag.ModuleId = new SelectList(db.Modules, "GroupId", "Name", document.ModuleId);
            return View(document);
        }

        // GET: Documents/Edit/5
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
            ViewBag.ActivityId = new SelectList(db.Activities, "ActivityId", "Name", document.ActivityId);
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "FirstName", document.ApplicationUserId);
            ViewBag.CourseId = new SelectList(db.Courses, "CourseID", "Name", document.CourseId);
            ViewBag.ModuleId = new SelectList(db.Modules, "ModuleID", "Name", document.ModuleId);
            return View(document);
        }

        // POST: Documents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DocumentId,ApplicationUserId,DocumentName,Description,Deadline,TimeStamp,ModuleId,CourseId,ActivityId")] Document document)
        {
            if (ModelState.IsValid)
            {
                db.Entry(document).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { gId = document.ModuleId, cId = document.CourseId, aId = document.ActivityId });
            }
            ViewBag.ActivityId = new SelectList(db.Activities, "ActivityId", "Name", document.ActivityId);
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "FirstName", document.ApplicationUserId);
            ViewBag.CourseId = new SelectList(db.Courses, "CourseID", "Name", document.CourseId);
            ViewBag.ModuleId = new SelectList(db.Modules, "ModuleID", "Name", document.ModuleId);
            return View(document);
        }

        // GET: Documents/Delete/5
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

        // POST: Documents/Delete/5
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
