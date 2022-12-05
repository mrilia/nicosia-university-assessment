using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nicosia.Assessment.Domain.Models.User;

namespace Nicosia.Assessment.Persistence.Configurations
{
    internal class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(e => e.StudentId).IsRequired().ValueGeneratedOnAdd();
            builder.HasKey(e => e.StudentId);
            builder.Property(e => e.Firstname).IsRequired();
            builder.Property(e => e.Lastname).IsRequired();
            builder.Property(e => e.DateOfBirth).IsRequired();
            builder.Property(e => e.Email).IsRequired();
            builder.Property(e => e.PhoneNumber).IsRequired();
            builder.Property(e => e.Password).IsRequired();

            builder.HasMany(e => e.RefreshTokens)
                .WithOne(e => e.Student);
        }
    }
}
