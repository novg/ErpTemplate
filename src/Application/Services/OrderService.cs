using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Models;
using Application.Validators;
using AutoMapper;
using Domain.Enums;
using Domain.Models;

namespace Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;
    private readonly IFileReaderFactory _readerFactory;
    private readonly IMapper _mapper;

    public OrderService(IOrderRepository repository, IFileReaderFactory readerFactory, IMapper mapper)
    {
        _repository = repository;
        _readerFactory = readerFactory;
        _mapper = mapper;
    }

    public async Task<OrderDto> CreateOrder(OrderInput input, ClientType? clientType)
    {
        Order newOrder = _mapper.Map<Order>(input);
        newOrder.Number = $"{DateTime.UtcNow::yyyyMMdd-HHmmss-fffffff}";
        newOrder.ClientType = clientType ?? ClientType.Api;

        Order order = await _repository.CreateOrder(newOrder);
        return _mapper.Map<OrderDto>(order);
    }

    public async Task<OrderDto> CreateOrderFromFile(string fileName, Stream stream, ClientType? clientType)
    {
        IFileReader reader = _readerFactory.GetFileReader(Path.GetExtension(fileName));
        Order newOrder = await reader.Read(stream);
        newOrder.Number = $"{DateTime.UtcNow::yyyyMMdd-HHmmss-fffffff}";
        OrderValidator.Validate(newOrder);

        Order order = await _repository.CreateOrder(newOrder);
        return _mapper.Map<OrderDto>(order);
    }

    public async Task<OrderDto> GetOrderById(int orderId)
    {
        Order order = await _repository.GetOrderById(orderId);
        return _mapper.Map<OrderDto>(order);
    }

    public async Task<IEnumerable<OrderDto>> GetOrders()
    {
        IEnumerable<Order> orders = await _repository.GetOrders();
        return _mapper.Map<IEnumerable<OrderDto>>(orders);
    }
}