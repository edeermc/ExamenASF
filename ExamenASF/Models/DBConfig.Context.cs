//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ExamenASF.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BOLSA_EXAMENEntities : DbContext
    {
        public BOLSA_EXAMENEntities()
            : base("name=BOLSA_EXAMENEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<DATOS_PERSONALES> DATOS_PERSONALES { get; set; }
        public virtual DbSet<EXP_LABORAL> EXP_LABORAL { get; set; }
        public virtual DbSet<USUARIOS> USUARIOS { get; set; }
    }
}
