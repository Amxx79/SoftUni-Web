﻿using CinemaApp.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CinemaApp.Web.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensionMethods
    {
        public static IApplicationBuilder ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope serviceScope = app.ApplicationServices.CreateScope();
            CinemaDbContext dbContext = serviceScope.ServiceProvider.GetRequiredService<CinemaDbContext>();
            dbContext.Database.Migrate();

            return app;
        }
    }
}
