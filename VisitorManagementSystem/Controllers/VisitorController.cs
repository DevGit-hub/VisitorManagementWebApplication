using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
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
        public ActionResult Create([Bind(Include = "VisitorID,FullName,NIC,Phone,Purpose,PersonToVisit,CheckInTime")] Visitor visitor)
        {

            if (db.Visitors.Any(v =>
                   v.NIC == visitor.NIC &&
                   !v.IsDeleted &&
                   v.Status == "Checked In"))
            {
                ModelState.AddModelError(
                    "NIC",
                    "This visitor is already checked in."
                );

                return View(visitor);
            }

            if (ModelState.IsValid)
            {
                visitor.CheckInTime = DateTime.Now;
                visitor.Status = "Checked In";

                visitor.CreatedBy = Session["Username"].ToString();
                visitor.CreatedDate = DateTime.Now;
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
                var existing = db.Visitors.Find(visitor.VisitorID);

                existing.FullName = visitor.FullName;
                existing.NIC = visitor.NIC;
                existing.Phone = visitor.Phone;
                existing.Purpose = visitor.Purpose;
                existing.PersonToVisit = visitor.PersonToVisit;
                existing.CheckInTime = visitor.CheckInTime;
                existing.CheckOutTime = visitor.CheckOutTime;

                existing.UpdatedBy = Session["Username"].ToString();
                existing.UpdatedDate = DateTime.Now;

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
                visitor.DeletedBy = Session["Username"].ToString();
                visitor.DeletedDate = DateTime.Now;
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
