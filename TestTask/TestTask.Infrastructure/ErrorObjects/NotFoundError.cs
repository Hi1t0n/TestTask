using TestTask.Domain.Interfaces;

namespace TestTask.Infrastructure.ErrorObjects;

public class NotFoundError(string errorMessage) : IError
{
    public string ErrorMessage { get; } = errorMessage;
}