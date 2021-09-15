using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP2236903.Models;
using System.IO;

namespace ASP2236903.Controllers
{
    public class ProveedorController : Controller
    {
        // GET: Proveedor
        public ActionResult Index()
        {
            using (var db = new invent2021Entities())
            {
                return View(db.proveedor.ToList());
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(proveedor proveedor)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var db = new invent2021Entities())
                {
                    db.proveedor.Add(proveedor);
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
                var FindProveedor = db.proveedor.Find(id);
                return View(FindProveedor);
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new invent2021Entities())
                {
                    var FindProveedor = db.proveedor.Find(id);
                    db.proveedor.Remove(FindProveedor);
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
                    proveedor FindProveedor = db.proveedor.Where(a => a.id == id).FirstOrDefault();
                    return View(FindProveedor);
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
        public ActionResult Edit(proveedor editProved)
        {
            try
            {
                using (var db = new invent2021Entities())
                {
                    proveedor user = db.proveedor.Find(editProved.id);
                    user.nombre = editProved.nombre;
                    user.direccion = editProved.direccion;
                    user.telefono = editProved.telefono;
                    user.nombre_contacto = editProved.nombre_contacto;


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
        public ActionResult uploadCsv()
        {
            return View();
        }

        [HttpPost]
        public ActionResult uploadCsv(HttpPostedFileBase file)
        {
            try
            {
                //string para guardar la ruta
                string filePath = string.Empty;

                //condicion para saber si el archivo llego
                if(file != null)
                {
                    //ruta de la carpeta que guardara el archivo
                    string path = Server.MapPath("~/Uploads/");

                    //condicion para saber si la carpeta uploads existe
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    //obtener el nombre del archivo
                    filePath = path + Path.GetFileName(file.FileName);

                    //obtener la extension del archivo
                    string extension = Path.GetExtension(file.FileName);

                    //guardar el archivo
                    file.SaveAs(filePath);

                    string CsvData = System.IO.File.ReadAllText(filePath);

                    foreach(string row in CsvData.Split('\n'))
                    {
                        if (!string.IsNullOrEmpty(row))
                        {
                            var newProveedor = new proveedor
                            {
                                nombre = row.Split(';')[0],
                                direccion = row.Split(';')[1],
                                telefono = row.Split(';')[2],
                                nombre_contacto = row.Split(';')[3],

                            };

                            using (var db = new invent2021Entities())
                            {
                                db.proveedor.Add(newProveedor);
                                db.SaveChanges();
                            }
                        }
                    }
                }
                return View();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();

            }
        }
    }

}