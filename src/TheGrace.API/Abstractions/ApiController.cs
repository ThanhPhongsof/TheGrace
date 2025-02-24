using MediatR;
using Microsoft.AspNetCore.Mvc;
using TheGrace.Contract.Abstractions.Shared;

namespace MySymptoms_Server.Presentation.Abstractions;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public abstract class ApiController : ControllerBase
{
    protected readonly ISender Sender;

    protected ApiController(ISender sender) => Sender = sender;

    //protected async Task<Result<Response.UserInfoResponse>> GetUserInfoByAuthorization()
    //{
    //    var authHeader = Request.Headers["Authorization"];

    //    var userInfo = await Sender.Send(new Query.GetUserInfoQuery(authHeader));

    //    return userInfo.IsSuccess ? userInfo
    //                              : (Result<Response.UserInfoResponse>)Result.Failure(userInfo.Error);
    //}

    protected IActionResult HandleFailure(Result result) =>
        result switch
        {
            { IsSuccess: true } => throw new InvalidOperationException(),
            IValidationResult validattionResult =>
                BadRequest(
                    CreateProblemDetails(
                        "Validation Error",
                        StatusCodes.Status400BadRequest,
                        result.Error,
                        validattionResult.Errors)),

            _ => BadRequest(
                    CreateProblemDetails(
                        "Bad Request",
                        StatusCodes.Status400BadRequest,
                        result.Error))
        };

    private static ProblemDetails CreateProblemDetails(string title, int status, Error error, Error[]? errors = null)
        => new ()
        {
            Title = title,
            Type = error.Code,
            Detail = error.Message,
            Status = status,
            Extensions = { { nameof(errors), errors } }
        };
}
