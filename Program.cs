var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<TransactionRepository>();

builder.Services.AddControllers();

var app = builder.Build();


app.MapGet("/", () => "Hello World!");

app.UseHttpsRedirection();

app.MapControllers();

app.Run();