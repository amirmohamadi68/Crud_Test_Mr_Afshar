using Mc2.CrudTest.Domain.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Persistanse.Config
{
    internal class CustomerEntityConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
           builder.HasKey(c=>c.Id);
            builder.Property(c=>c.Id).HasConversion(
                CustomerId => CustomerId.CId,
                value=> new CustomerId(value));
            builder.OwnsOne(customer => customer.FirstName, firstNameBuilder =>
            {
                firstNameBuilder.WithOwner();

                firstNameBuilder.Property(firstName => firstName.Value)
                    .HasColumnName(nameof(Customer.FirstName))
                    .HasMaxLength(FirstName.Length)
                    .IsRequired();

            });
            builder.OwnsOne(customer => customer.LastName, lastNameBuilder =>
            {
                lastNameBuilder.WithOwner();

                lastNameBuilder.Property(lastName => lastName.Value)
                    .HasColumnName(nameof(Customer.LastName))
                    .HasMaxLength(LastName.Length)
                    .IsRequired();
            });
            builder.OwnsOne(customer => customer.Email, emailBuilder =>
            {
                emailBuilder.WithOwner();

                emailBuilder.Property(email => email.Value)
                    .HasColumnName(nameof(Customer.Email))
                    .HasMaxLength(Email.MaxLength)
                    
                    .IsRequired();
            });
            builder.OwnsOne(customer => customer. BankAccountNumber, bankAccountNumberBuilder =>
            {
                bankAccountNumberBuilder.WithOwner();

                bankAccountNumberBuilder.Property(BankAccountNumber => BankAccountNumber.Value)
                    .HasColumnName(nameof(Customer.BankAccountNumber))
                    .IsRequired();
            });
            builder.OwnsOne(customer => customer.DateOfBirth, emailBuilder =>
            {
                emailBuilder.WithOwner();

                emailBuilder.Property(DateOfBirth => DateOfBirth.Value)
                    .HasColumnName(nameof(Customer.DateOfBirth))
                    .HasMaxLength(10)
                    .IsRequired();
            });
            builder.OwnsOne(customer => customer.PhoneNumber, phonelBuilder =>
            {
                phonelBuilder.WithOwner();

                phonelBuilder.Property(PhoneNumber => PhoneNumber.PhoneValue)
                    .HasColumnName(nameof(Customer.PhoneNumber))
                    .HasMaxLength(13)

                    .IsRequired();
            });
            builder.Property<string>("FullName")
                .HasComputedColumnSql("CONCAT(FirstName, '-', LastName, '-', DateOfBirth)")
                .HasColumnName("FullName")
                .ValueGeneratedOnAddOrUpdate()
                .HasMaxLength(255)
            .IsRequired();
            // Create a unique index on the computed column
            builder.HasIndex("FullName").IsUnique();
           




        }
    }
}
