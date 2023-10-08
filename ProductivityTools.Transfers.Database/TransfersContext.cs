using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using ProductivityTools.Transfers.Database.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.Transfers.Database
{
    public class TransfersContext : DbContext
    {

        private readonly IConfiguration configuration;
        
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<TransferHistory> TransfersHistory { get; set; }
        public DbSet<Account> Accounts { get; set; }

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
            modelBuilder.Entity<Transfer>().ToTable("Transfer")
                .HasKey(x => x.TransferId);
            modelBuilder.Entity<Account>().HasMany(x => x.TransfersSource).WithOne(x => x.Source).HasForeignKey(x => x.SourceId).HasPrincipalKey(x => x.AccountId);
            modelBuilder.Entity<Account>().HasMany(x => x.TransfersTarget).WithOne(x => x.Target).HasForeignKey(x => x.TargetId).HasPrincipalKey(x => x.AccountId);
            //modelBuilder.Entity<Transfer>().HasOne(x => x.Source).WithMany(x => x.Transfers).HasForeignKey(x => x.SourceId).HasPrincipalKey(x => x.AccountId);
            //modelBuilder.Entity<Transfer>().HasOne(x => x.Target).WithMany(x => x.Transfers).HasForeignKey(x => x.TargetId).HasPrincipalKey(x => x.AccountId);
            modelBuilder.Entity<TransferHistory>().ToTable("TransferHistory")
                .HasKey(x=>x.TransferHistoryId);
            modelBuilder.Entity<Account>().ToTable("Account")
                .HasKey(x => x.AccountId);
            base.OnModelCreating(modelBuilder);
        }


    }
}
