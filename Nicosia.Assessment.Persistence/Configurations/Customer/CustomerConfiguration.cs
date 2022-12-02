using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nicosia.Assessment.Persistence.Configurations.Customer
{
    internal class CustomerConfiguration : IEntityTypeConfiguration<Nicosia.Assessment.Domain.Models.Customer.Customer>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.Customer.Customer> builder)
        {
            builder.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Firstname).IsRequired();
            builder.Property(e => e.Lastname).IsRequired();
            builder.Property(e => e.Email).IsRequired();
            builder.Property(e => e.PhoneNumber).IsRequired();
            builder.Property(e => e.BankAccountNumber).IsRequired();
            builder.Property(e => e.DateOfBirth).IsRequired();
        }
    }
}
