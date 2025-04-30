
using Application.Interfaces;
using Application.Registration;
using Application.Service;
using FriendsOnlyWeb.Middlewares;
using Identity.Registration;
using Infrastrucutre_Persistance_Presentation.RepoRegistration;
using Microsoft.AspNetCore.Identity;
using ShareLayer.Registration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddPersistenceInfrastructure(builder.Configuration);
builder.Services.AddIdentityInfrastructure(builder.Configuration);
builder.Services.AddShareLayerRegistration(builder.Configuration);
builder.Services.AddSession();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<ValidateSassion, ValidateSassion>();
builder.Services.AddScoped<LoginAuthorized>();

builder.Services.AddServiceRegistration();


var app = builder.Build();

await app.Services.RunSeeds();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Index}/{id?}");

await app.RunAsync();
