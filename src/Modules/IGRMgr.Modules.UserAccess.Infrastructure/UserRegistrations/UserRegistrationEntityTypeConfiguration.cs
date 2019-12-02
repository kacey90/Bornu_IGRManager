using IGRMgr.Modules.UserAccess.Domain.UserRegistrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.UserAccess.Infrastructure.UserRegistrations
{
    internal class UserRegistrationEntityTypeConfiguration : IEntityTypeConfiguration<UserRegistration>
    {
        public void Configure(EntityTypeBuilder<UserRegistration> builder)
        {
            builder.ToTable("UserRegistrations", "users");

            builder.HasKey(x => x.Id);

            builder.Property<string>("_email").HasColumnName("Email");
            builder.Property<string>("_firstName").HasColumnName("FirstName");
            builder.Property<string>("_lastName").HasColumnName("LastName");
            builder.Property<string>("_middleName").HasColumnName("MiddleName");
            builder.Property<string>("_name").HasColumnName("Name");
            builder.Property<string>("_phoneNumber").HasColumnName("PhoneNumber");
            builder.Property<string>("_gender").HasColumnType("Gender");
            builder.Property<DateTime>("_dateRegistered").HasColumnName("RegisterDate");
            builder.Property<DateTime?>("_dateConfirmed").HasColumnName("ConfirmedDate");

            builder.OwnsOne<UserRegistrationStatus>("_status", b =>
            {
                b.Property(x => x.Value).HasColumnName("StatusCode");
            });

            builder.OwnsMany<UserRole>("_roles", b =>
            {
                b.ToTable("UserRoles", "users");
                b.Property<UserRegistrationId>("UserRegistrationId");
                b.Property<string>("Value").HasColumnName("RoleCode");
                b.WithOwner().HasForeignKey("UserRegistrationId");
                b.HasKey("UserRegistrationId", "Value");
            });

        }
    }
}
