using ExamenASF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
                                          fechaAlta = (DateTime)u.FECHA_ALTA
                                      }).ToList();
            con.Dispose();

            return View(usuarios);
        }

        // GET: Usuarios/Create
        public ActionResult Create() {
            return View();
        }

        // POST: Usuarios/Create
        [HttpPost]
        public JsonResult Create(FormCollection collection) {
            try {
                USUARIOS newUser = new USUARIOS();
                newUser.USUARIO = collection["usuario"];
                newUser.EMAIL = collection["correo"];
                newUser.FECHA_ALTA = DateTime.Parse(DateTime.Now.ToString());

                // Encriptar la contraseña en MD5 D:
                var contrasena = Encoding.ASCII.GetBytes(collection["contrasena"]);
                var md5 = new MD5CryptoServiceProvider();
                var md5data = md5.ComputeHash(contrasena);
                var asciiEncoding = new ASCIIEncoding();
                var hashedPassword = asciiEncoding.GetString(md5data);
                newUser.PASS = hashedPassword;

                BOLSA_EXAMENEntities con = new BOLSA_EXAMENEntities();
                con.USUARIOS.Add(newUser);
                con.SaveChanges();
                con.Dispose();

                var data = new { status = 1, msg = "Se han guardado los cambios correctamente" };

                return Json(data, JsonRequestBehavior.AllowGet);
            } catch {
                var data = new { status = 0, msg = "Ha ocurrido un error al actualizar el registro sobre la BD, reportar con su administrador" };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int id) {
            BOLSA_EXAMENEntities con = new BOLSA_EXAMENEntities();

            Usuario usuario = (from u in con.USUARIOS
                               where u.LLAVE_USUARIO == id
                               select new Usuario {
                                   LlaveUsuario = u.LLAVE_USUARIO,
                                   usuario = u.USUARIO,
                                   email = u.EMAIL,
                                   fechaAlta = (DateTime)u.FECHA_ALTA
                               }).First();
            con.Dispose();

            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection) {
            try {
                BOLSA_EXAMENEntities con = new BOLSA_EXAMENEntities();

                var usuario = (from u in con.USUARIOS
                               where u.LLAVE_USUARIO == id
                               select u).First();
                usuario.USUARIO = collection["usuario"];
                usuario.EMAIL = collection["correo"];
                if (collection["contrasena"].Length > 0) {
                    // Encriptar la contraseña en MD5 D:
                    var contrasena = Encoding.ASCII.GetBytes(collection["contrasena"]);
                    var md5 = new MD5CryptoServiceProvider();
                    var md5data = md5.ComputeHash(contrasena);
                    var asciiEncoding = new ASCIIEncoding();
                    var hashedPassword = asciiEncoding.GetString(md5data);
                    usuario.PASS = hashedPassword;
                }

                con.Entry(usuario).State = System.Data.Entity.EntityState.Modified;
                con.SaveChanges();
                con.Dispose();

                var data = new { status = 1, msg = "Se ha actualizado el registro correctamente" };

                return Json(data, JsonRequestBehavior.AllowGet);
            } catch (Exception e) {
                var data = new { status = 0, msg = "Ha ocurrido un error al actualizar el registro sobre la BD, reportar con su administrador" };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Usuarios/Delete/5
        public JsonResult Delete(int id) {
            try {
                BOLSA_EXAMENEntities con = new BOLSA_EXAMENEntities();
                
                var expLaboral = (from el in con.EXP_LABORAL
                               where el.LLAVE_USUARIO == id
                               select el).ToArray();
                if (expLaboral.Length > 0) { 
                    con.EXP_LABORAL.RemoveRange(expLaboral);
                }
                
                var datosPersonales = (from dp in con.DATOS_PERSONALES
                               where dp.LLAVE_USUARIO == id
                               select dp).FirstOrDefault();
                if (datosPersonales != null) { 
                    con.DATOS_PERSONALES.Remove(datosPersonales);
                }

                var usuario = (from u in con.USUARIOS
                               where u.LLAVE_USUARIO == id
                               select u).First();
                con.USUARIOS.Remove(usuario);

                con.SaveChanges();
                con.Dispose();

                var data = new { status = 1, msg = "Se ha eliminado el registro correctamente" };

                return Json(data, JsonRequestBehavior.AllowGet);
            } catch {
                var data = new { status = 0, msg = "Ha ocurrido un error al eliminar el registro sobre la BD, reportar con su administrador" };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Usuarios/DatosPersonales/5
        public ActionResult DatosPersonales(int id) {
            BOLSA_EXAMENEntities con = new BOLSA_EXAMENEntities();

            Usuario usuario = (from u in con.USUARIOS
                where u.LLAVE_USUARIO == id
                select new Usuario {
                    LlaveUsuario = u.LLAVE_USUARIO,
                    usuario = u.USUARIO,
                    email = u.EMAIL,
                    fechaAlta = (DateTime)u.FECHA_ALTA
                }).First();

            DatosPersonales datosPersonales = (from u in con.DATOS_PERSONALES
                where u.LLAVE_USUARIO == id
                select new DatosPersonales {
                    LlaveDatoPersonal = u.LLAVE_DATO_PERSONAL,
                    LlaveUsuario = (long) u.LLAVE_USUARIO,
                    nombre = u.NOMBRE,
                    paterno = u.PATERNO,
                    materno = u.MATERNO,
                    ciudad = u.CIUDAD,
                    calle = u.CALLE,
                    numero = u.NUMERO,
                    colonia = u.COLONIA,
                    telefono = u.TELEFONO,
                    otroTel = u.OTRO_TEL,
                    fechaNac = (DateTime) u.FECHA_NAC,
                    curp = u.CURP,
                    rfc = u.RFC,
                    pasaporte = u.PASAPORTE,
                    cartilla = u.CARTILLA,
                    genero = u.GENERO,
                    edoCivil = u.EDO_CIVIL,
                    fechaAlta = (DateTime) u.FECHA_ALTA,
                    mun = u.MUN,
                    edo = u.EDO
                }).FirstOrDefault();
            if (datosPersonales == null) {
                // Creamos el detalle de datos personales del usuario
                DATOS_PERSONALES datosPersonales2 = new DATOS_PERSONALES();
                datosPersonales2.LLAVE_USUARIO = id;
                datosPersonales2.FECHA_ALTA = DateTime.Parse(DateTime.Now.ToString());
                datosPersonales2.FECHA_NAC = DateTime.Parse(DateTime.Now.ToString());
                con.DATOS_PERSONALES.Add(datosPersonales2);
                con.SaveChanges();

                datosPersonales = (from u in con.DATOS_PERSONALES
                    where u.LLAVE_USUARIO == id
                    select new DatosPersonales {
                        LlaveDatoPersonal = u.LLAVE_DATO_PERSONAL,
                        LlaveUsuario = (long)u.LLAVE_USUARIO,
                        nombre = u.NOMBRE,
                        paterno = u.PATERNO,
                        materno = u.MATERNO,
                        ciudad = u.CIUDAD,
                        calle = u.CALLE,
                        numero = u.NUMERO,
                        colonia = u.COLONIA,
                        telefono = u.TELEFONO,
                        otroTel = u.OTRO_TEL,
                        fechaNac = (DateTime)u.FECHA_NAC,
                        curp = u.CURP,
                        rfc = u.RFC,
                        pasaporte = u.PASAPORTE,
                        cartilla = u.CARTILLA,
                        genero = u.GENERO,
                        edoCivil = u.EDO_CIVIL,
                        fechaAlta = (DateTime)u.FECHA_ALTA,
                        mun = u.MUN,
                        edo = u.EDO
                    }).FirstOrDefault();
            }

            con.Dispose();
            ViewBag.usuario = usuario;

            return View(datosPersonales);
        }


        // GET: Usuarios/DatosPersonales/5
        [HttpPost]
        public JsonResult DatosPersonales(int id, FormCollection collection) {
            try {
                BOLSA_EXAMENEntities con = new BOLSA_EXAMENEntities();

                var datosPersonales = (from u in con.DATOS_PERSONALES
                    where u.LLAVE_USUARIO == id
                    select u).FirstOrDefault();
                datosPersonales.NOMBRE = collection["nombre"];
                datosPersonales.PATERNO = collection["paterno"];
                datosPersonales.MATERNO = collection["materno"];
                datosPersonales.CIUDAD = collection["ciudad"];
                datosPersonales.CALLE = collection["calle"];
                datosPersonales.NUMERO = collection["numero"];
                datosPersonales.COLONIA = collection["colonia"];
                datosPersonales.TELEFONO = collection["telefono"];
                datosPersonales.OTRO_TEL = collection["telefono2"];
                DateTime fchNacimiento;
                DateTime.TryParse(collection["fech_nac"], out fchNacimiento);
                datosPersonales.FECHA_NAC = fchNacimiento;
                datosPersonales.CURP = collection["curp"].ToUpper();
                datosPersonales.RFC = collection["rfc"].ToUpper();
                datosPersonales.PASAPORTE = collection["pasaporte"].ToUpper();
                datosPersonales.CARTILLA = collection["cartilla"].ToUpper();
                datosPersonales.GENERO = collection["genero"];
                datosPersonales.EDO_CIVIL = collection["edo_civil"];
                datosPersonales.MUN = collection["municipio"];
                datosPersonales.EDO = collection["estado"];

                con.Entry(datosPersonales).State = System.Data.Entity.EntityState.Modified;
                con.SaveChanges();
                con.Dispose();

                var data = new { status = 1, msg = "Se han gauardado los cambios correctamente" };

                return Json(data, JsonRequestBehavior.AllowGet);
            } catch {
                var data = new { status = 0, msg = "Ha ocurrido un error al actualizar el registro sobre la BD, reportar con su administrador" };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
