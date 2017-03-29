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
            ViewBag.coursename = db.Courses.Where(b => b.CourseID == courseid).Select(b => b.Name).SingleOrDefault();
            var modules = db.Modules.Where(x => x.CourseId == courseid);
            return View(modules.ToList());
        }
        // GET: StudentsUsers
        public ActionResult Studentlist()
        {
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
            var courseid = currentUser.CourseId;
            ViewBag.coursename = db.Courses.Where(b => b.CourseID == courseid).Select(b => b.Name).SingleOrDefault();
            var role = db.Roles.SingleOrDefault(m => m.Name == "Student").Id;
            var students = db.Users.Where(u => u.Roles.Any(r => r.RoleId == role)).Where(x => x.CourseId == courseid);
            return View(students.ToList());
        }
        public ActionResult Activitylist()
        {
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
            var courseid = currentUser.CourseId;
            ViewBag.coursename = db.Courses.Where(b => b.CourseID == courseid).Select(b => b.Name).SingleOrDefault();
            var modules = db.Modules.Where(x => x.CourseId == courseid).Select(v => v.ModuleID);
            var activities = db.Activities.Where(p => modules.Contains(p.ModuleId.Value));
            return View(activities.ToList());
        }
        public ActionResult ActivityFilter(int? id)
        {
            var oneActivity = db.Modules.Where(v => v.ModuleID == id).ToList();


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
