using TestTask.Domain.DTO.OrderDTO;
using TestTask.Domain.Interfaces;
using TestTask.Infrastructure.ErrorObjects;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace TestTask.API.Routing;

public static class OrderRouting
{
    public static WebApplication AddOrderRouting(this WebApplication application)
    {
        var orderGroup = application.MapGroup("/api/orders");

        orderGroup.MapPost(pattern: "/", AddOrderAsync);
        orderGroup.MapGet(pattern: "/{id:guid}", handler: GetOrderByIdAsync);
        orderGroup.MapGet(pattern: "/", handler: GetAllOrderAsync);
        orderGroup.MapDelete(pattern: "/{id:guid}", handler: DeleteOrderByIdAsync);
        
        return application;
    }

    public static async Task<IResult> AddOrderAsync(AddOrderRequestDTO request, IOrderManager manager)
    {
        var result = await manager.AddOrderAsync(request);

        return Results.Ok(result.Value);
    }

    public static async Task<IResult> GetOrderByIdAsync(Guid id, IOrderManager manager)
    {
        var result = await manager.GetOrderByIdAsync(id);

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

    public static async Task<IResult> GetAllOrderAsync(IOrderManager manager)
    {
        var result = await manager.GetAllOrderAsync();
        
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

    public static async Task<IResult> DeleteOrderByIdAsync(Guid id, IOrderManager manager)
    {
        var result = await manager.DeleteOrderByIdAsync(id);
        
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