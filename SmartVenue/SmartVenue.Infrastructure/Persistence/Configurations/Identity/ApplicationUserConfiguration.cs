using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartVenue.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartVenue.Infrastructure.Persistence.Configurations.Identity
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> b)
        {
            b.Property(x => x.FullName).IsRequired().HasMaxLength(150);
        }
    }
}
