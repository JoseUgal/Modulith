using Domain.Results;
using FluentAssertions;
using Modules.Tenants.Application.Tenants.GetForUser;
using Moq;

namespace Modules.Tenants.Application.Tests.Tenants.GetForUser;

public sealed class GetTenantsForUserQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnEmptyArray_WhenUserHasNoTenants()
    {
        // Arrange
        var sut = new GetTenantsForUserQueryHandlerSut();
        
        var query = new GetTenantsForUserQuery(Guid.NewGuid());

        sut.SetupSqlReturnsEmptyArray();

        // Act
        Result<TenantResponse[]> result = await sut.Handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Should().BeEmpty();
        
        sut.VerifySqlQueryWasCalled();
    }

    [Fact]
    public async Task Handle_ShouldReturnTenants_WhenUserHasTenants()
    {
        // Arrange
        var sut = new GetTenantsForUserQueryHandlerSut();
        
        var query = new GetTenantsForUserQuery(Guid.NewGuid());
        
        TenantResponse[] responses =
        [
            sut.ValidResponse("Acme", "acme"),
            sut.ValidResponse("Contoso", "contoso")
        ];

        sut.SetupSqlReturnsTenants(responses);

        // Act
        Result<TenantResponse[]> result = await sut.Handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Length.Should().Be(responses.Length);
        result.Value.Select(x => x.Name).Should().Contain(["Acme", "Contoso"]);
        
        sut.VerifySqlQueryWasCalled();
    }
}
