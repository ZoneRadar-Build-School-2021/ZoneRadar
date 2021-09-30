using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using ZoneRadar.Models;

namespace ZoneRadar.Data
{
    public partial class ZONERadarContext : DbContext
    {
        public ZONERadarContext()
            : base("name=ZONERadarContext")
        {
        }

        public virtual DbSet<AmenityCategoryDetail> AmenityCategoryDetail { get; set; }
        public virtual DbSet<AmenityDetail> AmenityDetail { get; set; }
        public virtual DbSet<Cancellation> Cancellation { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<CleaningCategory> CleaningCategory { get; set; }
        public virtual DbSet<CleaningOption> CleaningOption { get; set; }
        public virtual DbSet<CleaningProtocol> CleaningProtocol { get; set; }
        public virtual DbSet<Collection> Collection { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<District> District { get; set; }
        public virtual DbSet<Member> Member { get; set; }
        public virtual DbSet<Operating> Operating { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderDetail> OrderDetail { get; set; }
        public virtual DbSet<OrderStatus> OrderStatus { get; set; }
        public virtual DbSet<Review> Review { get; set; }
        public virtual DbSet<Space> Space { get; set; }
        public virtual DbSet<SpaceAmenity> SpaceAmenity { get; set; }
        public virtual DbSet<SpaceDiscount> SpaceDiscount { get; set; }
        public virtual DbSet<SpacePhoto> SpacePhoto { get; set; }
        public virtual DbSet<SpaceType> SpaceType { get; set; }
        public virtual DbSet<TypeDetail> TypeDetail { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AmenityDetail>()
                .HasMany(e => e.SpaceAmenity)
                .WithRequired(e => e.AmenityDetail)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cancellation>()
                .HasMany(e => e.Space)
                .WithRequired(e => e.Cancellation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<City>()
                .HasMany(e => e.District)
                .WithRequired(e => e.City)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<City>()
                .HasMany(e => e.Space)
                .WithRequired(e => e.City)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CleaningCategory>()
                .HasMany(e => e.CleaningOption)
                .WithRequired(e => e.CleaningCategory)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CleaningOption>()
                .HasMany(e => e.CleaningProtocol)
                .WithRequired(e => e.CleaningOption)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Country>()
                .HasMany(e => e.City)
                .WithRequired(e => e.Country)
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
                .HasMany(e => e.Order)
                .WithRequired(e => e.Member)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Member>()
                .HasMany(e => e.Space)
                .WithRequired(e => e.Member)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderDetail)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.Review)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderStatus>()
                .HasMany(e => e.Order)
                .WithRequired(e => e.OrderStatus)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Space>()
                .Property(e => e.MeasureOfArea)
                .HasPrecision(18, 1);

            modelBuilder.Entity<Space>()
                .HasMany(e => e.CleaningProtocol)
                .WithRequired(e => e.Space)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Space>()
                .HasMany(e => e.Operating)
                .WithRequired(e => e.Space)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Space>()
                .HasMany(e => e.Order)
                .WithRequired(e => e.Space)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Space>()
                .HasMany(e => e.SpaceAmenity)
                .WithRequired(e => e.Space)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Space>()
                .HasMany(e => e.SpaceDiscount)
                .WithRequired(e => e.Space)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Space>()
                .HasMany(e => e.SpacePhoto)
                .WithRequired(e => e.Space)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Space>()
                .HasMany(e => e.SpaceType)
                .WithRequired(e => e.Space)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SpaceDiscount>()
                .Property(e => e.Discount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<TypeDetail>()
                .HasMany(e => e.SpaceType)
                .WithRequired(e => e.TypeDetail)
                .WillCascadeOnDelete(false);
        }
    }
}
