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
    
    public partial class MuayeneTedavi
    {
        public int MID { get; set; }
        public int TedaviID { get; set; }
        public string ReceteAdi { get; set; }
    
        public virtual Muayene Muayene { get; set; }
        public virtual Tedavi Tedavi { get; set; }
    }
}
