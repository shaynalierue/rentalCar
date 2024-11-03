using Microsoft.EntityFrameworkCore;
using RentalCars.Models;

namespace RentalCars.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<MsCar> MsCar { get; set; }
        public DbSet<MsCustomer> MsCustomer { get; set; }
        public DbSet<MsEmployee> MsEmployee { get; set; }
        public DbSet<MsCarImages> MsCarImages { get; set; }
        public DbSet<TrMaintenance> TrMaintenance { get; set; }
        public DbSet<TrRental> TrRental { get; set; }
        public DbSet<TrPayment> TrPayment { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    // Tambahkan konfigurasi tambahan di sini jika diperlukan
        //}
    }
}
