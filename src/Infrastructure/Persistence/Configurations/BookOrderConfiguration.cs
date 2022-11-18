using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class BookOrderConfiguration : IEntityTypeConfiguration<BookOrder>
{
    public void Configure(EntityTypeBuilder<BookOrder> builder)
    {
        builder
            .HasKey(bookOrder => new { bookOrder.BookId, bookOrder.OrderId });

        builder
            .HasOne(bookOrder => bookOrder.Book)
            .WithMany(book => book.BookOrders)
            .HasForeignKey(bookOrder => bookOrder.BookId);

        builder
            .HasOne(bookOrder => bookOrder.Order)
            .WithMany(order => order.BookOrders)
            .HasForeignKey(bookOrder => bookOrder.OrderId);
    }
}