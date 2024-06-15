using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using TestTask.Domain.DTO.ProductDTO;
using TestTask.Domain.Interfaces;
using TestTask.Domain.Models;
using TestTask.Infrastructure.Context;
using TestTask.Infrastructure.ErrorObjects;

namespace TestTask.Infrastructure.Managers;

public class ProductManager : IProductManager
{
    private readonly ApplicationDbContext _context;
    
    public ProductManager(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Result<ProductResponseDTO, IError>> AddProductAsync(AddProductRequestDTO request)
    {
        if(string.IsNullOrWhiteSpace(request.ProductName))
        {
            return Result.Failure<ProductResponseDTO, IError>(new BadRequestError("Введите название"));
        }

        if (string.IsNullOrWhiteSpace(request.ProductDescription))
        {
            return Result.Failure<ProductResponseDTO, IError>(new BadRequestError("Введите описание"));
        }

        var product = new Product()
        {
            ProductId = Guid.NewGuid(),
            ProductName = request.ProductName,
            ProductDescription = request.ProductDescription
        };

        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();

        return Result.Success<ProductResponseDTO, IError>(new ProductResponseDTO(product.ProductId, product.ProductName, product.ProductDescription));
    }

    public async Task<Result<ProductResponseDTO, IError>> GetProductByIdAsync(Guid id)
    {
        var product = await _context.Products
            .Where(x=> x.ProductId == id)   
            .Select(x => new ProductResponseDTO(
                x.ProductId,
                x.ProductName,
                x.ProductDescription))
            .FirstOrDefaultAsync();

        if (product is null)
        {
            return Result.Failure<ProductResponseDTO, IError>(new NotFoundError($"Товар с Id: {id} не найден"));
        }

        return Result.Success<ProductResponseDTO, IError>(product);
    }

    public async Task<Result<List<ProductResponseDTO>, IError>> GetAllProductAsync()
    {
        var products = await _context.Products.Select(x => new ProductResponseDTO(
            x.ProductId,
            x.ProductName,
            x.ProductDescription
        )).ToListAsync();

        if (!products.Any())
        {
            return Result.Failure<List<ProductResponseDTO>, IError>(new NotFoundError("Товары отсутствуют"));
        }

        return Result.Success<List<ProductResponseDTO>, IError>(products);
    }

    public async Task<Result<ProductResponseDTO, IError>> UpdateProductByIdAsync(UpdateProductRequestDTO request)
    {
        if(string.IsNullOrWhiteSpace(request.ProductName))
        {
            return Result.Failure<ProductResponseDTO, IError>(new BadRequestError("Введите название"));
        }

        if (string.IsNullOrWhiteSpace(request.ProductDescription))
        {
            return Result.Failure<ProductResponseDTO, IError>(new BadRequestError("Введите описание"));
        }

        var product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == request.Id);

        if (product is null)
        {
            return Result.Failure<ProductResponseDTO, IError>(new NotFoundError($"Товар с Id: {request.Id} не найден"));
        }

        product.ProductName = request.ProductName;
        product.ProductDescription = request.ProductDescription;

        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        
        return Result.Success<ProductResponseDTO, IError>(new ProductResponseDTO(product.ProductId, product.ProductName, product.ProductDescription));
    }

    public async Task<Result<ProductResponseDTO, IError>> DeleteProductById(Guid id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == id);

        if (product is null)
        {
            return Result.Failure<ProductResponseDTO, IError>(new NotFoundError($"Товар с Id: {id} не найден"));
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return Result.Success<ProductResponseDTO, IError>(new ProductResponseDTO(product.ProductId, product.ProductName, product.ProductDescription));
    }
}