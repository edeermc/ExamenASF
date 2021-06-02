using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamenASF.Models {
    public class DatosPersonales {
        public long LlaveDatoPersonal { get; set; }
        public long LlaveUsuario { get; set; }
        public string nombre { get; set; }
        public string paterno { get; set; }
        public string materno { get; set; }
        public string ciudad { get; set; }
        public string calle { get; set; }
        public string numero { get; set; }
        public string colonia { get; set; }
        public string telefono { get; set; }
        public string otroTel { get; set; }
        public Nullable<DateTime> fechaNac { get; set; }
        public string curp { get; set; }
        public string rfc { get; set; }
        public string pasaporte { get; set; }
        public string cartilla { get; set; }
        public string genero { get; set; }
        public string edoCivil { get; set; }
        public DateTime fechaAlta { get; set; }
        public string mun { get; set; }
        public string edo{ get; set; }
    }
}