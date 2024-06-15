namespace TestTask.Domain.DTO.ProductDTO;

public record UpdateProductRequestDTO(Guid Id, string? ProductName, string? ProductDescription);