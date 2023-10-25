namespace Domain.common;

public class Result
{
    public bool IsSuccess { get; init; }
    public string[]? Error { get; set; }

    public bool IsFailure() => !IsSuccess;
    public static Result<T> Success<T>(T value) => new(value);

    public static Result<T> Failure<T>(params string[]? error) => new(default)
    {
        Error = error,
        IsSuccess = false
    };
}

public class Result<T> : Result
{
    public T Value { get; init; }

    public Result(T value)
    {
        Value = value;
        IsSuccess = true;
    }
}

public static class ResultExtensions
{
    public static Result<T> AsSuccessResult<T>(this T obj) => Result.Success(obj);
    public static Result<T> AsFailureResult<T>(this string error) => Result.Failure<T>(error);
}