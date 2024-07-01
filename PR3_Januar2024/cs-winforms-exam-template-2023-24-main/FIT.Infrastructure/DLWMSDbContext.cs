﻿using FIT.Data;

using Microsoft.EntityFrameworkCore;

using System.Configuration;

namespace FIT.Infrastructure
{
    public class DLWMSDbContext : DbContext
    {
        private readonly string dbPutanja;

        public DLWMSDbContext()
        {
            dbPutanja = ConfigurationManager.
                ConnectionStrings["DLWMSBaza"].ConnectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(dbPutanja);
        }
    
        public DbSet<Student> Studenti { get; set; }
        public DbSet<Drzave> Drzave { get; set; }
        public DbSet<Gradovi> Gradovi { get; set; }
        public DbSet<PolozeniPredmeti> PolozeniPredmeti { get; set; }
        public DbSet<Predmet> Predmet { get; set; }

    }
}