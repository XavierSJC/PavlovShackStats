using Microsoft.EntityFrameworkCore;
using PavlovShackStats.Data;
using WebApplication1.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IPavlovShackStatsService, PavlovShackStatsService>();
var rconIpAddress = builder.Configuration.GetSection("RconSettings").GetValue<string>("ipAddress");
var rconPort = builder.Configuration.GetSection("RconSettings").GetValue<int>("port");
var rconPassword = builder.Configuration.GetSection("RconSettings").GetValue<string>("password");
builder.Services.AddSingleton<IGameStatusService>(serverStatusService => new GameStatusService(rconIpAddress, rconPort, rconPassword));

builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyOrigin();
        });
});

builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<PavlovShackStatsContext>(opt =>
    opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/error");
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

CreateDbIfNotExist(app);

app.Run();

void CreateDbIfNotExist(IHost host)
{
    using (var scope = host.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<PavlovShackStatsContext>();
            DbInitializer.Initialize(context);
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred creating the DB.");
        }
    }
}