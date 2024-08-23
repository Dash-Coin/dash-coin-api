using System.IdentityModel;
using System.Text;
using coin_api;
using coin_api.Domain.Model;
using coin_api.Infra.Repository;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<TransactionRepository>();

builder.Services.AddControllers();

builder.Services.AddTransient<IUserRepository, UserRepository>();

var key = Encoding.ASCII.GetBytes(Key.Secret);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
   //  talvez esse seja o correto
    // x.DefaultAuthenticateScheme = BearerTokenDefaults.AuthenticationScheme;
    // x.DefaultChallengeScheme = BearerTokenDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

var app = builder.Build();


app.MapGet("/", () => "Hello World!");

app.UseHttpsRedirection();

app.MapControllers();

app.Run();