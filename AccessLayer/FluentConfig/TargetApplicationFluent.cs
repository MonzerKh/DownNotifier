using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModelsLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessLayer.FluentConfig
{
    public class TargetApplicationFluent : IEntityTypeConfiguration<TargetApplication>
    {
        public void Configure(EntityTypeBuilder<TargetApplication> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.URL).IsRequired();
            builder.Property(r => r.Name).IsRequired(false);
            builder.Property(r => r.Interval).IsRequired().HasDefaultValue(5);

            builder.HasOne(t => t.SystemUser).WithMany(r => r.TargetApplications).HasForeignKey(c => c.SystemUser_Id);


        }
    }
}
