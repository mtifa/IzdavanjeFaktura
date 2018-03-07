using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.ModelConfiguration;

namespace IssuingInvoices
{
    public class IdentityUserLoginConfiguration : EntityTypeConfiguration<IdentityUserLogin>
    {
        public IdentityUserLoginConfiguration()
        {
            HasKey(iul => iul.UserId);
        }

    }
}