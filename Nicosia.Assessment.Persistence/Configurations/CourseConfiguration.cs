using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nicosia.Assessment.Domain.Models.Course;

namespace Nicosia.Assessment.Persistence.Configurations
{
    internal class CourserConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.Property(e => e.CourseId).IsRequired().ValueGeneratedOnAdd();
            builder.HasKey(e => e.CourseId);
            builder.Property(e => e.Code).IsRequired();
            builder.Property(e => e.Title).IsRequired();
        }
    }
}
