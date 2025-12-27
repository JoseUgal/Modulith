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

    public void SetupSqlReturnsTenant(TenantResponse response)
    {
        Sql.Setup(x =>
            x.FirstOrDefaultAsync<TenantResponse>(It.IsAny<string>(), It.IsAny<object?>())
        ).ReturnsAsync(response);
    }

    public void SetupSqlReturnsNull()
    {
        Sql.Setup(x =>
            x.FirstOrDefaultAsync<TenantResponse>(It.IsAny<string>(), It.IsAny<object?>())
        ).ReturnsAsync((TenantResponse?)null);
    }

    public void VerifySqlQueryWasCalled()
    {
        Sql.Verify(x =>
            x.FirstOrDefaultAsync<TenantResponse>(It.IsAny<string>(), It.IsAny<object?>()),
            Times.Once
        );
    }

    public TenantResponse ValidResponse(Guid? tenantId = null, string? name = null, string? slug = null) => new()
    {
        Id = tenantId ?? Guid.NewGuid(),
        Name = name ?? "Demo",
        Slug = slug ?? "demo"
    };
}
