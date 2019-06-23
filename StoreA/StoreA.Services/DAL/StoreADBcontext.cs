using System;
using System.Collections.Generic;
using System.Data.Entity;
using StoeA.Models.Models;

namespace StoreA.Services.DAL
{
    public class StoreADBcontext : DbContext
    {
        public StoreADBcontext() : base("name = StoreADBcontext")
            {

            }
        public DbSet<Products> Product { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Company> Companies { get; set; }
       

    }
}