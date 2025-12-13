using Infrastructure.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.InstallServicesFromAssemblies(
    builder.Configuration,
    App.AssemblyReference.Assembly,
    Persistence.AssemblyReference.Assembly
);

builder.Services.InstallModulesFromAssemblies(
    builder.Configuration,
    Modules.Users.Infrastructure.AssemblyReference.Assembly
);

WebApplication app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
