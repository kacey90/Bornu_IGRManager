using IGRMgr.Modules.Administration.Domain.BusinessPartners;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.Administration.Infrastructure.BusinessPartners
{
    public class BusinessPartnerEntityTypeConfiguration : IEntityTypeConfiguration<BusinessPartner>
    {
        public void Configure(EntityTypeBuilder<BusinessPartner> builder)
        {
            builder.ToTable("BusinessPartners", "administration");
            builder.HasKey(b => b.Id);

            builder.Property<string>("_firstName").HasColumnName("FirstName").HasMaxLength(100).IsRequired();
            builder.Property<string>("_lastName").HasColumnName("LastName").HasMaxLength(100);
            builder.Property<string>("_name").HasColumnName("FullName").HasMaxLength(200);
            builder.Property<string>("_phoneNumber").HasColumnName("PhoneNumber").HasMaxLength(15);
            builder.Property<string>("_email").HasColumnName("EmailAddress").HasMaxLength(255);
            builder.OwnsOne<BusinessPartnerLocation>("_location", l =>
            {
                l.Property(p => p.Street).HasColumnName("Street").HasMaxLength(150);
                l.Property(p => p.City).HasColumnName("City").HasMaxLength(100);
                l.Property(p => p.PostalCode).HasColumnName("PostalCode").HasMaxLength(100);
                l.Property(p => p.State).HasColumnName("State").HasMaxLength(100);
            });
            builder.Property<bool>("_isActive").HasColumnName("IsActive");
            builder.Property<DateTime>("_createDate").HasColumnName("CreateDate");
            builder.Property<DateTime?>("_updateDate").HasColumnName("UpdateDate");
        }
    }
}
