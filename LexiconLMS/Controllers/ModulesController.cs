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
    [Authorize(Roles = "Teacher")]
    public class ModulesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Modules
        public ActionResult Index(string searchString)
        {

            var module = from m in db.Modules
                       select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                module = module.Where(s => s.Name.Contains(searchString)
                                              || s.Description.Contains(searchString));

                return View(module);
            }

  
            return View(db.Modules.ToList());
        }

        public ActionResult ModuleFilter(int courseid)
        {
            IQueryable<Module> module = db.Modules.Where(x => x.CourseId == courseid);
            ViewBag.courseid = courseid;
            if (module.Count() !=0)
            {
            //ViewBag.modulid = id;
            //ViewBag.modulname = db.Modules.Where(v => v.CourseId == id).Select(x => x.Name).SingleOrDefault().ToString();
            }
            ViewBag.coursename = db.Courses.Where(v => v.CourseID == courseid).Select(x => x.Name).SingleOrDefault().ToString();

            return View("Index", module.ToList() );
        }
        // GET: Modules/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Module module = db.Modules.Find(id);
            if (module == null)
            {
                return HttpNotFound();
            }
            return View(module);
        }

        // GET: Modules/Create
        public ActionResult Create(string coursename,int courseid)
        {
            ViewBag.coursename = coursename;
            ViewBag.courseid = courseid;
            MakeCreateDropDown(null);
            return View();
        }

        
        // POST: Modules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ModuleID,Name,Description,StartDate,EndDate")] Module module, int courseid,string coursename)
        {
            if (ModelState.IsValid)
            {
                ViewBag.coursename = coursename;
                ViewBag.courseid = courseid;
                module.CourseId = courseid;
                //ViewBag.coursename = db.Courses.Where(v => v.CourseID == courseid).Select(x => x.Name).SingleOrDefault().ToString();
                db.Modules.Add(module);
                db.SaveChanges();
                TempData["successmessage"] = "Modulen " + module.Name + " har lagts till!";
                //return RedirectToAction("Index");
                return RedirectToAction("ModuleFilter", new { courseid = module.CourseId });
            }
            MakeCreateDropDown(module);
            return View(module);
        }

        // GET: Modules/Edit/5
        public ActionResult Edit(int? id, int courseid)
        {
            ViewBag.courseid= courseid;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Module module = db.Modules.Find(id);
            if (module == null)
            {
                return HttpNotFound();
            }
            MakeCreateDropDown(module);
            return View(module);
        }

        // POST: Modules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ModuleID,Name,Description,StartDate,EndDate,CourseId")] Module module, int courseid)
        {
            if (ModelState.IsValid)
            {
                module.CourseId = courseid;
                db.Entry(module).State = EntityState.Modified;
                db.SaveChanges();

                TempData["successmessage"] = "Modulen " + module.Name + " har ändrats!";
                return RedirectToAction("ModuleFilter", new { courseid = module.CourseId });
                //return RedirectToAction("Index");
            }
            MakeCreateDropDown(module);
            return View(module);
        }

        // GET: Modules/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Module module = db.Modules.Find(id);
            if (module == null)
            {
                return HttpNotFound();
            }
            return View(module);
        }

        // POST: Modules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Module module = db.Modules.Find(id);
            db.Modules.Remove(module);
            db.SaveChanges();
            TempData["successmessage"] = "Modulen " + module.Name + " har tagits bort!";
            return RedirectToAction("Index", "Courses");
            //return RedirectToAction("Index");
        }

        private void MakeCreateDropDown(Module module)
        {
            ViewBag.Courses = new SelectList(db.Courses, "CourseId", "Name", module?.CourseId);
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
