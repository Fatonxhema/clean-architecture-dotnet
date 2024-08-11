using AutoMapper;
using Domain.Core.Primitives.Result;

namespace Application.Core.Extensions
{
    public static class MappingExtensions
    {
        public static async Task<Result<TOut>> MapAsync<TIn, TOut>(
        this Task<Result<TIn>> resultTask,
        IMapper mapper)
        {
            var result = await resultTask;
            if (result.IsFailure)
                return (Result<TOut>)Result<TOut>.Failure(result.Error);

            var mapped = mapper.Map<TOut>(result.Value);
            return Result<TOut>.Success(mapped);
        }
    }
}
