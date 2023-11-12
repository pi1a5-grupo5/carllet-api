using Domain.Entities;
using Domain.Interfaces;
using Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public static class TestInitializer
    {
        private static ServiceProvider _serviceProvider;

        public static CarlletDbContext GetCarlletDbContext()
        {
            if(_serviceProvider == null)
            {
                var services = new ServiceCollection();
                services.AddDbContext<CarlletDbContext>(options =>
                    options.UseInMemoryDatabase(databaseName: "TestDatabase"));

                services.AddScoped<IUserService, UserService>();
                services.AddScoped<IVehicleService, VehicleService>();
                services.AddScoped<IUserVehicleService, UserVehicleService>();
                services.AddScoped<IEarningService, EarningService>();
                // services.AddScoped<IExpenseService, ExpenseService>();
                services.AddScoped<ICourseService, CourseService>();
                services.AddScoped<IAuthService, AuthService>();
                services.AddScoped<IEmailService, EmailService>();

                _serviceProvider = services.BuildServiceProvider();
            }

            return _serviceProvider.GetRequiredService<CarlletDbContext>();
        }
    }
}
