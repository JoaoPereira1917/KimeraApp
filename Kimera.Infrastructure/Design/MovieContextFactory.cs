using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Kimera.Infrastructure.Design
{
    internal class MovieContextFactory : IDesignTimeDbContextFactory<MovieContext>
    {
        public MovieContext CreateDbContext(string[] args)
        {
            var basePath = Directory.GetCurrentDirectory();

            var config = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = config.GetConnectionString("DefaultConnection")
                ?? Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")
                ?? Environment.GetEnvironmentVariable("CONNECTION_STRING")
                ?? "Host=localhost;Port=5433;Database=kimera;Username=postgres;Password=postgres";

            var optionsBuilder = new DbContextOptionsBuilder<MovieContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new MovieContext(optionsBuilder.Options);
        }
    }
}
