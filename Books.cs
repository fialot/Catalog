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
    
    public partial class Books
    {
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string OrigName { get; set; }
        public string ISBN { get; set; }
        public string InventoryNumber { get; set; }
        public Nullable<long> Count { get; set; }
        public Nullable<short> Rating { get; set; }
        public string Author { get; set; }
        public string Translator { get; set; }
        public string Illustrator { get; set; }
        public string Genre { get; set; }
        public string SubGenre { get; set; }
        public string Edition { get; set; }
        public string Type { get; set; }
        public string Series { get; set; }
        public Nullable<long> SeriesNum { get; set; }
        public string Bookbinding { get; set; }
        public Nullable<short> Year { get; set; }
        public Nullable<short> OrigYear { get; set; }
        public Nullable<short> Publication { get; set; }
        public string Language { get; set; }
        public string OrigLanguage { get; set; }
        public string Note { get; set; }
        public string Publisher { get; set; }
        public string URL { get; set; }
        public string Content { get; set; }
        public byte[] Cover { get; set; }
        public Nullable<System.DateTime> AcquisitionDate { get; set; }
        public Nullable<double> Price { get; set; }
        public string Pages { get; set; }
        public Nullable<bool> Read { get; set; }
        public string Location { get; set; }
        public string Tags { get; set; }
        public Nullable<long> FastTags { get; set; }
        public string EbookPath { get; set; }
        public string EbookType { get; set; }
        public Nullable<System.DateTime> update { get; set; }
        public string Note1 { get; set; }
        public string Note2 { get; set; }
        public string MainCharacter { get; set; }
        public Nullable<System.Guid> BorrowedPerson { get; set; }
        public Nullable<System.DateTime> BorrowedFrom { get; set; }
        public Nullable<System.DateTime> BorrowedTo { get; set; }
    }
}
