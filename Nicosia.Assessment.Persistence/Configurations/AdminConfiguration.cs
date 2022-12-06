using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nicosia.Assessment.Domain.Models.User;

namespace Nicosia.Assessment.Persistence.Configurations
{
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.Property(e => e.AdminId).IsRequired().ValueGeneratedOnAdd();
            builder.HasKey(e => e.AdminId);
            builder.Property(e => e.Firstname).IsRequired();
            builder.Property(e => e.Lastname).IsRequired();
            builder.Property(e => e.DateOfBirth).IsRequired();
            builder.Property(e => e.Email).IsRequired();
            builder.Property(e => e.PhoneNumber).IsRequired();
            builder.Property(e => e.Password).IsRequired();

            builder.HasMany(e => e.RefreshTokens)
                .WithOne(e => e.Admin);
        }
    }
}
