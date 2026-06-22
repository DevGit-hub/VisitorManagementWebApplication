using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VisitorManagementSystem.Models;

namespace VisitorManagementSystem.Controllers
{
    public class VisitorController : Controller
    {
        private VisitorContext db = new VisitorContext();

        // GET: Visitors
        public ActionResult Index(string searchString)
        {
            using (var db = new VisitorContext())
            {
                var visitors = db.Visitors
                               .Where(v => !v.IsDeleted);
                if (!string.IsNullOrEmpty(searchString))
                {
                    visitors =visitors.Where(v =>
                         v.FullName.Contains(searchString) ||
                         v.NIC.Contains(searchString));
                }
                

                return View(visitors.ToList());
            }
        }

        // GET: Visitors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visitor visitor = db.Visitors
                    .FirstOrDefault(v => v.VisitorID == id && !v.IsDeleted);

            if (visitor == null)
            {
                return HttpNotFound();
            }
            return View(visitor);
        }

        // GET: Visitors/Create
        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VisitorID,FullName,NIC,Phone,Purpose,PersonToVisit,CheckInTime,CheckOutTime")] Visitor visitor)
        {

            if (db.Visitors.Any(v => v.NIC == visitor.NIC))
            {
                ModelState.AddModelError("NIC", "This NIC already exists.");
                return View(visitor);
            }

            if (ModelState.IsValid)
            {
                visitor.CheckInTime = DateTime.Now;
                visitor.Status = "Checked In";
                

                db.Visitors.Add(visitor);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(visitor);
        }

        public ActionResult CheckOut(int id)
        {
            var visitor = db.Visitors.Find(id);

            if (visitor != null)
            {
                visitor.CheckOutTime = DateTime.Now;
                visitor.Status = "Checked Out";

                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // GET: Visitors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visitor visitor = db.Visitors.Find(id);
            if (visitor == null)
            {
                return HttpNotFound();
            }
            return View(visitor);
        }

        // POST: Visitors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VisitorID,FullName,NIC,Phone,Purpose,PersonToVisit,CheckInTime,CheckOutTime")] Visitor visitor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(visitor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(visitor);
        }

        // GET: Visitors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visitor visitor = db.Visitors.Find(id);
            if (visitor == null)
            {
                return HttpNotFound();
            }
            return View(visitor);
        }

        // POST: Visitors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Visitor visitor = db.Visitors.Find(id);
            if (visitor != null)
            {
                visitor.IsDeleted = true;
                db.SaveChanges();
            }

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

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["UserId"] == null)
            {
                filterContext.Result = RedirectToAction("Login", "Account");
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
