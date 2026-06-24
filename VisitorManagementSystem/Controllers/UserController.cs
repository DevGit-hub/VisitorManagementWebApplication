using System;
using System.Linq;
using System.Web.Mvc;
using VisitorManagementSystem.Models;
using BCrypt.Net;

namespace VisitorManagementSystem.Controllers
{
    public class UserController : Controller
    {
        private VisitorContext db = new VisitorContext();

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["UserId"] == null)
            {
                filterContext.Result =
                    RedirectToAction("Login", "Account");
                return;
            }

            if (Session["Role"].ToString() != "Admin")
            {
                filterContext.Result =
                    RedirectToAction("Index", "Home");
                return;
            }

            base.OnActionExecuting(filterContext);
        }

        public ActionResult Index()
        {
            return View(db.Users
                          .Where(u => !u.IsDeleted)
                          .ToList()
    );
        }

        public ActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(User user)
        {
            if (ModelState.IsValid)
            {
                user.Role = "User";

                user.Password =
                    BCrypt.Net.BCrypt.HashPassword(user.Password);

                user.CreatedBy =
                    Session["Username"].ToString();

                user.CreatedDate =
                    DateTime.Now;

                db.Users.Add(user);
                db.SaveChanges();

                return RedirectToAction("Users");
            }

            return View(user);
        }

        public ActionResult CreateAdmin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAdmin(User user)
        {
            if (ModelState.IsValid)
            {
                user.Role = "Admin";

                user.Password =
                    BCrypt.Net.BCrypt.HashPassword(user.Password);

                user.CreatedBy =
                    Session["Username"].ToString();

                user.CreatedDate =
                    DateTime.Now;

                db.Users.Add(user);
                db.SaveChanges();

                return RedirectToAction("Admins");
            }

            return View(user);
        }

        public ActionResult Admins()
        {
            var admins = db.Users
                  .Where(u => u.Role == "Admin" &&
                              !u.IsDeleted)
                  .ToList();

            return View(admins);
        }

        public ActionResult Users()
        {
            var users = db.Users
                  .Where(u => u.Role == "User" &&
                              !u.IsDeleted)
                  .ToList();

            return View(users);
        }

        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            User user = db.Users.Find(id);

            if(user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = db.Users.Find(user.UserId);

                existingUser.FullName = user.FullName;
                existingUser.NIC = user.NIC;
                existingUser.Username = user.Username;
                existingUser.Role = user.Role;

                existingUser.UpdatedBy = Session["Username"].ToString();
                existingUser.UpdatedDate = DateTime.Now;

                db.SaveChanges();

                if (existingUser.Role == "Admin")
                {
                    return RedirectToAction("Admins");
                }

                return RedirectToAction("Users");
            }

            return View(user);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            User user = db.Users.Find(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            //  super admin protection
            if (user.Username == "Devika_admin")
            {
                TempData["Error"] = "Super Admin account cannot be deleted.";
                return RedirectToAction("Admins");
            }

           

            user.IsDeleted = true;

            user.DeletedBy = Session["Username"].ToString();
            user.DeletedDate = DateTime.Now;

            db.SaveChanges();

            return RedirectToAction("Users");
        }


    }
}