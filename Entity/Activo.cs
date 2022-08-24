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
    
    public partial class Activo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Activo()
        {
            this.ActivoInspeccion = new HashSet<ActivoInspeccion>();
        }
    
        public int Id { get; set; }
        public int LineaId { get; set; }
        public string Descripcion { get; set; }
        public int TipoActivoId { get; set; }
        public int SedeId { get; set; }
    
        public virtual Linea Linea { get; set; }
        public virtual Sede Sede { get; set; }
        public virtual TipoActivo TipoActivo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActivoInspeccion> ActivoInspeccion { get; set; }
    }
}