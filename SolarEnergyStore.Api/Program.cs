using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SolarEnergyStore.Repositories;
using SolarEnergyStore.Repositories.DevicePeriodActive;
using SolarEnergyStore.Repositories.DeviceTemperatureRecord;
using SolarEnergyStore.Services.DevicePeriodActive;
using SolarEnergyStore.Services.DeviceTemperatureRecord;
using SolarEnergyStore.Services.Interfaces;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "Origins",
                      builder =>
                      {
                          builder.WithOrigins("https://localhost:8080").AllowAnyHeader().AllowAnyMethod();
                      });
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Device Control API",
        Description = "API to control electrical devices using Arduino sensor data",
    });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
Console.WriteLine(connectionString);
builder.Services.AddDbContext<ApplicantDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(80);
});

builder.Services.AddScoped<IDeviceTemperatureRecordRepository, DeviceTemperatureRecordRepository>();
builder.Services.AddScoped<IDevicePeriodActiveRepository, DevicePeriodActiveRepository>();

builder.Services.AddScoped<DeviceTemperatureRecordService>();
builder.Services.AddScoped<DevicePeriodActiveService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Device Control API v1");
    c.RoutePrefix = "swagger";
});


app.MapControllers();

app.UseHttpsRedirection();
app.UseCors("Origins");

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicantDbContext>();

    // Aplica as migrations automaticamente
    dbContext.Database.Migrate();
}

app.Run();