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
    
    public partial class Copies
    {
        public System.Guid ID { get; set; }
        public string ItemType { get; set; }
        public Nullable<System.Guid> ItemID { get; set; }
        public Nullable<short> ItemNum { get; set; }
        public string InventoryNumber { get; set; }
        public Nullable<long> Barcode { get; set; }
        public string Condition { get; set; }
        public string Location { get; set; }
        public string Note { get; set; }
        public Nullable<System.DateTime> AcquisitionDate { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<bool> Excluded { get; set; }
        public Nullable<short> Status { get; set; }
    }
}
