using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PlatformService.AsyncDataServices;
using PlatformService.Data;
using PlatformService.SyncDataServices.Grpc;
using PlatformService.SyncDataServices.Http;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
if (builder.Environment.IsProduction())
{
    var sqlConBuilder = new SqlConnectionStringBuilder();
    sqlConBuilder.ConnectionString = builder.Configuration.GetConnectionString("PlatformsConnection");
    Console.WriteLine("---> using SqlServer Db");
    builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(sqlConBuilder.ConnectionString));
}
else
{
    Console.WriteLine("---> using InMem Db");
    builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMemory"));
}
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();
builder.Services.AddSingleton<IMessageBusClient, MessageBusClient>();
builder.Services.AddGrpc();


var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

Console.WriteLine($"--> CommandService Endpoint {builder.Configuration["CommandService"]}");
//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapGrpcService<GrpcPlatformService>();
app.MapGet("/Protos/platforms.proto", async context => {

    await context.Response.WriteAsync(File.ReadAllText("Protos/platforms.proto"));
});

Console.WriteLine($"---app is running on: {app.Environment.EnvironmentName} ");
PrepDb.PrepPopulation(app, app.Environment.IsProduction());

app.Run();
