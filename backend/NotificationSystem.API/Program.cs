using Microsoft.EntityFrameworkCore;
using NotificationSystem.Business.Services;
using NotificationSystem.Core.Interfaces;
using NotificationSystem.DataAccess.Data;
using NotificationSystem.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<INotificationLogRepository, NotificationLogRepository>();

// FCM Service
var fcmServiceAccountJson = builder.Configuration["Firebase:ServiceAccountJson"] ?? "{}";
builder.Services.AddSingleton<IFcmService>(new FcmService(fcmServiceAccountJson));

// Business Services
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IDeviceService, DeviceService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors();
app.UseAuthorization();
app.MapControllers();

// Auto-migrate on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.Run();
