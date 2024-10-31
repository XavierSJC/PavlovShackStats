using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PavlovShackStats.Data;
using WebApplication1.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

builder.Services.AddTransient<IPavlovShackStatsService, PavlovShackStatsService>();

var modIoApiPath = builder.Configuration.GetSection("Mod.io").GetValue<string>("ApiPath");
var modIoApiKey = builder.Configuration.GetSection("Mod.io").GetValue<string>("ApiKey");
builder.Services.AddSingleton<IModIoService>(modIoService => new ModIoService(modIoApiPath, modIoApiKey));

var rconIpAddress = builder.Configuration.GetSection("RconSettings").GetValue<string>("ipAddress");
var rconPort = builder.Configuration.GetSection("RconSettings").GetValue<int>("port");
var rconPassword = builder.Configuration.GetSection("RconSettings").GetValue<string>("password");
builder.Services.AddSingleton<IGameStatusService>(x => 
    new GameStatusService(x.GetRequiredService<IModIoService>(), rconIpAddress, rconPort, rconPassword));

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

app.UseForwardedHeaders();

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