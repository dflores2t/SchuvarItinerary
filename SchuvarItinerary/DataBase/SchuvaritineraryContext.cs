using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SchuvarItinerary.DataBase;

public partial class SchuvaritineraryContext : DbContext
{
    public SchuvaritineraryContext()
    {
    }

    public SchuvaritineraryContext(DbContextOptions<SchuvaritineraryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aerolinea> Aerolineas { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Flycustomer> Flycustomers { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aerolinea>(entity =>
        {
            entity.HasKey(e => e.AerolineaId).HasName("aerolinea_pkey");

            entity.ToTable("aerolinea", "schu");

            entity.Property(e => e.AerolineaId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("aerolinea_id");
            entity.Property(e => e.AerolineaDatemodify)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("aerolinea_datemodify");
            entity.Property(e => e.AerolineaDateup)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("aerolinea_dateup");
            entity.Property(e => e.AerolineaFullname)
                .HasMaxLength(50)
                .HasColumnName("aerolinea_fullname");
            entity.Property(e => e.AerolineaIsdeleted)
                .HasDefaultValueSql("false")
                .HasColumnName("aerolinea_isdeleted");
            entity.Property(e => e.AerolineaShortname)
                .HasMaxLength(2)
                .HasColumnName("aerolinea_shortname");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("customer_pkey");

            entity.ToTable("customer", "schu");

            entity.HasIndex(e => e.CustomerPhone, "idx_customer_phone");

            entity.HasIndex(e => e.CustomerPhone, "unq_phone").IsUnique();

            entity.Property(e => e.CustomerId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("customer_id");
            entity.Property(e => e.CustomerDatemodify)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("customer_datemodify");
            entity.Property(e => e.CustomerDateup)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("customer_dateup");
            entity.Property(e => e.CustomerFullname)
                .HasMaxLength(100)
                .HasColumnName("customer_fullname");
            entity.Property(e => e.CustomerIsdeleted)
                .HasDefaultValueSql("false")
                .HasColumnName("customer_isdeleted");
            entity.Property(e => e.CustomerPhone)
                .HasMaxLength(9)
                .HasColumnName("customer_phone");
        });

        modelBuilder.Entity<Flycustomer>(entity =>
        {
            entity.HasKey(e => e.FlycustomerId).HasName("flycustomer_pkey");

            entity.ToTable("flycustomer", "schu");

            entity.Property(e => e.FlycustomerId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("flycustomer_id");
            entity.Property(e => e.FlycustomerArrivals)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("flycustomer_arrivals");
            entity.Property(e => e.FlycustomerDatemodify)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("flycustomer_datemodify");
            entity.Property(e => e.FlycustomerDateup)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("flycustomer_dateup");
            entity.Property(e => e.FlycustomerDeparture)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("flycustomer_departure");
            entity.Property(e => e.FlycustomerIdaerolinea).HasColumnName("flycustomer_idaerolinea");
            entity.Property(e => e.FlycustomerIdcustomer).HasColumnName("flycustomer_idcustomer");
            entity.Property(e => e.FlycustomerIsdeleted)
                .HasDefaultValueSql("false")
                .HasColumnName("flycustomer_isdeleted");
            entity.Property(e => e.FlycustomerLocalyzer)
                .HasMaxLength(10)
                .HasColumnName("flycustomer_localyzer");
            entity.Property(e => e.FlycustomerRoute)
                .HasMaxLength(20)
                .HasColumnName("flycustomer_route");

            entity.HasOne(d => d.FlycustomerIdaerolineaNavigation).WithMany(p => p.Flycustomers)
                .HasForeignKey(d => d.FlycustomerIdaerolinea)
                .HasConstraintName("flycustomer_flycustomer_idaerolinea_fkey");

            entity.HasOne(d => d.FlycustomerIdcustomerNavigation).WithMany(p => p.Flycustomers)
                .HasForeignKey(d => d.FlycustomerIdcustomer)
                .HasConstraintName("flycustomer_flycustomer_idcustomer_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
