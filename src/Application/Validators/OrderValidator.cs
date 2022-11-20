using Domain.Models;

namespace Application.Validators;

public static class OrderValidator
{
    public static void Validate(Order order)
    {
        if (!order.Books.Any())
        {
            throw new ArgumentException("The field Books must be a string or array type with a minimum length of '1'");
        }

        foreach (Book book in order.Books)
        {
            if (book.Id < 1)
            {
                throw new ArgumentException("The field Id must be minimum 1.");
            }

            if (book.Count < 1)
            {
                throw new ArgumentException("The field Count must be minimum 1");
            }
        }
    }
}