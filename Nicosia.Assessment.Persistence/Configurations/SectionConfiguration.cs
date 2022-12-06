using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nicosia.Assessment.Domain.Models.Section;

namespace Nicosia.Assessment.Persistence.Configurations
{
    public class SectionConfiguration : IEntityTypeConfiguration<Section>
    {
        public void Configure(EntityTypeBuilder<Section> builder)
        {
            builder.Property(e => e.SectionId).IsRequired().ValueGeneratedOnAdd();
            builder.HasKey(e => e.SectionId);
            builder.Property(e => e.Number).IsRequired();
            builder.Property(e => e.Details);
            builder.Property(e => e.CourseId).IsRequired();
            builder.Property(e => e.PeriodId).IsRequired();

            builder.HasOne(e => e.Course)
                .WithMany(e => e.Sections)
                .IsRequired();

            builder.HasOne(e => e.Period)
                .WithMany(e => e.Sections)
                .IsRequired();
            
            builder.HasMany(e => e.Lecturers)
                .WithMany(e => e.Sections);


            builder.HasMany(e => e.Students)
                .WithMany(e => e.Sections);
        }
    }
}
