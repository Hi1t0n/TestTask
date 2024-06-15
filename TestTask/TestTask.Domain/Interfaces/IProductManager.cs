using CSharpFunctionalExtensions;
using TestTask.Domain.DTO.ProductDTO;
using TestTask.Domain.Models;

namespace TestTask.Domain.Interfaces;

public interface IProductManager
{
    Task<Result<ProductResponseDTO, IError>> AddProductAsync(AddProductRequestDTO request);
    Task<Result<ProductResponseDTO, IError>> GetProductByIdAsync(Guid id);
    Task<Result<List<ProductResponseDTO>, IError>> GetAllProductAsync();
    Task<Result<ProductResponseDTO, IError>> UpdateProductByIdAsync(UpdateProductRequestDTO request);
    Task<Result<ProductResponseDTO, IError>> DeleteProductById(Guid id);
}