using TestTask.API.Routing;

namespace TestTask.API.Extensions;

public static class RoutingExtensions
{
    public static WebApplication AddRouting(this WebApplication application)
    {
        application.AddProductRouting();
        application.AddOrderRouting();
        return application;
    }
}