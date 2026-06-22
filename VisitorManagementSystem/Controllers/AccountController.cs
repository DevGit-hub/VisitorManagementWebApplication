using BCrypt.Net;
using System;
using System.Linq;
using System.Web.Mvc;
using VisitorManagementSystem.Models;

namespace VisitorManagementSystem.Controllers
{

    public class AccountController : Controller
    {
        private VisitorContext db = new VisitorContext();
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User model)
        {
            using (var db = new VisitorContext())
            {
                var user = db.Users
                    .FirstOrDefault(u => u.Username == model.Username
                                         && !u.IsDeleted);

                if (user != null)
                {
                    bool isValidPassword = false;

                    // If password is hashed
                    if (user.Password.StartsWith("$2"))
                    {
                        isValidPassword = BCrypt.Net.BCrypt.Verify(model.Password, user.Password);
                    }
                    else
                    {
                        // fallback for old plain text passwords
                        isValidPassword = (model.Password == user.Password);
                    }

                    if (isValidPassword)
                    {
                        Session["UserId"] = user.UserId;
                        Session["Username"] = user.Username;
                        Session["Role"] = user.Role;

                        return RedirectToAction("Index", "Home");
                    }
                }

                ViewBag.Error = "Invalid Username or Password";
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }

       
    }
}