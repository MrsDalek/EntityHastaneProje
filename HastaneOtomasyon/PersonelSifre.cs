//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HastaneOtomasyon
{
    using System;
    using System.Collections.Generic;
    
    public partial class PersonelSifre
    {
        public int PSifreID { get; set; }
        public string Sifre { get; set; }
        public bool Durum { get; set; }
    
        public virtual Personeller Personeller1 { get; set; }
    }
}
