using CSharpFunctionalExtensions;
using TestTask.Domain.DTO.OrderDTO;

namespace TestTask.Domain.Interfaces;

public interface IOrderManager
{
    Task<Result<OrderResponseDTO>> AddOrderAsync(AddOrderRequestDTO request);
    Task<Result<OrderGetResponseDTO, IError>> GetOrderByIdAsync(Guid id);
    Task<Result<List<OrderGetResponseDTO>, IError>> GetAllOrderAsync();
    Task<Result<OrderResponseDTO, IError>> DeleteOrderByIdAsync(Guid id);
}