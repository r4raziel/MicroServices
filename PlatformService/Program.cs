using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
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

Console.WriteLine($"---app is running on: {app.Environment.EnvironmentName} ");
PrepDb.PrepPopulation(app, app.Environment.IsProduction());

app.Run();
