using DiaryAgronomist.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace DiaryAgronomist.Context
{
    public class DaContext : DbContext
    {
        public DaContext(DbContextOptions<DaContext> options) : base(options) { }

        public virtual DbSet<Cereal> Cereals { get; set; }
        public virtual DbSet<ConsumptionFuel> ConsumptionFuels { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<FieldPlanting> FieldPlantings { get; set; }  
        public virtual DbSet<Fuel> Fuels { get; set; }
        public virtual DbSet<FuelSupplier> FuelSuppliers { get; set; }
        public virtual DbSet<Harvesting> Harvestings { get; set; }
        public virtual DbSet<HarvestingEmployee> HarvestingEmployees { get; set; }
        public virtual DbSet<HarvestingTechnique> HarvestingTechniques { get; set; }
        public virtual DbSet<Machinery> Machineries { get; set; }
        public virtual DbSet<ReceptionFuel> ReceptionFuels { get; set; }
        public virtual DbSet<Sowing> Sowings { get; set; }  
        public virtual DbSet<SowingEmployee> SowingEmployees { get; set; }
        public virtual DbSet<SowingTechnique> SowingTechniques { get; set; }
        public virtual DbSet<Warehouse> Warehouses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder) =>
                builder
                .UseNpgsql("Host=localhost;Port=5433;Username=postgres;Password=postgres;Database=diary_agronomist");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Harvesting>()
                    .HasMany(e => e.Employees)
                    .WithMany(h => h.Harvestings)
                    .UsingEntity<HarvestingEmployee>(
                        j => j.HasOne(e => e.Employee)
                        .WithMany(h => h.HarvestingEmployees)
                        .HasForeignKey(e => e.EmployeeId),
                        j => j.HasOne(h => h.Harvesting)
                        .WithMany(e => e.HarvestingEmployees)
                        .HasForeignKey(h => h.HarvestingId),
                        j => 
                        {
                            j.HasKey(k => new { k.EmployeeId, k.HarvestingId });
                        });

            modelBuilder.Entity<Harvesting>()
                    .HasMany(m => m.Machineries)
                    .WithMany(h => h.Harvestings)
                    .UsingEntity<HarvestingTechnique>(
                        j => j.HasOne(m => m.Machinery)
                        .WithMany(ht => ht.HarvestingTechniques)
                        .HasForeignKey(m => m.MachineryId),
                        j => j.HasOne(h => h.Harvesting)
                        .WithMany(ht => ht.HarvestingTechniques)
                        .HasForeignKey(h => h.HarvestingId),
                        j =>
                        {
                            j.HasKey(k => new { k.HarvestingId, k.MachineryId });
                        });

            modelBuilder.Entity<Sowing>()
                .HasMany(e => e.Employees)
                    .WithMany(h => h.Sowings)
                    .UsingEntity<SowingEmployee>(
                        j => j.HasOne(e => e.Employee)
                        .WithMany(h => h.SowingEmployees)
                        .HasForeignKey(e => e.EmployeeId),
                        j => j.HasOne(h => h.Sowing)
                        .WithMany(e => e.SowingEmployees)
                        .HasForeignKey(h => h.SowingId),
                        j =>
                        {
                            j.HasKey(k => new { k.EmployeeId, k.SowingId });
                        });

            modelBuilder.Entity<Sowing>()
                .HasMany(m => m.Machineries)
                .WithMany(s => s.Sowings)
                    .UsingEntity<SowingTechnique>(
                        j => j.HasOne(m => m.Machinery)
                        .WithMany(st => st.SowingTechniques)
                        .HasForeignKey(p => p.MachineryId),
                        j => j.HasOne(s => s.Sowing)
                        .WithMany(st => st.SowingTechniques)
                        .HasForeignKey(s => s.SowingId),
                        j =>
                        {
                            j.HasKey(k => new { k.SowingId, k.MachineryId });
                        });


        }
    }
}
