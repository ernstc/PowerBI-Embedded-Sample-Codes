using AppOwnsData.Models;
using AppOwnsData.Services;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Register AadService and PbiEmbedService for dependency injection
builder.Services
    .AddScoped<AadService>()
    .AddScoped<PbiEmbedService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Loading appsettings.json in C# Model classes
builder.Services
    .Configure<AzureAd>(builder.Configuration.GetSection("AzureAd"))
    .Configure<PowerBI>(builder.Configuration.GetSection("PowerBI"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

// Add DefaultFiles middleware
//app.UseDefaultFiles(new DefaultFilesOptions
//{
//    DefaultFileNames = ["index.html"]
//});

app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
