using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP2236903.Models;

namespace ASP2236903.Controllers
{
    public class Producto_compraController : Controller
    {
        // GET: Producto_compra
        public ActionResult Index()
        {
            using (var db = new invent2021Entities())
            {
                return View(db.producto_compra.ToList());
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(producto_compra producto_Compra)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var db = new invent2021Entities())
                {
                    db.producto_compra.Add(producto_Compra);
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
                var findproducto_compra = db.producto_compra.Find(id);
                return View(findproducto_compra);
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new invent2021Entities())
                {
                    var findproducto_compra = db.producto_compra.Find(id);
                    db.producto_compra.Remove(findproducto_compra);
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
                    producto_compra findproducto_Compra = db.producto_compra.Where(a => a.id == id).FirstOrDefault();
                    return View(findproducto_Compra);
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
        public ActionResult Edit(producto_compra editProductoCompra)
        {
            try
            {
                using (var db = new invent2021Entities())
                {
                    producto_compra user = db.producto_compra.Find(editProductoCompra.id);
                    user.id_compra = editProductoCompra.id_compra;
                    user.id_producto = editProductoCompra.id_producto;
                    user.cantidad = editProductoCompra.cantidad;



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
        public ActionResult ListaCompra()
        {
            using (var db = new invent2021Entities())
            {
                return PartialView(db.compra.ToList());
            }
        }
        public ActionResult ListaProducto()
        {
            using (var db = new invent2021Entities())
            {
                return PartialView(db.producto.ToList());
            }
        }
        public static string NombreProdcuto(int idProducto)
        {
            using (var db = new invent2021Entities())
            {
                return db.producto.Find(idProducto).nombre;
            }
        }
        public static string FechaCompra(int idCompra)
        {
            using (var db = new invent2021Entities())
            {
                return Convert.ToString(db.compra.Find(idCompra).fecha);
            }
        }


    }
}