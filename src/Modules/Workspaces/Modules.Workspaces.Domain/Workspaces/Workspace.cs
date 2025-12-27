using Domain.Primitives;
using Domain.Results;
using Modules.Workspaces.Domain.WorkspaceMemberships;

namespace Modules.Workspaces.Domain.Workspaces;

public sealed class Workspace : Entity<WorkspaceId>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Workspace"/> class.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="tenantId">The tenant identifier.</param>
    /// <param name="name">The name.</param>
    private Workspace(WorkspaceId id, Guid tenantId, WorkspaceName name) : base(id)
    {
        TenantId = tenantId;
        Name = name;
    }
    
    /// <summary>
    /// Gets the tenant identifier.
    /// </summary>
    public Guid TenantId { get; private set; }
    
    /// <summary>
    /// Gets the name.
    /// </summary>
    public WorkspaceName Name { get; private set; }

    private readonly List<WorkspaceMembership> _memberships = [];

    /// <summary>
    /// Gets the workspace memberships.
    /// </summary>
    public IReadOnlyCollection<WorkspaceMembership> Memberships => _memberships.AsReadOnly();

    /// <summary>
    /// Creates a new <see cref="Workspace"/> entity.
    /// </summary>
    /// <remarks>
    /// The method generates a new <see cref="WorkspaceId"/> for the entity.
    /// All value objects should be validated in this method.
    /// </remarks>
    public static Result<Workspace> Create(Guid tenantId, WorkspaceName name, Guid ownerId)
    {
        var id = new WorkspaceId(Guid.NewGuid());

        var workspace = new Workspace(id, tenantId, name);

        Result result = workspace.AddOwner(ownerId);

        if (result.IsFailure)
        {
            return Result.Failure<Workspace>(
                result.Error
            );
        }

        return workspace;
    }

    /// <summary>
    /// Adds the first owner membership for this workspace.
    /// </summary>
    /// <param name="userId">The identifier of the user.</param>
    /// <returns>The result of the operation.</returns>
    private Result AddOwner(Guid userId)
    {
        if (_memberships.Count != 0)
        {
            return Result.Failure(
                WorkspaceErrors.OwnerAlreadyExist    
            );
        }
        
        _memberships.Add(WorkspaceMembership.CreateOwner(Id, userId));
        
        return Result.Success();
    }
}
