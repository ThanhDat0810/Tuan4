using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Tuan4.Models;

namespace Tuan4.Controllers
{
    
    public class AttendancesController : ApiController
    {
        [System.Web.Http.HttpPost]
        public IHttpActionResult Attend(Course attendanceDto)
        {
            var userID = User.Identity.GetUserId();
            BigSchoolModel context = new BigSchoolModel();
            if(context.Attendance.Any(p =>p.Attendee == userID && p.CourseId == attendanceDto.Id))
            {
                //return BadRequest("the attendance already exists !");
                // xóa thông tin khóa học đã đăng ký tham gia trong bảng Attendances
                context.Attendance.Remove(context.Attendance.SingleOrDefault(p => p.Attendee == userID && p.CourseId == attendanceDto.Id));
                context.SaveChanges();
                return Ok("cancel");
            }
            var attendance = new Attendance() { CourseId = attendanceDto.Id, Attendee = User.Identity.GetUserId() };
            context.Attendance.Add(attendance);
            context.SaveChanges();
            return Ok();
        }
    }
}
