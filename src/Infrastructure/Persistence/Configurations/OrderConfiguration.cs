using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(p => p.ClientType).HasColumnType("nvarchar(24)");
        builder.Property(p => p.Number).HasColumnType("nvarchar(50)");

        builder
            .HasMany(p => p.Books)
            .WithMany(p => p.Orders)
            .UsingEntity<BookOrder>(
                j => j
                    .HasOne(bo => bo.Book)
                    .WithMany(b => b.BookOrders)
                    .HasForeignKey(bo => bo.BookId),
                j => j
                    .HasOne(bo => bo.Order)
                    .WithMany(o => o.BookOrders)
                    .HasForeignKey(bo => bo.OrderId),
                j =>
                {
                    j.HasKey(bo => new { bo.OrderId, bo.BookId });
                }
            );
    }
}