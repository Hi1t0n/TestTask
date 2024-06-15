namespace TestTask.Domain.DTO.OrderDTO;

public record OrderGetResponseDTO(Guid OrderId, Guid ProductId, string ProductName, string ProductDescription, DateTime OrderDateTime);