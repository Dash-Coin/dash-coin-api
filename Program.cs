using System.IdentityModel;
using System.Text;
using coin_api;
using coin_api.Domain.Model;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<TransactionRepository>();

builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseHttpsRedirection();

app.MapControllers();

app.Run();