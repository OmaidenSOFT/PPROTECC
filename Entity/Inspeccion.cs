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
    
    public partial class Inspeccion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Inspeccion()
        {
            this.Inspeccion_Elemento = new HashSet<Inspeccion_Elemento>();
            this.Inspeccion_Elementos = new HashSet<Inspeccion_Elementos>();
            this.Inspeccion_inspectores = new HashSet<Inspeccion_inspectores>();
        }
    
        public int id { get; set; }
        public int Sedeid { get; set; }
        public System.DateTime FechaInicio { get; set; }
        public System.DateTime FechaFinal { get; set; }
        public Nullable<int> Numero { get; set; }
        public Nullable<int> Ano { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Inspeccion_Elemento> Inspeccion_Elemento { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Inspeccion_Elementos> Inspeccion_Elementos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Inspeccion_inspectores> Inspeccion_inspectores { get; set; }
        public virtual Sede Sede { get; set; }
    }
}