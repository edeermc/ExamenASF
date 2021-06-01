using ExamenASF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamenASF.Controllers {
    public class UsuariosController : Controller {
        // GET: Usuarios
        public ActionResult Index() {
            BOLSA_EXAMENEntities con = new BOLSA_EXAMENEntities();

            List<Usuario> usuarios = (from u in con.USUARIOS
                    select new Usuario {
                        LlaveUsuario = u.LLAVE_USUARIO,
                        usuario = u.USUARIO,
                        email = u.EMAIL,
                        fechaAlta = (DateTime) u.FECHA_ALTA
                    }).ToList();
            con.Dispose();
           
            return View(usuarios);
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        [HttpPost]
        public JsonResult Create(FormCollection collection) {
            try {
                // TODO: Add insert logic here

                return Json("Todo OK", JsonRequestBehavior.AllowGet);
            } catch {
                return Json("Mori :c", JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Usuarios/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
