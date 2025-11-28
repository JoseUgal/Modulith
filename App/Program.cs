using App;
using Infrastructure.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.InstallServicesFromAssemblies(
    builder.Configuration,
    AssemblyReference.Assembly,
    Infrastructure.AssemblyReference.Assembly
);

builder.Services.InstallModulesFromAssemblies(
    builder.Configuration,
    Modules.Users.Infrastructure.AssemblyReference.Assembly
);

WebApplication app = builder.Build();

app.UseHttpsRedirection();

app.Run();
