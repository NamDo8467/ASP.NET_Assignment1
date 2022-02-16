﻿using GBCSports.Models;
using Microsoft.EntityFrameworkCore;

namespace GBCSports.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Incident> Incidents { get; set; }

        public DbSet<Customer> Customers { get; set; }
        
    }
}
