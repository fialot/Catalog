//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Katalog
{
    using System;
    using System.Collections.Generic;
    
    public partial class Items
    {
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Subcategory { get; set; }
        public string Subcategory2 { get; set; }
        public string Keywords { get; set; }
        public string Note { get; set; }
        public Nullable<short> Count { get; set; }
        public Nullable<System.DateTime> AcqDate { get; set; }
        public Nullable<double> Price { get; set; }
        public string InvNumber { get; set; }
        public string Location { get; set; }
        public Nullable<short> FastTags { get; set; }
        public byte[] Image { get; set; }
        public Nullable<bool> Excluded { get; set; }
    }
}