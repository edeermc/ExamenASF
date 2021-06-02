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
        public ActionResult Create() {
            return View();
        }

        // POST: Usuarios/Create
        [HttpPost]
        public JsonResult Create(FormCollection collection) {
            try {
                // TODO: Add insert logic here
                USUARIOS newUser = new USUARIOS();
                newUser.USUARIO = collection["usuario"];
                newUser.EMAIL = collection["correo"];
                newUser.FECHA_ALTA = DateTime.Parse(DateTime.Today.ToString());

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

                var data = new { status = 1, msg = "Se ha insertado el registro correctamente" };

                return Json(data, JsonRequestBehavior.AllowGet);
            } catch {
                var data = new { status = 0, msg = "Ha ocurrido un error al insertar el registro sobre la BD, reportar con su administrador" };
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

                var data = new { status = 1, msg = "Se ha insertado el registro correctamente" };

                return Json(data, JsonRequestBehavior.AllowGet);
            } catch (Exception e) {
                var data = new { status = 0, msg = "Ha ocurrido un error al eliminar el registro sobre la BD, reportar con su administrador" };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Usuarios/Delete/5
        public JsonResult Delete(int id) {
            try {
                BOLSA_EXAMENEntities con = new BOLSA_EXAMENEntities();

                var usuario = (from u in con.USUARIOS
                               where u.LLAVE_USUARIO == id
                               select u).First();
                con.USUARIOS.Remove(usuario);
                con.SaveChanges();
                con.Dispose();

                var data = new { status = 1, msg = "Se ha insertado el registro correctamente" };

                return Json(data, JsonRequestBehavior.AllowGet);
            } catch {
                var data = new { status = 0, msg = "Ha ocurrido un error al eliminar el registro sobre la BD, reportar con su administrador" };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
