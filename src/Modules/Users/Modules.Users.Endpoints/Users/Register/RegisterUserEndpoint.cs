using Domain.Results;
using Endpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Modules.Users.Application.Users.Register;

namespace Modules.Users.Endpoints.Users.Register;

public sealed class RegisterUserEndpoint(ISender sender) : Endpoint
{
    [HttpPost("/users/register")]
    public async Task<ActionResult<Guid>> HandleAsync(
        [FromBody] RegisterUserRequest request, 
        CancellationToken cancellationToken
    )
    {
        var command = new RegisterUserCommand(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password
        );
        
        Result<Guid> result = await sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        
        return Ok(result.Value);
    }
}
