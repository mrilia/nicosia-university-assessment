using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nicosia.Assessment.Domain.Models.Period;

namespace Nicosia.Assessment.Persistence.Configurations
{
    public class PeriodConfiguration : IEntityTypeConfiguration<Period>
    {
        public void Configure(EntityTypeBuilder<Period> builder)
        {
            builder.Property(e => e.PeriodId).IsRequired().ValueGeneratedOnAdd();
            builder.HasKey(e => e.PeriodId);
            builder.Property(e => e.Name).IsRequired();
            builder.Property(e => e.StartDate).IsRequired();
            builder.Property(e => e.EndDate).IsRequired();
        }
    }
}
