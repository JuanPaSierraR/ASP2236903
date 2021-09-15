using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP2236903.Models;
using Rotativa;
using System.IO;

namespace ASP2236903.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {
            using (var db = new invent2021Entities())
            {
                return View(db.cliente.ToList());
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(cliente cliente)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var db = new invent2021Entities())
                {
                    db.cliente.Add(cliente);
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
                var FindCliente = db.cliente.Find(id);
                return View(FindCliente);
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new invent2021Entities())
                {
                    var FindCliente = db.cliente.Find(id);
                    db.cliente.Remove(FindCliente);
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
                    cliente FindCliente = db.cliente.Where(a => a.id == id).FirstOrDefault();
                    return View(FindCliente);
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
        public ActionResult Edit(cliente editCliente)
        {
            try
            {
                using (var db = new invent2021Entities())
                {
                    cliente user = db.cliente.Find(editCliente.id);
                    user.nombre = editCliente.nombre;
                    user.documento = editCliente.documento;
                    user.email = editCliente.email;
                    


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
        public ActionResult Reporte()
        {
            try
            {
                var db = new invent2021Entities();
                var query = from tabCompra in db.compra
                            join tabCliente in db.cliente on tabCompra.id_cliente equals tabCliente.id
                            select new Reporte
                            {
                                nombre = tabCliente.nombre,
                                documentoCliente = tabCliente.documento,
                                emailCliente = tabCliente.email,
                                totalCompra = tabCompra.total,
                                fechaCompra = tabCompra.fecha

                            };
                return View(query);

            }catch(Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();
            }
        }

        public ActionResult PdfReporte()
        {
            return new ActionAsPdf("Reporte") { FileName = "reporte.pdf" };
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
                if (file != null)
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

                    foreach (string row in CsvData.Split('\n'))
                    {
                        if (!string.IsNullOrEmpty(row))
                        {
                            var newCliente = new cliente
                            {
                                nombre = row.Split(';')[0],
                                documento = row.Split(';')[1],
                                email = row.Split(';')[2],
                                

                            };

                            using (var db = new invent2021Entities())
                            {
                                db.cliente.Add(newCliente);
                                db.SaveChanges();
                            }
                        }
                    }
                }
                return View();

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();

            }
        }
    }
}