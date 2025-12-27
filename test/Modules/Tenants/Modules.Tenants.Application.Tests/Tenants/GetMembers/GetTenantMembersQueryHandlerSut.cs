using Modules.Tenants.Application.Tenants.GetMembers;
using Modules.Tenants.Domain.TenantMemberships;
using Modules.Tenants.Domain.Tenants;
using Moq;

namespace Modules.Tenants.Application.Tests.Tenants.GetMembers;

internal sealed class GetTenantMembersQueryHandlerSut
{
    public Mock<ITenantRepository> Repository { get; } = new(MockBehavior.Default);

    public GetTenantMembersQueryHandler Handler { get; }

    public GetTenantMembersQueryHandlerSut()
    {
        Handler = new GetTenantMembersQueryHandler(Repository.Object);
    }

    public GetTenantMembersQuery ValidQuery() => new(Guid.NewGuid());

    public void SetupRepositoryExistsAsyncReturnsTrue()
    {
        Repository.Setup(x =>
            x.ExistsAsync(It.IsAny<TenantId>(), It.IsAny<CancellationToken>())
        ).ReturnsAsync(true);
    }

    public void SetupRepositoryExistsAsyncReturnsFalse()
    {
        Repository.Setup(x =>
            x.ExistsAsync(It.IsAny<TenantId>(), It.IsAny<CancellationToken>())
        ).ReturnsAsync(false);
    }

    public void SetupRepositoryReturnsMembers(TenantMembership[] members)
    {
        Repository.Setup(x =>
            x.GetMembersAsync(It.IsAny<TenantId>(), It.IsAny<CancellationToken>())
        ).ReturnsAsync(members);
    }

    public void VerifyRepositoryGetMembersWasCalled()
    {
        Repository.Verify(x =>
            x.GetMembersAsync(It.IsAny<TenantId>(), It.IsAny<CancellationToken>()),
            Times.Once
        );
    }

    public void SetupRepositoryReturnsEmptyMembers()
    {
        Repository.Setup(x =>
            x.GetMembersAsync(It.IsAny<TenantId>(), It.IsAny<CancellationToken>())
        ).ReturnsAsync([]);
    }

    public void VerifyRepositoryGetMembersWasNotCalled()
    {
        Repository.Verify(x =>
            x.GetMembersAsync(It.IsAny<TenantId>(), It.IsAny<CancellationToken>()),
            Times.Never
        );
    }
}
