//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Empleado
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Identificacion { get; set; }
        public Nullable<int> Sedeid { get; set; }
        public Nullable<int> CargoID { get; set; }
    
        public virtual Cargo Cargo { get; set; }
        public virtual Sede Sede { get; set; }
    }
}
