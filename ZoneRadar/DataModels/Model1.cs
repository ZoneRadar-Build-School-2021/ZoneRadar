using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ZoneRadar.DataModels
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<AmenityDetail> AmenityDetail { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Collection> Collection { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<District> District { get; set; }
        public virtual DbSet<Member> Member { get; set; }
        public virtual DbSet<Operating> Operating { get; set; }
        public virtual DbSet<OrderDetail> OrderDetail { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Review> Review { get; set; }
        public virtual DbSet<Space> Space { get; set; }
        public virtual DbSet<SpaceAmenity> SpaceAmenity { get; set; }
        public virtual DbSet<SpacePhoto> SpacePhoto { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AmenityDetail>()
                .HasMany(e => e.SpaceAmenity)
                .WithRequired(e => e.AmenityDetail)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<City>()
                .HasMany(e => e.Space)
                .WithRequired(e => e.City)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Country>()
                .HasMany(e => e.Space)
                .WithRequired(e => e.Country)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<District>()
                .HasMany(e => e.Space)
                .WithRequired(e => e.District)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Member>()
                .HasMany(e => e.Collection)
                .WithRequired(e => e.Member)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Member>()
                .HasMany(e => e.Space)
                .WithRequired(e => e.Member)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Orders>()
                .HasMany(e => e.OrderDetail)
                .WithRequired(e => e.Orders)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Orders>()
                .HasMany(e => e.Review)
                .WithRequired(e => e.Orders)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Space>()
                .Property(e => e.MeasureOfArea)
                .HasPrecision(18, 1);

            modelBuilder.Entity<Space>()
                .Property(e => e.PricePerHour)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Space>()
                .HasMany(e => e.Operating)
                .WithRequired(e => e.Space)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Space>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Space)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Space>()
                .HasMany(e => e.SpaceAmenity)
                .WithRequired(e => e.Space)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Space>()
                .HasMany(e => e.SpacePhoto)
                .WithRequired(e => e.Space)
                .WillCascadeOnDelete(false);
        }
    }
}
