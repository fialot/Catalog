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
    
    public partial class Foto
    {
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> DateFrom { get; set; }
        public Nullable<System.DateTime> DateTo { get; set; }
        public string Location { get; set; }
        public string Country { get; set; }
        public string GPS { get; set; }
        public string Persons { get; set; }
        public string AlbumPath { get; set; }
        public string Tags { get; set; }
        public Nullable<long> FastTags { get; set; }
        public Nullable<System.DateTime> update { get; set; }
    }
}