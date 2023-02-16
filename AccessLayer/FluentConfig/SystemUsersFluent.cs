using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ModelsLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessLayer.FluentConfig
{
    public class SystemUsersFluent : IEntityTypeConfiguration<SystemUser>
    {
        public void Configure(EntityTypeBuilder<SystemUser> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.UserName).IsRequired().HasMaxLength(250);
            builder.Property(r => r.Email).HasMaxLength(250).IsRequired();
            builder.Property(r => r.PasswordHash).IsRequired();
            builder.Property(r => r.PasswordSalt).IsRequired();
        }
    }
}
