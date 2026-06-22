using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VisitorManagementSystem.Models;

namespace VisitorManagementSystem.Controllers
{


    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var db = new VisitorContext())
            {
                ViewBag.TotalVisitors = db.Visitors.Count(v => !v.IsDeleted);

                ViewBag.TodayVisitors =
                    db.Visitors.Count(v =>
                    !v.IsDeleted  && v.CheckInTime >= DateTime.Today);
                ViewBag.CurrentVisitors =
                    db.Visitors.Count(v =>
                    !v.IsDeleted && v.Status == "checked In");
                return View();
            }
        }

       

        
        public ActionResult Dashboard()
        {
            using (var db = new VisitorContext())
            {
                var totalVisitors = db.Visitors.Count();

                var todayVisitors = db.Visitors
                    .Count(v => v.CheckInTime >= DateTime.Today);

                ViewBag.TotalVisitors = totalVisitors;
                ViewBag.TodayVisitors = todayVisitors;

                return View();
            }
        }
    }
}