namespace Final_Assignment1.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class OurConnection : DbContext
    {
        public OurConnection()
            : base("name=OurConnection")
        {
        }

        public virtual DbSet<Make> Make { get; set; }
        public virtual DbSet<Model> Model { get; set; }
        public virtual DbSet<Vehicle> Vehicle { get; set; }
        public virtual DbSet<VehicleType> VehicleType { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Make>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Make>()
                .HasMany(e => e.Vehicle)
                .WithRequired(e => e.Make)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Model>()
                .Property(e => e.Colour)
                .IsUnicode(false);

            modelBuilder.Entity<Model>()
                .HasMany(e => e.Vehicle)
                .WithRequired(e => e.Model)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vehicle>()
                .Property(e => e.Price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<VehicleType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<VehicleType>()
                .HasMany(e => e.Model)
                .WithRequired(e => e.VehicleType)
                .WillCascadeOnDelete(false);
        }
    }
}
