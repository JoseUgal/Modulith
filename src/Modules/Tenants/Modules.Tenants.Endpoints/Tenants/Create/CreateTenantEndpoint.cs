using Domain.Results;
using Endpoints.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modules.Tenants.Application.Tenants.Create;
using Swashbuckle.AspNetCore.Annotations;
using Endpoint = Endpoints.Endpoint;

namespace Modules.Tenants.Endpoints.Tenants.Create;

public sealed class CreateTenantEndpoint(ISender sender) : Endpoint
{
    [HttpPost(TenantRoutes.Create)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [SwaggerOperation(
        Summary = "Create a new tenant (organization/account)",
        Description = "Creates a new tenant (organization/account) owned by the currently authenticated user.",
        Tags = [TenantRoutes.Tag]
    )]
    public async Task<ActionResult> HandleAsync(CreateTenantRequest request, CancellationToken cancellation)
    {
        // TODO: Gets the user identifier from CurrentUser.
        Guid userId = Guid.Empty;
        
        var command = new CreateTenantCommand(
            userId,
            request.Name,
            request.Slug
        );
        
        Result<Guid> result = await sender.Send(command, cancellation);

        return result.IsFailure ? this.HandleFailure(result) : NoContent();
    }
}
