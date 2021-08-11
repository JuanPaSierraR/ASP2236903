using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP2236903.Models;

namespace ASP2236903.Controllers
{
    public class RolesController : Controller
    {
        // GET: Roles
        public ActionResult Index()
        {
            using (var db = new invent2021Entities())
            {
                return View(db.roles.ToList());
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(roles roles)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var db = new invent2021Entities())
                {
                    db.roles.Add(roles);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();
            }
        }

        public ActionResult Details(int id)
        {
            using (var db = new invent2021Entities())
            {
                var FindRoles = db.roles.Find(id);
                return View(FindRoles);
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new invent2021Entities())
                {
                    var FindRoles = db.roles.Find(id);
                    db.roles.Remove(FindRoles);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                using (var db = new invent2021Entities())
                {
                    roles FindRoles = db.roles.Where(a => a.id == id).FirstOrDefault();
                    return View(FindRoles);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();

            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(roles editRoles)
        {
            try
            {
                using (var db = new invent2021Entities())
                {
                    roles user = db.roles.Find(editRoles.id);
                    user.descripcion = editRoles.descripcion;
                    


                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();

            }
        }
    }
}