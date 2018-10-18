using System;
using System.Collections.Generic;
using System.Text;
using iBudget.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace iBudget.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<FinancialAnalyst> FinancialAnalysts { get; set; }
        public DbSet<iBudget.Models.Ad> Ad { get; set; }
    }
}
