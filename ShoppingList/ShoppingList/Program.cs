using EntityLayer.Context;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);




// Add services to the container.
//bu fluent validation için ekledim
builder.Services.AddControllersWithViews()
    .AddFluentValidation(a => {
        a.RegisterValidatorsFromAssemblyContaining<Program>();
    });

#region Token

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//        .AddJwtBearer(options =>
//        {
//            options.RequireHttpsMetadata = false;
//            options.TokenValidationParameters = new TokenValidationParameters()
//            {
//                ValidAudience = "localhost",
//                ValidIssuer = "localhost",
//                ValidateAudience = true,
//                ValidateIssuer = true,
//                ValidateLifetime = true,
//                IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String
//                ("shoplistlistshopshoplistlistshop"))
//            };
//        }); 
#endregion

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
 {
     options.Cookie.Name = "NetCoreMvc.Auth";
     options.LoginPath = "/Login/Login/";
     options.AccessDeniedPath = "/ErrorPage/Error401/";

 });


var app = builder.Build();





// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();

app.UseStatusCodePagesWithReExecute("/ErrorPage/Error404/");
app.UseAuthorization();

app.MapControllerRoute(
    name: "default2",
    pattern: "{controller=Login}/{action=Login}/{id?}/{name?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();