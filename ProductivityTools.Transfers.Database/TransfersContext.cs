using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ProductivityTools.Transfers.Database.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.Transfers.Database
{
    public class TransfersContext : DbContext
    {

        private readonly IConfiguration configuration;
        public DbSet<Transfer> Transfers { get; set; }

        public TransfersContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        //private ILoggerFactory GetLoggerFactory()
        //{
        //    IServiceCollection serviceCollection = new ServiceCollection();
        //    serviceCollection.AddLogging(builder =>
        //           builder.AddConsole()
        //                  .AddFilter(DbLoggerCategory.Database.Command.Name,
        //                             LogLevel.Information));
        //    return serviceCollection.BuildServiceProvider()
        //            .GetService<ILoggerFactory>();
        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("PTTransfers"));
               // optionsBuilder.UseLoggerFactory(GetLoggerFactory());
                optionsBuilder.EnableSensitiveDataLogging();
                base.OnConfiguring(optionsBuilder);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transfer>().ToTable("Transfer");

            base.OnModelCreating(modelBuilder);
        }


    }
}
