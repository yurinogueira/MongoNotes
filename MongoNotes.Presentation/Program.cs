using AspNetCore.Identity.MongoDbCore.Models;
using Microsoft.AspNetCore.Identity;
using MongoNotes.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
                       ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
var databaseName = builder.Configuration.GetValue<string>("MongoDb:DatabaseName") 
                   ?? throw new InvalidOperationException("Database name 'MongoDb:Database' not found.");

builder.Services
    .AddIdentity<MongoIdentityUser, MongoIdentityRole>()
    .AddMongoDbStores<MongoIdentityUser, MongoIdentityRole, Guid>(
        connectionString,
        databaseName)
    .AddDefaultTokenProviders()
    .AddDefaultUI();

builder.Services.AddRepositories(connectionString, databaseName);

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
