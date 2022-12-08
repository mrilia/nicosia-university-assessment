using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nicosia.Assessment.Domain.Models.ApprovalRequests;

namespace Nicosia.Assessment.Persistence.Configurations
{
    public class ApprovalRequestConfiguration : IEntityTypeConfiguration<ApprovalRequest>
    {
        public void Configure(EntityTypeBuilder<ApprovalRequest> builder)
        {
            builder.Property(e => e.ApprovalRequestId).IsRequired().ValueGeneratedOnAdd();
            builder.HasKey(e => e.ApprovalRequestId);
            builder.Property(e => e.SectionId).IsRequired();
            builder.Property(e => e.StudentId).IsRequired();
            builder.Property(e => e.Status).IsRequired();
            builder.Property(e => e.Details);
            builder.Property(e => e.LecturerId);
            builder.Property(e => e.LastChange);

            builder.HasOne(e => e.Student)
                .WithMany(e => e.ApprovalRequests);

            builder.HasOne(e => e.Section);
            builder.HasOne(e => e.Lecturer)
                .WithMany(e=>e.ApprovalRequests);
        }
    }
}
