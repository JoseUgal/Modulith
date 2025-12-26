using Modules.Tenants.Application.Tenants.Create;
using Modules.Tenants.Domain;
using Modules.Tenants.Domain.Tenants;
using Moq;

namespace Modules.Tenants.Application.Tests.Tenants.Create;

internal sealed class CreateTenantCommandHandlerSut
{
    public Mock<ITenantRepository> Repository { get; } = new(MockBehavior.Loose);
    
    public Mock<IUnitOfWork> UnitOfWorkMock { get; } = new(MockBehavior.Strict);

    public CreateTenantCommandHandler Handler { get; }

    public CreateTenantCommandHandlerSut()
    {
        Handler = new CreateTenantCommandHandler(
            Repository.Object,
            UnitOfWorkMock.Object
        );
    }

    public CreateTenantCommand ValidCommand() => new(
        Guid.NewGuid(),
        "demo",
        "demo"
    );

    public void VerifyPersistedNever()
    {
        Repository.Verify(x =>
                x.Add(It.IsAny<Tenant>()),
            Times.Never
        );
        
        UnitOfWorkMock.Verify(x =>
                x.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Never
        );
    }
    
    public void VerifyPersistedOnce()
    {
        Repository.Verify(x =>
                x.Add(It.IsAny<Tenant>()),
            Times.Once
        );
        
        UnitOfWorkMock.Verify(x =>
                x.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Once
        );
    }
}
