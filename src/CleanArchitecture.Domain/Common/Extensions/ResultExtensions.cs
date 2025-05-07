using CleanArchitecture.Domain.Common.SharedKernel;

namespace CleanArchitecture.Domain.Common.Extensions;
public static class ResultExtensions
{
    public static async Task<Result<TOut>> MapAsync<TIn, TOut>(
        this Task<Result<TIn>> resultTask,
        Func<TIn, TOut> mapping)
    {
        Result<TIn> result = await resultTask;
        return result.Map(mapping);
    }
}
