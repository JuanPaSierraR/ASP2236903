using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP2236903.Models;

namespace ASP2236903.Controllers
{
    public class UsuarioRolController : Controller
    {
        // GET: UsuarioRol
        public ActionResult Index()
        {
            using (var db = new invent2021Entities())
            {
                return View(db.usuariorol.ToList());
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(usuariorol usuariorol)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var db = new invent2021Entities())
                {
                    db.usuariorol.Add(usuariorol);
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
                var findur = db.usuariorol.Find(id);
                return View(findur);
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new invent2021Entities())
                {
                    var findur = db.usuariorol.Find(id);
                    db.usuariorol.Remove(findur);
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
                    usuariorol findur = db.usuariorol.Where(a => a.id == id).FirstOrDefault();
                    return View(findur);
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
        public ActionResult Edit(usuariorol editusuariorol)
        {
            try
            {
                using (var db = new invent2021Entities())
                {
                    usuariorol user = db.usuariorol.Find(editusuariorol.id);
                    user.idUsuario = editusuariorol.idUsuario;
                    user.idRol = editusuariorol.idRol;




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
        public ActionResult ListaUsuario()
        {
            using (var db = new invent2021Entities())
            {
                return PartialView(db.usuario.ToList());
            }
        }
        public ActionResult Listaroles()
        {
            using (var db = new invent2021Entities())
            {
                return PartialView(db.roles.ToList());
            }
        }
    }
}