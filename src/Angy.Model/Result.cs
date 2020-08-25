using System;
using System.Threading.Tasks;

namespace Angy.Model
{
    public class Result<TSuccess, TError>
    {
        public TSuccess Success { get; }
        public TError Error { get; }

        public Result(TSuccess success, TError error)
        {
            Success = success;
            Error = error;
        }

        public Result(TSuccess success) : this(success, error: default)
        {
        }

        public Result(TError error) : this(success: default, error)
        {
        }
    }

    public static class Result<T>
    {
        public static Result<TSuccess, T> Success<TSuccess>(TSuccess success) => new Result<TSuccess, T>(success);

        public static Result<T, TError> Error<TError>(TError error) => new Result<T, TError>(error);
    }

    public static class Result
    {
        public static bool HasError<TValue, TError>(this Result<TValue, TError> source) => source.Error != null;

        public static Result<TResult, TError> Cast<TValue, TError, TResult>(this Result<TValue, TError> source, Func<TValue, TResult> selector) =>
            source.Success != null
                ? new Result<TResult, TError>(selector(source.Success), source.Error)
                : new Result<TResult, TError>(success: default, source.Error);

        public static Task<Result<Unit, Error.Exceptional>> Try(Func<Task> func, Action<Exception> logger)
        {
            return Try(async () =>
            {
                await func();
                return Unit.Value;
            }, logger);
        }

        public static Result<T, Error.Exceptional> Try<T>(Func<T> func, Action<Exception> logger)
        {
            try
            {
                return Result<Error.Exceptional>.Success(func());
            }
            catch (Exception e)
            {
                logger(e);
                return Result<T>.Error(new Error.Exceptional(e));
            }
        }

        public static Result<Unit, Error.Exceptional> Try(Action func, Action<Exception> logger)
        {
            return Try(() =>
            {
                func();
                return Unit.Value;
            }, logger);
        }

        public static async Task<Result<T, Error.Exceptional>> Try<T>(Func<Task<T>> func, Action<Exception> logger)
        {
            try
            {
                return Result<Error.Exceptional>.Success(await func());
            }
            catch (Exception e)
            {
                logger(e);
                return Result<T>.Error(new Error.Exceptional(e));
            }
        }
    }

    public abstract class Error
    {
        public sealed class Exceptional : Error
        {
            public Exception Exception { get; }

            public Exceptional(Exception exception)
            {
                Exception = exception;
            }
        }
    }
}