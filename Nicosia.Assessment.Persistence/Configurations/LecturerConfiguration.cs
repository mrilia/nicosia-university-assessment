using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nicosia.Assessment.Domain.Models.User;

namespace Nicosia.Assessment.Persistence.Configurations
{
    public class LecturerConfiguration : IEntityTypeConfiguration<Lecturer>
    {
        public void Configure(EntityTypeBuilder<Lecturer> builder)
        {
            builder.Property(e => e.LecturerId).IsRequired().ValueGeneratedOnAdd();
            builder.HasKey(e => e.LecturerId);
            builder.Property(e => e.Firstname).IsRequired();
            builder.Property(e => e.Lastname).IsRequired();
            builder.Property(e => e.DateOfBirth).IsRequired();
            builder.Property(e => e.Email).IsRequired();
            builder.Property(e => e.PhoneNumber).IsRequired();
            builder.Property(e => e.SocialInsuranceNumber);
            builder.Property(e => e.Password).IsRequired();

            builder.HasMany(e => e.RefreshTokens)
                .WithOne(e => e.Lecturer);
        }
    }
}
