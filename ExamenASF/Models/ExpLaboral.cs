using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamenASF.Models {
    public class ExpLaboral {
        public Nullable<long> LlaveExpLab { get; set; }
        public long LlaveUsuario { get; set; }
        public string puesto { get; set; }
        public string funciones { get; set; }
        public string empresa { get; set; }
        public Nullable<int> noEmpleados { get; set; }
        public string fechaInicio { get; set; }
        public string fechaFin { get; set; }
        public Nullable<long> retMensBruta { get; set; }
        public Nullable<long> retMensNeta { get; set; }
        public DateTime fechaAlta { get; set; }
        public string pais { get; set; }
        public Nullable<long> LlaveDatoPersonal { get; set; }
    }
}