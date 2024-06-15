using TestTask.Domain.Interfaces;

namespace TestTask.Infrastructure.ErrorObjects;

public class BadRequestError(string errorMessage) : IError
{
    public string ErrorMessage { get; } = errorMessage;
}