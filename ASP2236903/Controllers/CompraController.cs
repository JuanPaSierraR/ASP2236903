using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP2236903.Models;

namespace ASP2236903.Controllers
{
    public class CompraController : Controller
    {
        // GET: Compra
        public ActionResult Index()
        {
            using (var db = new invent2021Entities())
            {
                return View(db.compra.ToList());
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(compra compra)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var db = new invent2021Entities())
                {
                    db.compra.Add(compra);
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
                var findcompra = db.compra.Find(id);
                return View(findcompra);
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new invent2021Entities())
                {
                    var findcompra = db.compra.Find(id);
                    db.compra.Remove(findcompra);
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
                    compra findcompra  = db.compra.Where(a => a.id == id).FirstOrDefault();
                    return View(findcompra);
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
        public ActionResult Edit(compra editCompra)
        {
            try
            {
                using (var db = new invent2021Entities())
                {
                    compra user = db.compra.Find(editCompra.id);
                    user.fecha = editCompra.fecha;
                    user.total = editCompra.id_usuario;
                    user.id_usuario = editCompra.id_usuario;
                    user.id_cliente = editCompra.id_cliente;


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
        public ActionResult ListaCliente()
        {
            using (var db = new invent2021Entities())
            {
                return PartialView(db.cliente.ToList());
            }
        }
        public ActionResult ListaUsuario()
        {
            using (var db = new invent2021Entities())
            {
                return PartialView(db.usuario.ToList());
            }
        }
        public static string NombreUsuario(int idusuario)
        {
            using (var db = new invent2021Entities())
            {
                return db.usuario.Find(idusuario).nombre;
            }
        }
        public static string NombreCliente(int idCliente)
        {
            using (var db = new invent2021Entities())
            {
                return db.cliente.Find(idCliente).nombre;
            }
        }
        
        }

    }
