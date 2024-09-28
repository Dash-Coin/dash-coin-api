using System.IdentityModel;
using System.Text;
using coin_api;
using coin_api.Application.Service;
using coin_api.Controller;
using coin_api.Domain.Model;
using coin_api.Token;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ConnectionContext>(options => 
         options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policy => policy
            .AllowAnyOrigin()  
            .AllowAnyMethod()  
            .AllowAnyHeader()); 
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                  .AddJwtBearer(option =>
                  {
                     option.TokenValidationParameters = new TokenValidationParameters
                     {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = "Teste.Securiry.Bearer",
                        ValidAudience = "Teste.Securiry.Bearer",
                        IssuerSigningKey = JwtSecurityKey.Create("JGHF4W3KHUG2867RUYFSDUIYFDT%DBHAJHKSFFY%")
                     };

                     option.Events = new JwtBearerEvents
                     {
                        OnAuthenticationFailed = context =>
                        {
                           Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                           return Task.CompletedTask;
                        },
                        OnTokenValidated = context =>
                        {
                           Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
                           return Task.CompletedTask;
                        }
                     };
                  });

builder.Services.AddScoped<TransactionRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<TransactionSerivce>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseCors("AllowAllOrigins");

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "Hello World!");

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
