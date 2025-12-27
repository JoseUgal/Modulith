using Application.Data;
using Modules.Tenants.Application.Tenants.GetById;
using Moq;

namespace Modules.Tenants.Application.Tests.Tenants.GetById;

internal sealed class GetTenantByIdQueryHandlerSut
{
    public Mock<ISqlQueryExecutor> Sql { get; } = new(MockBehavior.Strict);

    public GetTenantByIdQueryHandler Handler { get; }

    public GetTenantByIdQueryHandlerSut()
    {
        Handler = new GetTenantByIdQueryHandler(Sql.Object);
    }
}
