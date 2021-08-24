using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP2236903.Models;

namespace ASP2236903.Controllers
{
    public class Producto_imagenController : Controller
    {
        // GET: Producto_imagen
        public ActionResult Index()
        {
            using (var db = new invent2021Entities())
            {
                return View(db.producto_imagen.ToList());
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(producto_imagen producto_Imagen)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var db = new invent2021Entities())
                {
                    db.producto_imagen.Add(producto_Imagen);
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
                var findpi = db.producto_imagen.Find(id);
                return View(findpi);
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new invent2021Entities())
                {
                    var findpi = db.producto_imagen.Find(id);
                    db.producto_imagen.Remove(findpi);
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
                    producto_imagen findpi = db.producto_imagen.Where(a => a.id == id).FirstOrDefault();
                    return View(findpi);
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
        public ActionResult Edit(producto_imagen editpi)
        {
            try
            {
                using (var db = new invent2021Entities())
                {
                    producto_imagen user = db.producto_imagen.Find(editpi.id);
                    user.imagen = editpi.imagen;
                    user.id_producto = editpi.id_producto;



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
            public ActionResult ListaProducto()
            {
                using (var db = new invent2021Entities())
                {
                    return PartialView(db.producto.ToList());
                }
            }
        }
    }