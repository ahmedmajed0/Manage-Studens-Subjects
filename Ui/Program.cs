using BL.Interfaces;
using BL.MappingProfile;
using BL.Services;
using DAL.Context;
using DAL.Interfaces;
using DAL.Repopsitories;
using DAL.UserModels;
using Domains;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System.Configuration;
using Ui.Models;
using Ui.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


RegisterServicesHelper.RegisteredServices(builder);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


//app.UseEndpoints(endpoints =>
//{
//endpoints.MapControllerRoute(
//name: "admin",
//pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

//app.MapControllerRoute(
//name: "default",
//pattern: "{controller=Home}/{action=Index}/{id?}");
//});

app.MapControllerRoute(
  name: "areas",
  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var dbContext = services.GetRequiredService<ShippingContext>();

    //Apply migrations
    await dbContext.Database.MigrateAsync();

    //Seed data
    await SeedingConfig.SeedDataAsync(dbContext, userManager, roleManager);
}

app.Run();
