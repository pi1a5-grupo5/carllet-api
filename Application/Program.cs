using Domain.Interfaces;
using Infra.Data;
using Services;

var builder = WebApplication.CreateBuilder(args);

//var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
//var url = $"http://0.0.0.0:{port}";
//var target = Environment.GetEnvironmentVariable("TARGET") ?? "World";

builder.Services.AddScoped<IDbService, DbService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// app.MapGet("/", () => $"Hello {target}!");

app.MapControllers();

// app.Run(url);
app.Run();