using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SchuvarItinerary.Models;

public partial class SchuvarItineraryContext : DbContext
{
    public SchuvarItineraryContext()
    {
    }

    public SchuvarItineraryContext(DbContextOptions<SchuvarItineraryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aerolinea> Aerolineas { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<FlyCustomer> FlyCustomers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aerolinea>(entity =>
        {
            entity.HasKey(e => e.IdAerolinea).HasName("pk_Id_aerolinea");

            entity.ToTable("Aerolinea");

            entity.Property(e => e.AeroDescription)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.AerolineaName)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.IdCustomer).HasName("pk_id_customer");

            entity.ToTable("Customer");

            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<FlyCustomer>(entity =>
        {
            entity.HasKey(e => e.IdFly).HasName("pk_IdFly_FlyCustomer");

            entity.ToTable("FlyCustomer");

            entity.Property(e => e.Arrivals).HasColumnType("date");
            entity.Property(e => e.Departures).HasColumnType("date");
            entity.Property(e => e.Localizer)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Route)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.IdAerolineaNavigation).WithMany(p => p.FlyCustomers)
                .HasForeignKey(d => d.IdAerolinea)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_IdAerolinea_FlyCustomer_Aerolinea");

            entity.HasOne(d => d.IdCustomerNavigation).WithMany(p => p.FlyCustomers)
                .HasForeignKey(d => d.IdCustomer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_IdCustomer_FlyCustomer_Customer");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
