namespace TestTask.Domain.DTO.OrderDTO;

public record OrderResponseDTO(Guid OrderId, Guid ProductId, DateTime OrderDateTime);