using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http.HttpResults;
using TestTask.Domain.DTO.ProductDTO;
using TestTask.Domain.Interfaces;
using TestTask.Infrastructure.ErrorObjects;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace TestTask.API.Routing;

public static class ProductRouting
{
    public static WebApplication AddProductRouting(this WebApplication application)
    {
        var productGroup = application.MapGroup("/api/products");

        productGroup.MapPost(pattern: "/", handler: AddProductAsync);
        productGroup.MapGet(pattern: "/{id:guid}", handler: GetProductByIdAsync);
        productGroup.MapGet(pattern: "/", handler: GetAllProductAsync);
        productGroup.MapPut(pattern: "/", handler: UpdateProductByIdAsync);
        productGroup.MapDelete(pattern: "/{id:guid}", handler: DeleteProductByIdAsync);
        
        return application;
    }

    public static async Task<IResult> AddProductAsync(AddProductRequestDTO request, IProductManager manager)
    {
        var result = await manager.AddProductAsync(request);

        if (result.IsFailure)
        {
            switch (result.Error)
            {
                case BadRequestError error:
                    return Results.BadRequest(new
                    {
                        errorMessage = error.ErrorMessage
                    });
            }
        }

        return Results.Ok(result.Value);
    }

    public static async Task<IResult> GetProductByIdAsync(Guid id, IProductManager manager)
    {
        var result = await manager.GetProductByIdAsync(id);

        if (result.IsFailure)
        {
            switch (result.Error)
            {
                case NotFoundError error:
                    return Results.NotFound(new
                    {
                        errorMessage = error.ErrorMessage
                    });
            }
        }

        return Results.Ok(result.Value);
    }

    public static async Task<IResult> GetAllProductAsync(IProductManager manager)
    {
        var result = await manager.GetAllProductAsync();

        if (result.IsFailure)
        {
            switch (result.Error)
            {
                case NotFoundError error:
                    return Results.NotFound(new
                    {
                        errorMessage = error.ErrorMessage
                    });
            }
        }
        
        return Results.Ok(result.Value);
    }

    public static async Task<IResult> UpdateProductByIdAsync(UpdateProductRequestDTO request, IProductManager manager)
    {
        var result = await manager.UpdateProductByIdAsync(request);

        if (result.IsFailure)
        {
            switch (result.Error)
            {
                case BadRequestError error:
                    return Results.BadRequest(new
                    {
                        errorMessage = error.ErrorMessage
                    });
                
                case NotFoundError error:
                    return Results.NotFound(new
                    {
                        errorMessage = error.ErrorMessage
                    });
            }
        }

        return Results.Ok(result.Value);
    }

    public static async Task<IResult> DeleteProductByIdAsync(Guid id, IProductManager manager)
    {
        var result = await manager.DeleteProductById(id);

        if (result.IsFailure)
        {
            switch (result.Error)
            {
                case NotFoundError error:
                    return Results.NotFound(new
                    {
                        errorMessage = error.ErrorMessage
                    });
            }
        }

        return Results.Ok(result.Value);
    }
}