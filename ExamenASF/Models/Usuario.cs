using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamenASF.Models {
    public class Usuario {
        public long LlaveUsuario { get; set; }
        public string usuario { get; set; }
        public string pass { get; set; }
        public string email { get; set; }
        public DateTime fechaAlta { get; set; }
    }
}