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
    
    public partial class Tahliller
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tahliller()
        {
            this.HastaTahlils = new HashSet<HastaTahlil>();
        }
    
        public int TahlilID { get; set; }
        public string TahlilTur { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HastaTahlil> HastaTahlils { get; set; }
    }
}
