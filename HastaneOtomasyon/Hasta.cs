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
    
    public partial class Hasta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Hasta()
        {
            this.HastaTahlils = new HashSet<HastaTahlil>();
            this.Muayenes = new HashSet<Muayene>();
        }
    
        public int HastaID { get; set; }
        public string HastaAdi { get; set; }
        public string HastaSoyAdi { get; set; }
        public string Tc_Passaport { get; set; }
        public Nullable<bool> durum { get; set; }
        public string HastaKanGrubu { get; set; }
        public string HastaBoy { get; set; }
        public string HastaKilo { get; set; }
        public string HastaAdres { get; set; }
        public string HastaTel { get; set; }
        public Nullable<int> HastaYas { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HastaTahlil> HastaTahlils { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Muayene> Muayenes { get; set; }
    }
}
