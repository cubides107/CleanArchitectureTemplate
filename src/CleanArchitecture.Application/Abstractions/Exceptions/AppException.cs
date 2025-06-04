using CleanArchitecture.Domain.Common.SharedKernel;

namespace CleanArchitecture.Application.Abstractions.Exceptions;
public sealed class AppException : Exception
{
    public AppException(string requestName, Error? error = default, Exception? innerException = default)
        : base("Application exception", innerException)
    {
        RequestName = requestName;
        Error = error;
    }

    public string RequestName { get; }

    public Error? Error { get; }
}
