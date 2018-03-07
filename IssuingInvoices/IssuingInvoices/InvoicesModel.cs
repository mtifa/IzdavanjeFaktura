namespace IssuingInvoices
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using IssuingInvoices.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Threading.Tasks;
    using System.Security.Claims;
    using Microsoft.AspNet.Identity;
    using IssuingInvoices.Models;

    public partial class InvoicesModel : IdentityDbContext<ApplicationUser>
    {
        public InvoicesModel()
            : base("name=InvoicesModel")
        {
        }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Configurations.Add(new IdentityUserLoginConfiguration());
            modelBuilder.Configurations.Add(new IdentityUserRoleConfiguration());

        }
    }
}
