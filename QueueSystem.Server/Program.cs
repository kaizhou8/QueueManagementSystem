using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using QueueSystem.Server.Data;
using QueueSystem.Server.Hubs;
using QueueSystem.Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSingleton<QueueService>();
builder.Services.AddSignalR();

// Add CORS policy for mobile client
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("AllowAll");
app.UseRouting();

app.MapBlazorHub();
app.MapHub<QueueHub>("/queueHub");
app.MapFallbackToPage("/_Host");

app.Run();
