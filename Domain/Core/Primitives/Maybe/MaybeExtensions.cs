using AutoMapper;
using Domain.Core.Primitives.Result;

namespace Domain.Core.Primitives.Maybe
{
    /// <summary>
    /// Contains extension methods for the Maybe class.
    /// </summary>

    public static class MaybeExtensions
    {
        /// <summary>
        /// Return the Result based on the Maybe and failure prediction
        /// </summary>
        /// <typeparam name="T">The result type.</typeparam>
        /// <param name="maybeTask">THe Maybe that will be inspected for the failurePredicated</param>
        /// <param name="failurePredicate"> Predicate for the Value fails</param>
        /// <param name="error">The error for when the failurePredicated is true</param>
        /// <returns>
        /// The success result with the bound value if the current result is a success result, otherwise a failure result.
        /// </returns>
        public async static Task<Result<T>> ToResultAsync<T>(this Task<Maybe<T>> maybeTask, Func<T, bool> failurePredicate,Error error)
        {
            var maybe = await maybeTask;
            return (Result<T>)(failurePredicate(maybe.Value)
                ? Result.Result.Failure(error)
                : Result.Result.Success(maybe));
        }
        /// <summary>
        /// Ensure that the Object is not null 
        /// </summary>
        /// <typeparam name="T">The result type.</typeparam>
        /// <param name="obj">THe Maybe that will be inspected for the null</param>
        /// <param name="error">The error for when the object is null </param>
        /// <returns>
        /// The success result with the bound value if the current result is a success result, otherwise a failure result.
        /// </returns>
        public async static Task<Result<T>> EnsureNotNullAsync<T>(this T obj,Error error)
        {
            
            return obj is null
                ? Result.Result.Failure<T>(error)
                : Result.Result.Success<T>(obj);
        }

        /// <summary>
        /// Ensure that the Result is not Null
        /// </summary>
        /// <typeparam name="T">The result type.</typeparam>
        /// <param name="obj">THe Maybe that will be inspected for the null</param>
        /// <param name="resultTask"></param>
        /// <param name="error">The error for when the object is null </param>
        /// <returns>
        /// The success result with the bound value if the current result is a success result, otherwise a failure result.
        /// </returns>
        public async static Task<Result<Maybe<T>>> EnsureNotNullAsync<T>(this Task<Result<Maybe<T>>> resultTask, Error error)
        {
            var result = await resultTask;
            if (result.IsFailure)
                return result;

            return (Result<Maybe<T>>)(result.Value is null
                ? Result.Result.Failure(error)
                : result);
        }

        public async static Task<Result<Maybe<T>>> EnsureExistsAsync<T>(
            this Task<Result<Maybe<T>>> resultTask,
            Error error)
        {
            var result = await resultTask;
            if (result.IsFailure)
                return result;

            return (Result<Maybe<T>>)(result.Value.IsActivatorInstance
                ? Result.Result.Failure(error)
                : result);
        }

        public async static Task<Result<Maybe<TOut>>> BindAsync<TIn, TOut>(
            this Task<Result<Maybe<TIn>>> resultTask,
            Func<Maybe<TIn>, Task<Maybe<TOut>>> func)
        {
            var result = await resultTask;
            if (result.IsFailure)
                return (Result<Maybe<TOut>>)Result.Result.Failure(result.Error);

            var maybeOut = await func(result.Value);
            return Result.Result.Success(maybeOut);
        }

        public async static Task<Result<Maybe<T>>> ToResultAsync<T>(
            this Task<Result<Maybe<T>>> resultTask,
            Func<Maybe<T>, bool> failurePredicate,
            Error error)
        {
            var result = await resultTask;
            if (result.IsFailure)
                return result;

            return (Result<Maybe<T>>)(failurePredicate(result.Value)
                ? Result.Result.Failure(error)
                : result);
        }

        public async static Task<Result<Maybe<TOut>>> Map<TIn, TOut>(
            this Task<Result<Maybe<TIn>>> resultTask,
            Func<Maybe<TIn>, Result<Maybe<TOut>>> func)
        {
            var result = await resultTask;
            if (result.IsFailure)
                return (Result<Maybe<TOut>>)Result.Result.Failure(result.Error);

            return func(result.Value);
        }
        public async static Task<Result<T>> UnwrapAsync<T>(
        this Task<Result<Maybe<T>>> resultTask,
        Error error)
        {
            var result = await resultTask;
            if (result.IsFailure)
                return (Result<T>)Result.Result.Failure(result.Error);

            return (Result<T>)(result.Value.IsActivatorInstance
                ? Result.Result.Failure(error)
                : Result.Result.Success(result.Value.Value));
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
        public async static Task<Maybe<TOut>> Bind<TIn, TOut>(this Maybe<TIn> maybe, Func<TIn, Task<Maybe<TOut>>> func) =>
                maybe.HasValue ? await func(maybe.Value) : Maybe<TOut>.None;

        /// <summary>
        /// Matches to the corresponding functions based on existence of the value.
        /// </summary>
        /// <typeparam name="TIn">The input type.</typeparam>
        /// <typeparam name="TOut">The output type.</typeparam>
        /// <param name="resultTask">The maybe task.</param>
        /// <param name="onSuccess">The on-success function.</param>
        /// <param name="onDatabaseProblem">The Database problem function</param>
        /// <param name="onNotFound">The not found function</param>
        /// <returns>
        /// The result of the on-success function if the maybe has a value, otherwise the result of the failure result.
        /// </returns>
        public async static Task<TOut> Match<TIn, TOut>(
            this Task<Maybe<TIn>> resultTask,
            Func<TIn, TOut> onSuccess,
            Func<TOut> onDatabaseProblem,
            Func<TOut> onNotFound)
        {
            var maybe = await resultTask;

            if (maybe.Value is null)
                return onDatabaseProblem();

            if (maybe.IsActivatorInstance)
                return onNotFound();

            return maybe.HasValue ? onSuccess(maybe.Value) : onNotFound();
        }
    }
}
