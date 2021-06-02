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
                    puesto = e.PUESTO,
                    funciones = e.FUNCIONES,
                    empresa = e.EMPRESA,
                    noEmpleados = e.NO_EMPLEADOS,
                    pais = e.PAIS,
                    fechaInicio = e.FECHA_INICIO,
                    fechaFin = e.FECHA_FIN,
                    fechaAlta = (DateTime) e.FECHA_ALTA,
                    retMensBruta = e.RET_MENS_BRUTA,
                    retMensNeta = e.RET_MENS_NETA
                }).ToList();
            con.Dispose();
            ViewBag.usuario = usuario;

            return View(expLaboral);
        }

        // GET: Experiencia/Create/5
        public ActionResult Create(int id) {
            ViewBag.idUsuario = id;
            return View();
        }

        // POST: Experiencia/Create/5
        [HttpPost]
        public JsonResult Create(int id, FormCollection collection) {
            try {
                EXP_LABORAL newExp = new EXP_LABORAL();
                newExp.LLAVE_USUARIO = id;
                newExp.PAIS = collection["pais"];
                newExp.EMPRESA = collection["empresa"];
                newExp.PUESTO = collection["puesto"];
                newExp.FUNCIONES = collection["funciones"];
                newExp.NO_EMPLEADOS = Int32.Parse(collection["num_empledos"]);
                newExp.FECHA_INICIO = collection["fecha_inicio"];
                newExp.FECHA_FIN = collection["fecha_fin"];
                newExp.RET_MENS_BRUTA = Int64.Parse(collection["ret_men_bruta"]);
                newExp.RET_MENS_NETA = Int64.Parse(collection["ret_men_neta"]);
                newExp.FECHA_ALTA = DateTime.Parse(DateTime.Now.ToString());

                BOLSA_EXAMENEntities con = new BOLSA_EXAMENEntities();
                con.EXP_LABORAL.Add(newExp);
                con.SaveChanges();
                con.Dispose();

                var data = new { status = 1, msg = "Se han guardado los cambios correctamente" };

                return Json(data, JsonRequestBehavior.AllowGet);
            } catch {
                var data = new { status = 0, msg = "Ha ocurrido un error al actualizar el registro sobre la BD, reportar con su administrador" };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Experiencia/Edit/5
        public ActionResult Edit(int id) {
            BOLSA_EXAMENEntities con = new BOLSA_EXAMENEntities();

            ExpLaboral expLaboral = (from e in con.EXP_LABORAL
                where e.LLAVE_EXP_LAB == id
                select new ExpLaboral {
                    LlaveExpLab = e.LLAVE_EXP_LAB,
                    LlaveUsuario = (long) e.LLAVE_USUARIO,
                    pais = e.PAIS,
                    empresa = e.EMPRESA,
                    puesto = e.PUESTO,
                    funciones = e.FUNCIONES,
                    noEmpleados = e.NO_EMPLEADOS,
                    fechaInicio = e.FECHA_INICIO,
                    fechaFin = e.FECHA_FIN,
                    retMensBruta = e.RET_MENS_BRUTA,
                    retMensNeta = e.RET_MENS_NETA,
                    fechaAlta = (DateTime) e.FECHA_ALTA
                }).First();
            con.Dispose();

            return View(expLaboral);
        }

        // POST: Experiencia/Edit/5
        [HttpPost]
        public JsonResult Edit(int id, FormCollection collection) {
            try {
                BOLSA_EXAMENEntities con = new BOLSA_EXAMENEntities();

                var expLaboral = (from e in con.EXP_LABORAL
                    where e.LLAVE_EXP_LAB == id
                    select e).First();
                expLaboral.PAIS = collection["pais"];
                expLaboral.EMPRESA = collection["empresa"];
                expLaboral.PUESTO = collection["puesto"];
                expLaboral.FUNCIONES = collection["funciones"];
                expLaboral.NO_EMPLEADOS = Int32.Parse(collection["num_empledos"]);
                expLaboral.FECHA_INICIO = collection["fecha_inicio"];
                expLaboral.FECHA_FIN = collection["fecha_fin"];
                expLaboral.RET_MENS_BRUTA = Int64.Parse(collection["ret_men_bruta"]);
                expLaboral.RET_MENS_NETA = Int64.Parse(collection["ret_men_neta"]);

                con.Entry(expLaboral).State = System.Data.Entity.EntityState.Modified;
                con.SaveChanges();
                con.Dispose();

                var data = new { status = 1, msg = "Se ha actualizado el registro correctamente" };

                return Json(data, JsonRequestBehavior.AllowGet);
            } catch (Exception e) {
                var data = new { status = 0, msg = "Ha ocurrido un error al actualizar el registro sobre la BD, reportar con su administrador" };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Experiencia/Delete/5
        [HttpPost]
        public ActionResult Delete(int id) {
            try {
                BOLSA_EXAMENEntities con = new BOLSA_EXAMENEntities();

                var expLaboral = (from e in con.EXP_LABORAL
                    where e.LLAVE_EXP_LAB == id
                    select e).FirstOrDefault();
                con.EXP_LABORAL.Remove(expLaboral);
                
                con.SaveChanges();
                con.Dispose();

                var data = new { status = 1, msg = "Se ha eliminado el registro correctamente" };

                return Json(data, JsonRequestBehavior.AllowGet);
            } catch {
                var data = new { status = 0, msg = "Ha ocurrido un error al eliminar el registro sobre la BD, reportar con su administrador" };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
