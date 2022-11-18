using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Models;
using AutoMapper;
using Domain.Enums;
using Domain.Models;

namespace Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;
    private readonly IMapper _mapper;

    public OrderService(IOrderRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<OrderDto> CreateOrder(OrderDto input)
    {
        input.ClientType = ClientType.Api;
        Order order = await _repository.CreateOrder(_mapper.Map<Order>(input));
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