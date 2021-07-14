using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tuan4.Models;

namespace Tuan4.Controllers
{
    public class CoursesController : Controller
    {
        // GET: Courses
        
        public ActionResult Create()
        {
            BigSchoolModel context = new BigSchoolModel();
            Course objCourse = new Course();
            objCourse.ListCategory = context.Category.ToList();
            return View(objCourse);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Course objCourse)
        {
            BigSchoolModel context = new BigSchoolModel();

            ModelState.Remove("LecturerId");
            if (!ModelState.IsValid)
            {
                objCourse.ListCategory = context.Category.ToList();
                return View("Create", objCourse);
            }



            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            objCourse.LecturerId = user.Id;

            context.Course.Add(objCourse);
            context.SaveChanges();
            ViewBag.Message = "Data Insert Successfully";

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Attending()
        {
            BigSchoolModel context = new BigSchoolModel();
            ApplicationUser currentUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>()
                .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var listAttendances = context.Attendance.Where(p => p.Attendee == currentUser.Id).ToList();
            var course = new List<Course>();
            
            foreach (Attendance temp in listAttendances)
            {
                Course objCourse = temp.Course;
                objCourse.Category.Name= System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>()
                    .FindById(objCourse.LecturerId).Name;
                ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
                objCourse.LecturerId = user.Id;
                course.Add(objCourse);
            }
            return View(course);
        }

        public ActionResult Mine()
        {
            BigSchoolModel context = new BigSchoolModel();

            ApplicationUser currentUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>()
                .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            
            var courses = context.Course.Where(c => c.LecturerId == currentUser.Id && c.DateTime > DateTime.Now).ToList();
            foreach(Course i in courses)
            {
                i.Category.Name= currentUser.Name;
            }
            return View(courses);
        }
    }
}