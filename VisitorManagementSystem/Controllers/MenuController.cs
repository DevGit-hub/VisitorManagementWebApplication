using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using VisitorManagementSystem.Models;

public class MenuController : Controller
{
    private VisitorContext db = new VisitorContext();

    protected override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        if (Session["UserId"] == null)
        {
            filterContext.Result = RedirectToAction("Login", "Account");
            return;
        }

        base.OnActionExecuting(filterContext);
    }

    // SIDEBAR 
    public PartialViewResult Sidebar()
    {
        if (Session["Role"] == null)
        {
            return PartialView("_Sidebar", new List<Menu>());
        }

        var role = Session["Role"].ToString();

        var menuIds = db.RoleMenus
                        .Where(r => r.Role == role)
                        .Select(r => r.MenuId)
                        .ToList();

        var menus = db.Menus
                      .Where(m => menuIds.Contains(m.MenuId) && m.IsActive)
                      .ToList();

        return PartialView("_Sidebar", menus);
    }

    // LIST MENUS (ADMIN PANEL)
    public ActionResult Index()
    {
        return View(
            db.Menus
              .Where(m => m.IsActive)
              .ToList()
        );
    }

    // CREATE MENU (GET)
    public ActionResult Create()
    {
        ViewBag.Controllers = new List<string>
    {
        "Home",
        "Visitor",
        "User",
        "Menu"
    };

        ViewBag.Actions = new List<string>
    {
        "Index",
        "Create",
        "Edit",
        "Delete",
        "Users",
        "Admins"
    };

        ViewBag.ParentMenus = db.Menus
                        .Where(m => m.IsActive)
                        .ToList();

        return View();
    }

    // CREATE MENU (POST)
    [HttpPost]
public ActionResult Create(Menu menu, string[] Roles)
    {
        if (ModelState.IsValid)
        {
            menu.IsActive = true;

            menu.CreatedBy = Session["Username"].ToString();
            menu.CreatedDate = DateTime.Now;

            db.Menus.Add(menu);
            db.SaveChanges();

            if (Roles != null)
            {
                foreach (var role in Roles)
                {
                    db.RoleMenus.Add(new RoleMenu
                    {
                        Role = role,
                        MenuId = menu.MenuId,
                        CreatedBy = Session["Username"].ToString(),
                        CreatedDate = DateTime.Now
                    });
                }

                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        ViewBag.ParentMenus = db.Menus
                                .Where(m => m.IsActive)
                                .ToList();

        return View(menu);
    }

    // EDIT MENU
    public ActionResult Edit(int? id)
    {
        if (id == null)
        {
            return HttpNotFound();
        }

        var menu = db.Menus
                     .FirstOrDefault(m => m.MenuId == id);

        if (menu == null)
        {
            return HttpNotFound();
        }

        ViewBag.ParentMenus = db.Menus
                                .Where(m => m.MenuId != id && m.IsActive)
                                .ToList();

        return View(menu);
    }

    [HttpPost]
    public ActionResult Edit(Menu menu)
    {
        if (ModelState.IsValid)
        {
            var existing = db.Menus.Find(menu.MenuId);

            existing.DisplayName = menu.DisplayName;
            existing.Controller = menu.Controller;
            existing.Action = menu.Action;
            existing.ParentMenuId = menu.ParentMenuId;

            existing.UpdatedBy = Session["Username"].ToString();
            existing.UpdatedDate = DateTime.Now;

            // IMPORTANT: keep old value
            existing.IsActive = existing.IsActive;

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        ViewBag.ParentMenus = db.Menus.Where(m => m.IsActive).ToList();
        return View(menu);
    }

    // DELETE MENU
    public ActionResult Delete(int? id)
    {
        var menu = db.Menus.FirstOrDefault(m => m.MenuId == id);

        if (menu == null)
        {
            return HttpNotFound();
        }

        return View(menu);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
        var menu = db.Menus.Find(id);

        if (menu != null)
        {
            menu.IsActive = false;
            menu.DeletedBy = Session["Username"].ToString();
            menu.DeletedDate = DateTime.Now;
            db.SaveChanges();
        }

        return RedirectToAction("Index");
    }
}