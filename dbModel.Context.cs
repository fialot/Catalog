﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class databaseEntities : DbContext
    {
        public databaseEntities()
            : base("name=databaseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Boardgames> Boardgames { get; set; }
        public virtual DbSet<Books> Books { get; set; }
        public virtual DbSet<Borrowing> Borrowing { get; set; }
        public virtual DbSet<Copies> Copies { get; set; }
        public virtual DbSet<Foto> Foto { get; set; }
        public virtual DbSet<Items> Items { get; set; }
        public virtual DbSet<Lending> Lending { get; set; }
        public virtual DbSet<Video> Video { get; set; }
        public virtual DbSet<Contacts> Contacts { get; set; }
        public virtual DbSet<Games> Games { get; set; }
        public virtual DbSet<Objects> Objects { get; set; }
        public virtual DbSet<Recipes> Recipes { get; set; }
    }
}
