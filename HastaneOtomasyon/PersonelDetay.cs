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
    
    public partial class PersonelDetay
    {
        public int PDID { get; set; }
        public Nullable<int> PersonelYas { get; set; }
        public string PersonelCinsiyet { get; set; }
        public string PersonelTel { get; set; }
        public string PersonelMail { get; set; }
        public string PersonelAdres { get; set; }
        public Nullable<bool> durum { get; set; }
    
        public virtual Personeller Personeller { get; set; }
    }
}