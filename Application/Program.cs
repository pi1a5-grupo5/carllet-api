using Domain.Entities.Budget.Expenses;
using Domain.Interfaces;
using Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Services;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<IUserVehicleService, UserVehicleService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IEarningService, EarningService>();
builder.Services.AddScoped<IEarningService, EarningService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IGoalService, GoalService>();
builder.Services.AddScoped<IExpenseService<Expense>, ExpenseService>();
builder.Services.AddScoped<IExpenseService<FuelExpense>, FuelExpenseService>();
builder.Services.AddScoped<IExpenseService<MaintenanceExpense>, MaintenanceExpenseService>();
builder.Services.AddScoped<IExpenseService<OtherExpense>, OtherExpenseService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddDbContext<CarlletDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("mvp_v1"))
    .LogTo(s => System.Diagnostics.Debug.WriteLine(s)));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v2.21",
        Title = "Tarefa Swagger",
        Description = "Tarefa da materia de PDW2, onde foi feita a implementação de API RESTful, e utilizado swagger na documentação",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "João Vanderlei",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.WebHost.ConfigureKestrel(opt => opt.AddServerHeader = false);

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.Use(async (context, next) =>
{
    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Add("X-Xss-Protection", "1; mode=block");
    context.Response.Headers.Add("Referrer-Policy", "no-referrer");
    context.Response.Headers.Add("Content-Security-Policy", "base-uri 'self' www.gstatic.com; style-src 'self' fonts.googleapis.com; ");
    context.Response.Headers.Add("Feature-Policy",
                "vibrate 'self' ; " +
                "geolocation 'self' ; " +
                "push 'self' ; " +
                "notifications 'self' ; " +
                "fullscreen '*' ; ");

    context.Response.Headers.Remove("X-Powered-By");
    context.Response.Headers.Remove("Server");

    await next();
});
app.UseHttpsRedirection();

app.MapControllers();

// app.Run(url);
app.Run();