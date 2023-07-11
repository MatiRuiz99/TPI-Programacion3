using Microsoft.EntityFrameworkCore;
using Model.Models;
using Serilog;
using Serilog.Events;
using Service.IServices;
using Service.Services;

var builder = WebApplication.CreateBuilder(args);

//Config serilog
string logsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
Directory.CreateDirectory(logsFolder);
Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.File(Path.Combine(logsFolder, "logs.txt"), rollingInterval: RollingInterval.Day)
            .CreateLogger();

// Add services to the container.
builder.Services.AddDbContext<CafeteriaContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.ClearProviders();
    loggingBuilder.AddSerilog();
});
builder.Services.AddScoped<IProductService, ProductoService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISalesService, SalesService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
