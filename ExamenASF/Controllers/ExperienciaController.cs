using ExamenASF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamenASF.Controllers {
    public class ExperienciaController : Controller {
        // GET: Experiencia/Usuario/4
        public ActionResult Usuario(int id) {
            BOLSA_EXAMENEntities con = new BOLSA_EXAMENEntities();
            
            Usuario usuario = (from u in con.USUARIOS
                where u.LLAVE_USUARIO == id
                select new Usuario {
                    LlaveUsuario = u.LLAVE_USUARIO,
                    usuario = u.USUARIO,
                    email = u.EMAIL,
                    fechaAlta = (DateTime)u.FECHA_ALTA
                }).First();

            List<ExpLaboral> expLaboral = (from e in con.EXP_LABORAL
                where e.LLAVE_USUARIO == id
                select new ExpLaboral {
                    LlaveExpLab = e.LLAVE_EXP_LAB,
                    LlaveUsuario = (long) e.LLAVE_USUARIO,
                    LlaveDatoPersonal = (long) e.LLAVE_DATO_PERSONAL,
                    puesto = e.PUESTO,
                    funciones = e.FUNCIONES,
                    empresa = e.EMPRESA,
                    noEmpleados = (int) e.NO_EMPLEADOS,
                    pais = e.PAIS,
                    fechaInicio = DateTime.ParseExact(e.FECHA_INICIO, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture),
                    fechaFin = DateTime.ParseExact(e.FECHA_FIN, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture),
                    fechaAlta = (DateTime) e.FECHA_ALTA,
                    retMensBruta = (long) e.RET_MENS_BRUTA,
                    retMensNeta = (long) e.RET_MENS_NETA
                }).ToList();
            con.Dispose();
            ViewBag.usuario = usuario;

            return View(expLaboral);
        }

        // GET: Experiencia/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Experiencia/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Experiencia/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Experiencia/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Experiencia/Edit/5
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

        // GET: Experiencia/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Experiencia/Delete/5
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
