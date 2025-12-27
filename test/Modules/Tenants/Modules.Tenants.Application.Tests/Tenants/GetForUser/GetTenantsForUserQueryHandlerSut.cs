using Application.Data;
using Modules.Tenants.Application.Tenants.GetForUser;
using Moq;

namespace Modules.Tenants.Application.Tests.Tenants.GetForUser;

internal sealed class GetTenantsForUserQueryHandlerSut
{
    public Mock<ISqlQueryExecutor> Sql { get; } = new(MockBehavior.Strict);

    public GetTenantsForUserQueryHandler Handler { get; }

    public GetTenantsForUserQueryHandlerSut()
    {
        Handler = new GetTenantsForUserQueryHandler(Sql.Object);
    }

    public void SetupSqlReturnsEmptyArray()
    {
        Sql.Setup(x =>
            x.QueryAsync<TenantResponse>(It.IsAny<string>(), It.IsAny<object?>())
        ).ReturnsAsync([]);
    }

    public void SetupSqlReturnsTenants(TenantResponse[] responses)
    {
        Sql.Setup(x =>
            x.QueryAsync<TenantResponse>(It.IsAny<string>(), It.IsAny<object?>())
        ).ReturnsAsync(responses);
    }

    public TenantResponse ValidResponse(string name, string slug) => new()
    {
        Id = Guid.NewGuid(),
        Name = name,
        Slug = slug
    };

    public void VerifySqlQueryWasCalled()
    {
        Sql.Verify(x =>
            x.QueryAsync<TenantResponse>(It.IsAny<string>(), It.IsAny<object?>()),
            Times.Once
        );
    }
}
