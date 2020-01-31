using IGRMgr.Modules.Administration.Domain.Staffs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.Administration.Infrastructure.Staffs
{
    public class StaffEntityTypeConfiguration : IEntityTypeConfiguration<Staff>
    {
        public void Configure(EntityTypeBuilder<Staff> builder)
        {
            builder.ToTable("Staffs", "administration");
            builder.HasKey(s => s.Id);

            builder.Property<string>("_firstName").HasColumnName("FirstName").HasMaxLength(100).IsRequired();
            builder.Property<string>("_lastName").HasColumnName("LastName").HasMaxLength(100).IsRequired();
            builder.Property<string>("_middleName").HasColumnName("MiddleName").HasMaxLength(100);
            builder.Property<string>("_name").HasColumnName("FullName").HasMaxLength(100).IsRequired();
            builder.Property<string>("_email").HasColumnName("EmailAddress").HasMaxLength(255).IsRequired();
            builder.Property<string>("_phoneNumber").HasColumnName("PhoneNumber").HasMaxLength(15);
            builder.Property<string>("_staffNo").HasColumnName("StaffNumber").HasMaxLength(50);
            builder.OwnsOne<StaffGender>("_gender", g =>
            {
                g.Property(p => p.Value).HasColumnName("Gender").HasMaxLength(50);
            });
            builder.Property<DateTime?>("_dateOfBirth").HasColumnName("DateOfBirth").HasColumnType("date");
            builder.Property<string>("_jobTitle").HasColumnName("JobTitle").HasMaxLength(50);
            builder.Property<bool>("_isActive").HasColumnName("IsActive").IsRequired();
            builder.Property<DateTime>("_createDate").HasColumnName("CreateDate").IsRequired();
        }
    }
}
