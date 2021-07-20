using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tuan4.Models;

namespace Tuan4.Controllers
{
    public class FollowingController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Follow(Following follow)
        {
            //user login la ng theo doi,folow.followeeid theo doi dc
            var userID = User.Identity.GetUserId();
            if (userID == null)
                return BadRequest("Please login Fisrt!");
            if (userID == follow.FolloweeId)
                return BadRequest("Can not follow myself!");

            BigSchoolModel context = new BigSchoolModel();
            //kt ma userid da dc theo doi chua
            Following find = context.Following.FirstOrDefault(p => p.FollowerId == userID && p.FolloweeId == follow.FolloweeId);
            if(find != null)
            {
                //return BadRequest("The already following exists!");
                context.Following.Remove(context.Following.SingleOrDefault(p => p.FollowerId == userID && p.FolloweeId == follow.FolloweeId));
                context.SaveChanges();
                return Ok("cancel");
            }

            follow.FollowerId = userID;
            context.Following.Add(follow);
            context.SaveChanges();

            return Ok();

        }
    }
}
