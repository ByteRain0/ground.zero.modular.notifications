using Microsoft.AspNetCore.Http;

namespace Shared.Result;

public static class ResultExtensions
{
    public static IResult ToProblemDetails(this FluentResults.Result result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException("Cannot transform a success result");
        }

        var errors = result.Errors.Select(x => x.Message);

        var titles = errors
            .Aggregate((i, j) => $"{i}, {Environment.NewLine} {j}");

        var reason = result.Reasons.Select(x => x.Message)
            .Aggregate((i, j) => $"{i}, {Environment.NewLine} {j}");


        return Results.Problem(title: titles, detail: reason);
    }

    public static IResult ToProblemDetails<T>(this FluentResults.Result<T> result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException("Cannot transform a success result");
        }

        var errors = result.Errors.Select(x => x.Message);

        var titles = errors
            .Aggregate((i, j) => $"{i}, {Environment.NewLine} {j}");

        var reason = result.Reasons.Select(x => x.Message)
            .Aggregate((i, j) => $"{i}, {Environment.NewLine} {j}");


        return Results.Problem(title: titles, detail: reason);
    }

    public static bool IsNotFoundError(this FluentResults.Result result) =>
        result.HasError(x => x.Message.ToLower().Contains("notfound"));

    public static bool IsNotFoundError<T>(this FluentResults.Result<T> result) =>
        result.HasError(x => x.Message.ToLower().Contains("notfound"));
}
