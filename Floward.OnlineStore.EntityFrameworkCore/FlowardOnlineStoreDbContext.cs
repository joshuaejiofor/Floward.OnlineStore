using Floward.OnlineStore.Core.Models;
using Floward.OnlineStore.EntityFrameworkCore.EntityConfigurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Floward.OnlineStore.EntityFrameworkCore
{

    public class FlowardOnlineStoreDbContext : IdentityDbContext
    {
        private readonly static string _connectionStringKey = "DefaultConnection";

        public FlowardOnlineStoreDbContext(IConfiguration configuration) : base(GetOptions(configuration)) { }

        private static DbContextOptions GetOptions(IConfiguration configuration)
        {
            // Used for unit tests only
            if (Convert.ToBoolean(configuration["IsUseInMemoryDB"]))
            {
                return new DbContextOptionsBuilder<FlowardOnlineStoreDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .EnableSensitiveDataLogging().Options;
            }

            var connectionString = configuration.GetConnectionString(_connectionStringKey);
            var sqlConBuilder = new SqlConnectionStringBuilder(connectionString);
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString,
                                sqlServerOptions =>
                                {
                                    sqlServerOptions.CommandTimeout(sqlConBuilder.ConnectTimeout);
                                    sqlServerOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                                }).AddInterceptors(new CommandInterceptor()).Options;
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is EntityBase && (x.State == EntityState.Added || x.State == EntityState.Modified || x.State == EntityState.Deleted));

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((EntityBase)entity.Entity).IsActive = true;
                    ((EntityBase)entity.Entity).CreatedOn = DateTime.Now;
                }
                else if (entity.State == EntityState.Modified)
                {
                    entity.Property("CreatedOn").IsModified = false;
                }
                else if (entity.State == EntityState.Deleted)
                {
                    ((EntityBase)entity.Entity).IsActive = false;
                }

                ((EntityBase)entity.Entity).UpdatedOn = DateTime.Now;
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
        }


        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
            

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            AddTimestamps();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddTimestamps();
            return await base.SaveChangesAsync(default);
        }

    }
}
