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
    public class ActivitiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Activities
        public ActionResult Index(string searchString)
        {
            var activity = from m in db.Activities
                           select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                activity = activity.Where(s => s.Name.Contains(searchString)
                                              || s.Description.Contains(searchString) || s.Module.Course.Name.Contains(searchString));

                return View(activity.ToList());
            }
            return View(db.Activities.ToList());
        }

        public ActionResult ActivityFilter(int? id)
        {

            ViewBag.id = id;
            ViewBag.coursename = db.Courses.Where(v => v.CourseID == id).Select(x =>x.Name).SingleOrDefault().ToString();
            ViewBag.modulname = db.Modules.Where(v => v.ModuleID == id).Select(x => x.Name).SingleOrDefault().ToString();
            //ViewBag.activityname = db.Activities.Where(v => v.ModuleId == id).Select(x => x.Name).SingleOrDefault().ToString();


            IQueryable<Activity> activity = db.Activities.Where(x => x.ModuleId == id);
            return View("Index", activity.ToList());
        }
        // GET: Activities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // GET: Activities/Create
        public ActionResult Create()
        {
            ViewBag.ActivityType = new SelectList(db.ActivityTypes, "ActivityTypeID", "TypeName");
            MakeCreateDropDown(null);
            return View();
        }

        // POST: Activities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ActivityId,Name,Description,StartDate,EndDate,ModuleId,ActivityTypeID")] Activity activity)
        {
            
            if (ModelState.IsValid)
            {
             
               
                   
                db.Activities.Add(activity);
                db.SaveChanges();
                //return RedirectToAction("Index");
                return RedirectToAction("ActivityFilter", new { id = activity.ModuleId });
            }
            MakeCreateDropDown(activity);
            ViewBag.ActivityType = new SelectList(db.ActivityTypes, "ActivityTypeID", "TypeName");
            return View(activity);
        }

        private void MakeCreateDropDown(Activity activity)
        {
            ViewBag.Modules = new SelectList(db.Modules, "ModuleId", "Name", activity?.ModuleId);
        }


        // GET: Activities/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.ActivityType = new SelectList(db.ActivityTypes, "ActivityTypeID", "TypeName");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            MakeCreateDropDown(activity);
            return View(activity);
        }

        // POST: Activities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ActivityId,Name,Description,StartDate,EndDate,ModuleId,ActivityTypeID")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                ViewBag.ActivityType = new SelectList(db.ActivityTypes, "ActivityTypeID", "TypeName");
                db.Entry(activity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(activity);
        }

        // GET: Activities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // POST: Activities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Activity activity = db.Activities.Find(id);
            db.Activities.Remove(activity);
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
