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
    public class AppCheckHistoryFluent : IEntityTypeConfiguration<AppCheckHistory>
    {
        public void Configure(EntityTypeBuilder<AppCheckHistory> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.IsUp).IsRequired();
            builder.Property(r => r.ExecuteTime).IsRequired();

            builder.HasOne(t => t.TargetApplication).WithMany(r => r.AppCheckHistories).HasForeignKey(c => c.TargetApplication_Id);

        }
    }
}
