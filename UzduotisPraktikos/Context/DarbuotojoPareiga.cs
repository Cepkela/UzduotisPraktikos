//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UzduotisPraktikos.Context
{
    using System;
    using System.Collections.Generic;
    
    public partial class DarbuotojoPareiga
    {
        public int ID { get; set; }
        public Nullable<int> PareigosID { get; set; }
        public Nullable<int> DarbuotojasID { get; set; }
    
        public virtual Darbuotojai Darbuotojai { get; set; }
        public virtual Pareigo Pareigo { get; set; }
    }
}