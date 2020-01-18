using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AireSpringTest.Data
{
    public class EmployeeEntityTypeConfiguration
        : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");
            
            builder.HasKey(e => e.Id);


            builder.Property(e => e.EmployeeCode)
                .HasMaxLength(250).IsRequired();
            builder.HasIndex(u => u.EmployeeCode)
                .IsUnique();

            builder.Property(e => e.FirstName)
                .HasMaxLength(250).IsRequired();

            builder.Property(e => e.LastName)
                .HasMaxLength(250).IsRequired();

            builder.Property(e => e.PhoneNumber)
                .HasMaxLength(250).IsRequired();

            builder.Property(e => e.ZipCode)
                .HasMaxLength(250).IsRequired();
        }
    }
}
