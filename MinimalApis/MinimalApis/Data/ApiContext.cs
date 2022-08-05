using Microsoft.EntityFrameworkCore;
using MinimalApis.Models;

namespace MinimalApis.Data {
    public class ApiContext : DbContext {
        private readonly string _connectionString;

        #pragma warning disable CS8618 
        public ApiContext(string connectionString) { 
            _connectionString = connectionString;
        }

        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }
        #pragma warning restore CS8618

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseMySQL(_connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<User>().ToTable("users");
            base.OnModelCreating(modelBuilder);
        }
    }
}