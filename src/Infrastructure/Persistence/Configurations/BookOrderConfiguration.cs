using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class BookOrderConfiguration : IEntityTypeConfiguration<BookOrder>
{
    public void Configure(EntityTypeBuilder<BookOrder> builder)
    {
        builder.HasKey(p => new { p.BookId, p.OrderId });

        builder
            .HasOne(p => p.Book)
            .WithMany(p => p.OrdersLink)
            .HasForeignKey(p => p.BookId);

        builder
            .HasOne(p => p.Order)
            .WithMany(p => p.BooksLink)
            .HasForeignKey(p => p.OrderId);
    }
}