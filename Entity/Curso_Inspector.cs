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
    
    public partial class Curso_Inspector
    {
        public int id { get; set; }
        public Nullable<int> Cursoid { get; set; }
        public Nullable<int> Inspectorid { get; set; }
    
        public virtual Curso_Inspector Curso_Inspector1 { get; set; }
        public virtual Curso_Inspector Curso_Inspector2 { get; set; }
    }
}
