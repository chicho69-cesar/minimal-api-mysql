using Microsoft.EntityFrameworkCore;
using MinimalApis.Models;

namespace MinimalApis.Data {
    public class ApiContext : DbContext {
        private readonly string _connectionString;

        public ApiContext(string connectionString) { 
            _connectionString = connectionString;
        }

        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }

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