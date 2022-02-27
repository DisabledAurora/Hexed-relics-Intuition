using Hexed_Relics.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;
var webHostenv = builder.Environment;

// Add services to the container.
services.AddRazorPages(options =>
{
    //options.Conventions.AuthorizeFolder("/Banking");
});

services.AddAuthentication("authcookie").AddCookie("authcookie",options =>{
    options.Cookie.Name = "loginAuthCookie";
    options.LoginPath = "/Auth/Login";
    options.AccessDeniedPath = "/Auth/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(2);
});

services.AddAuthorization(options =>
{
    options.AddPolicy("User", policy => policy.RequireClaim("User"));
});

services.AddDbContext<HexedRelicsContext>(option =>
{
    option.UseNpgsql(configuration.GetConnectionString("HexedRelicsContext"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseStatusCodePagesWithRedirects("/404");

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
