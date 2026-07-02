using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VisitorManagementSystem.Models;

namespace VisitorManagementSystem.Controllers
{

    public static class VisitorStatus
    {
        public const string CheckedIn = "Checked In";
        public const string CheckedOut = "Checked Out";
    }


    public class HomeController : Controller
    {
        public static class VisitorStatus
        {
            public const string CheckedIn = "Checked In";
            public const string CheckedOut = "Checked Out";
        }

        public ActionResult Index(DateTime? fromDate, DateTime? toDate)
        {
            using (var db = new VisitorContext())
            {
                var visitors = db.Visitors.Where(v => !v.IsDeleted);

                // TOTAL
                ViewBag.TotalVisitors = visitors.Count();

                // TODAY
                ViewBag.TodayVisitors = visitors.Count(v =>
                    v.CheckInTime >= DateTime.Today);

                // YESTERDAY
                var yesterday = DateTime.Today.AddDays(-1);
                ViewBag.YesterdayVisitors = visitors.Count(v =>
                    v.CheckInTime >= yesterday &&
                    v.CheckInTime < DateTime.Today);

                // CURRENT INSIDE
                ViewBag.CurrentVisitors = visitors.Count(v =>
                    v.Status == "Checked In");

                // THIS MONTH
                var startMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                ViewBag.MonthVisitors = visitors.Count(v =>
                    v.CheckInTime >= startMonth);

                // THIS YEAR
                var startYear = new DateTime(DateTime.Today.Year, 1, 1);
                ViewBag.YearVisitors = visitors.Count(v =>
                    v.CheckInTime >= startYear);

                // CUSTOM FILTER
                if (fromDate != null && toDate != null)
                {
                    ViewBag.FilterVisitors = visitors.Count(v =>
                        v.CheckInTime >= fromDate &&
                        v.CheckInTime <= toDate);
                }

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