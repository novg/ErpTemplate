using Domain.Models;

namespace Application.Interfaces;

public interface IFileReader
{
    Task<Order> Read(Stream stream);
}