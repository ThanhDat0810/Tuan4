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
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            BigSchoolModel context = new BigSchoolModel();
            var upcommingCourse = context.Course.Where(p => p.DateTime > DateTime.Now).OrderBy(p => p.DateTime).ToList();
            var userID = User.Identity.GetUserId();
            foreach (Course i in upcommingCourse)
            {
                ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(i.LecturerId);
                //i.LecturerId = user.Name;
                i.Name = user.Name;

                // lay ds tham gia khoa hoc
                if (userID != null)
                {
                    i.isLogin = true;
                    // kt user do chua tham gia khoa hoc
                    Attendance find = context.Attendance.FirstOrDefault(p => p.CourseId == i.Id && p.Attendee == userID);
                    if (find == null)
                        i.isShowGoing = true;

                    //ktra user đã theo dõi giảng viên của khóa học ?
                    Following findFollow = context.Following.FirstOrDefault(p => p.FollowerId == userID && p.FolloweeId == i.LecturerId);
                    if (findFollow == null)
                        i.isShowFollow = true;
                }
            }
            return View(upcommingCourse);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
       
    }
}