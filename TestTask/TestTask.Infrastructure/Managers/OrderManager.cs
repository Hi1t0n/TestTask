using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using TestTask.Domain.DTO.OrderDTO;
using TestTask.Domain.Interfaces;
using TestTask.Domain.Models;
using TestTask.Infrastructure.Context;
using TestTask.Infrastructure.ErrorObjects;

namespace TestTask.Infrastructure.Managers;

public class OrderManager : IOrderManager
{
    private readonly ApplicationDbContext _context;

    public OrderManager(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Result<OrderResponseDTO>> AddOrderAsync(AddOrderRequestDTO request)
    {
        var order = new Order()
        {
            OrderId = Guid.NewGuid(),
            ProductId = request.ProductId,
            OrderDateTime = DateTime.Now
        };

        await _context.AddAsync(order);
        await _context.SaveChangesAsync();

        return Result.Success(new OrderResponseDTO(order.OrderId, order.ProductId, order.OrderDateTime));
    }

    public async Task<Result<OrderGetResponseDTO, IError>> GetOrderByIdAsync(Guid id)
    {
        var order = await _context.Orders
            .Include(x => x.Product)
            .Where(x => x.OrderId == id)
            .Select(x => new OrderGetResponseDTO
            (
                x.OrderId,
                x.ProductId,
                x.Product.ProductName,
                x.Product.ProductDescription,
                x.OrderDateTime
            )).FirstOrDefaultAsync();

        if (order is null)
        {
            return Result.Failure<OrderGetResponseDTO, IError>(new NotFoundError($"Заказ с Id: {id} не найден"));
        }

        return Result.Success<OrderGetResponseDTO, IError>(order);
    }

    public async Task<Result<List<OrderGetResponseDTO>, IError>> GetAllOrderAsync()
    {
        var orders = await _context.Orders
            .Include(x => x.Product)
            .Select(x => new OrderGetResponseDTO
            (
                x.OrderId,
                x.ProductId,
                x.Product.ProductName,
                x.Product.ProductDescription,
                x.OrderDateTime
            )).ToListAsync();

        if (!orders.Any())
        {
            return Result.Failure<List<OrderGetResponseDTO>, IError>(new NotFoundError("Заказы отсутствуют"));
        }

        return Result.Success<List<OrderGetResponseDTO>, IError>(orders);
    }

    public async Task<Result<OrderResponseDTO, IError>> DeleteOrderByIdAsync(Guid id)
    {
        var order = await _context.Orders
            .Where(x => x.OrderId == id)
            .FirstOrDefaultAsync();

        if (order is null)
        {
            return Result.Failure<OrderResponseDTO, IError>(new NotFoundError($"Заказ с Id: {id} не найден"));
        }

        _context.Remove(order);
        await _context.SaveChangesAsync();

        return Result.Success<OrderResponseDTO, IError>(new OrderResponseDTO(order.OrderId, order.ProductId, order.OrderDateTime));
    }
}