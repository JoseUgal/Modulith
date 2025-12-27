using Domain.Results;
using Endpoints.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modules.Tenants.Application.Tenants.GetById;
using Swashbuckle.AspNetCore.Annotations;
using Endpoint = Endpoints.Endpoint;

namespace Modules.Tenants.Endpoints.Tenants.GetById;

public sealed class GetTenantByIdEndpoint(ISender sender) : Endpoint
{
    [HttpGet(TenantRoutes.GetById, Name = nameof(GetTenantByIdEndpoint))]
    [ProducesResponseType(typeof(TenantResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(
        Summary = "Get tenant by id",
        Description = "Retrieves a tenant by its identifier.",
        Tags = [TenantRoutes.Tag]
    )]
    public async Task<ActionResult> HandleAsync(Guid tenantId, CancellationToken cancellationToken = default)
    {
        var query = new GetTenantByIdQuery(tenantId);
        
         Result<TenantResponse> result = await sender.Send(query, cancellationToken);
        
        return result.IsFailure ? this.HandleFailure(result) : Ok(result.Value);
    }
}
