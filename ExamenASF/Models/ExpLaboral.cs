using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamenASF.Models {
    public class ExpLaboral {
        public long LlaveExpLab { get; set; }
        public long LlaveUsuario { get; set; }
        public string puesto { get; set; }
        public string funciones { get; set; }
        public string empresa { get; set; }
        public int noEmpleados { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }
        public long retMensBruta { get; set; }
        public long retMensNeta { get; set; }
        public DateTime fechaAlta { get; set; }
        public string pais { get; set; }
        public long LlaveDatoPersonal { get; set; }
    }
}