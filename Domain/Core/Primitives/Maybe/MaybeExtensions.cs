using AutoMapper;
using Domain.Core.Primitives.Result;

namespace Domain.Core.Primitives.Maybe
{
    /// <summary>
    /// Contains extension methods for the Maybe class.
    /// </summary>

    public static class MaybeExtensions
    {
        public static async Task<Result<T>> ToResultAsync<T>(
        this Task<T> maybeTask,
        Func<T, bool> failurePredicate,
        Error error)
        {
            var maybe = await maybeTask;
            return (Result<T>)(failurePredicate(maybe)
                ? Result<T>.Failure(error)
                : Result<T>.Success(maybe));
        }

        public static async Task<Result<T>> EnsureNotNullAsync<T>(
         this Task<T> maybeTask,
         Error error)
        {
            var maybe = await maybeTask;
            return (Result<T>)(maybe is null
                ? Result<T>.Failure(error)
                : Result<T>.Success(maybe));
        }

        public static async Task<Result<Maybe<T>>> EnsureNotNullAsync<T>(
            this Task<Result<Maybe<T>>> resultTask,
            Error error)
        {
            var result = await resultTask;
            if (result.IsFailure)
                return result;

            return (Result<Maybe<T>>)(result.Value is null
                ? Result<Maybe<T>>.Failure(error)
                : result);
        }

        public static async Task<Result<Maybe<T>>> EnsureExistsAsync<T>(
            this Task<Result<Maybe<T>>> resultTask,
            Error error)
        {
            var result = await resultTask;
            if (result.IsFailure)
                return result;

            return (Result<Maybe<T>>)(result.Value.IsActivatorInstance
                ? Result<Maybe<T>>.Failure(error)
                : result);
        }

        public static async Task<Result<Maybe<TOut>>> BindAsync<TIn, TOut>(
            this Task<Result<Maybe<TIn>>> resultTask,
            Func<Maybe<TIn>, Task<Maybe<TOut>>> func)
        {
            var result = await resultTask;
            if (result.IsFailure)
                return (Result<Maybe<TOut>>)Result<Maybe<TOut>>.Failure(result.Error);

            var maybeOut = await func(result.Value);
            return Result<Maybe<TOut>>.Success(maybeOut);
        }

        public static async Task<Result<Maybe<T>>> ToResultAsync<T>(
            this Task<Result<Maybe<T>>> resultTask,
            Func<Maybe<T>, bool> failurePredicate,
            Error error)
        {
            var result = await resultTask;
            if (result.IsFailure)
                return result;

            return (Result<Maybe<T>>)(failurePredicate(result.Value)
                ? Result<Maybe<T>>.Failure(error)
                : result);
        }

        public static async Task<Result<Maybe<TOut>>> Map<TIn, TOut>(
            this Task<Result<Maybe<TIn>>> resultTask,
            Func<Maybe<TIn>, Result<Maybe<TOut>>> func)
        {
            var result = await resultTask;
            if (result.IsFailure)
                return (Result<Maybe<TOut>>)Result<Maybe<TOut>>.Failure(result.Error);

            return func(result.Value);
        }
        public static async Task<Result<T>> UnwrapAsync<T>(
        this Task<Result<Maybe<T>>> resultTask,
        Error error)
        {
            var result = await resultTask;
            if (result.IsFailure)
                return (Result<T>)Result<T>.Failure(result.Error);

            return (Result<T>)(result.Value.IsActivatorInstance
                ? Result<T>.Failure(error)
                : Result<T>.Success(result.Value.Value));
        }
        
        /// <summary>
        /// Binds to the result of the function and returns it.
        /// </summary>
        /// <typeparam name="TIn">The result type.</typeparam>
        /// <typeparam name="TOut">The output result type.</typeparam>
        /// <param name="maybe">The result.</param>
        /// <param name="func">The bind function.</param>
        /// <returns>
        /// The success result with the bound value if the current result is a success result, otherwise a failure result.
        /// </returns>
        public static async Task<Maybe<TOut>> Bind<TIn, TOut>(this Maybe<TIn> maybe, Func<TIn, Task<Maybe<TOut>>> func) =>
                maybe.HasValue ? await func(maybe.Value) : Maybe<TOut>.None;

        /// <summary>
        /// Matches to the corresponding functions based on existence of the value.
        /// </summary>
        /// <typeparam name="TIn">The input type.</typeparam>
        /// <typeparam name="TOut">The output type.</typeparam>
        /// <param name="resultTask">The maybe task.</param>
        /// <param name="onSuccess">The on-success function.</param>
        /// <param name="onFailure">The on-failure function.</param>
        /// <returns>
        /// The result of the on-success function if the maybe has a value, otherwise the result of the failure result.
        /// </returns>
        public static async Task<TOut> Match<TIn, TOut>(
            this Task<Maybe<TIn>> resultTask,
            Func<TIn, TOut> onSuccess,
            Func<TOut> onDatabaseProblem,
            Func<TOut> onNotFound)
        {
            Maybe<TIn> maybe = await resultTask;

            if (maybe is null)
                return onDatabaseProblem();

            if (maybe.IsActivatorInstance)
                return onNotFound();

            return maybe.HasValue ? onSuccess(maybe.Value) : onNotFound();
        }
    }
}
