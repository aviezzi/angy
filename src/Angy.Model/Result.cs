using System;
using System.Threading.Tasks;

namespace Angy.Model
{
    public class Result<TSuccess, TError>
    {
        public bool IsValid { get; }

        public TSuccess Success { get; }
        public TError Error { get; }

        public Result(TSuccess success)
        {
            Success = success;
            IsValid = true;
        }

        public Result(TError error)
        {
            Error = error;
            IsValid = false;
        }
    }

    public static class Result<T>
    {
        public static Result<TSuccess, T> Success<TSuccess>(TSuccess success) => new Result<TSuccess, T>(success);

        public static Result<T, TError> Error<TError>(TError error) => new Result<T, TError>(error);
    }

    public static class Result
    {
        public static async Task<Result<TResult, Error.ExceptionalError>> Try<TResult>(Func<Task<TResult>> selector)
        {
            try
            {
                return Result<Error.ExceptionalError>.Success(await selector());
            }
            catch (Exception ex)
            {
                return Result<TResult>.Error(new Error.ExceptionalError(ex));
            }
        }
    }

    public abstract class Error
    {
        public sealed class ExceptionalError : Error
        {
            public Exception Exception { get; }

            public ExceptionalError(Exception exception)
            {
                Exception = exception;
            }
        }
    }
}