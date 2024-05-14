using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Car_Rental_System.Models;

public partial class CrsContext : DbContext
{
    public CrsContext()
    {
    }

    public CrsContext(DbContextOptions<CrsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Lease> Leases { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=TANYA-PERSONAL;Database=CRS;Trusted_Connection=True;Encrypt=false;TrustServerCertificate=false;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__customer__B611CB7DA1B18EE4");

            entity.ToTable("customer");

            entity.Property(e => e.CustomerId)
                .ValueGeneratedNever()
                .HasColumnName("customerId");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("firstName");
            entity.Property(e => e.LastName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("lastName");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("phoneNumber");
        });

        modelBuilder.Entity<Lease>(entity =>
        {
            entity.HasKey(e => e.LeaseId).HasName("PK__Lease__C6D5C5834F0B19FF");

            entity.ToTable("Lease");

            entity.Property(e => e.LeaseId)
                .ValueGeneratedNever()
                .HasColumnName("leaseId");
            entity.Property(e => e.CustomerId).HasColumnName("customerId");
            entity.Property(e => e.EndDate).HasColumnName("endDate");
            entity.Property(e => e.LeaseType)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("leaseType");
            entity.Property(e => e.StartDate).HasColumnName("startDate");
            entity.Property(e => e.VehicleId).HasColumnName("vehicleId");

            entity.HasOne(d => d.Customer).WithMany(p => p.Leases)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Lease__customerI__3B75D760");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.Leases)
                .HasForeignKey(d => d.VehicleId)
                .HasConstraintName("FK__Lease__vehicleId__3C69FB99");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__A0D9EFC653DAA7B5");

            entity.ToTable("Payment");

            entity.Property(e => e.PaymentId)
                .ValueGeneratedNever()
                .HasColumnName("paymentId");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.LeaseId).HasColumnName("leaseId");
            entity.Property(e => e.PaymentDate).HasColumnName("paymentDate");

            entity.HasOne(d => d.Lease).WithMany(p => p.Payments)
                .HasForeignKey(d => d.LeaseId)
                .HasConstraintName("FK__Payment__leaseId__3F466844");
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.VehicleId).HasName("PK__Vehicle__5B9D25F2BB249009");

            entity.ToTable("Vehicle");

            entity.Property(e => e.VehicleId)
                .ValueGeneratedNever()
                .HasColumnName("vehicleId");
            entity.Property(e => e.DailyRate).HasColumnName("dailyRate");
            entity.Property(e => e.EngineCapacity).HasColumnName("engineCapacity");
            entity.Property(e => e.Make)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("make");
            entity.Property(e => e.Model)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("model");
            entity.Property(e => e.PassengerCapacity).HasColumnName("passengerCapacity");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.Year).HasColumnName("year");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
