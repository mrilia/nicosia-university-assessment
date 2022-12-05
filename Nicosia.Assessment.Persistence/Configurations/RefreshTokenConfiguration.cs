using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nicosia.Assessment.Domain.Models.Security;

namespace Nicosia.Assessment.Persistence.Configurations
{
    internal class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.Property(e => e.RefreshTokenId).IsRequired().ValueGeneratedOnAdd();
            builder.HasKey(e => e.RefreshTokenId);
            builder.Property(e => e.Token).IsRequired();
            builder.Property(e => e.Expires).IsRequired();
            builder.Property(e => e.Created);
            builder.Property(e => e.CreatedByIp);
            builder.Property(e => e.Revoked);
            builder.Property(e => e.RevokedByIp);
            builder.Property(e => e.ReplacedByToken);
            builder.Property(e => e.ReasonRevoked);
        }
    }
}
