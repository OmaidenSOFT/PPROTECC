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
    
    public partial class Encabezado
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Encabezado()
        {
            this.EncabezadoFactor = new HashSet<EncabezadoFactor>();
            this.FactorElemento = new HashSet<FactorElemento>();
            this.Inspeccion_Elementos = new HashSet<Inspeccion_Elementos>();
        }
    
        public int id { get; set; }
        public string RFID { get; set; }
        public string Serial { get; set; }
        public string Modelo { get; set; }
        public string Material { get; set; }
        public Nullable<System.DateTime> FechaFabricacion { get; set; }
        public string Marca { get; set; }
        public string Lote { get; set; }
        public Nullable<int> ElementoID { get; set; }
        public Nullable<int> Sedeid { get; set; }
        public string Ubicacion { get; set; }
        public string NroFoto { get; set; }
        public Nullable<System.DateTime> FechaCompra { get; set; }
        public string Observaciones { get; set; }
        public string RutaHojadeVida { get; set; }
        public string TAG { get; set; }
    
        public virtual Elemento Elemento { get; set; }
        public virtual Sede Sede { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EncabezadoFactor> EncabezadoFactor { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FactorElemento> FactorElemento { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Inspeccion_Elementos> Inspeccion_Elementos { get; set; }
    }
}
