using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Lab10.Models;

public partial class Lab10Context : DbContext
{
    public Lab10Context()
    {
    }

    public Lab10Context(DbContextOptions<Lab10Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = WebApplication.CreateBuilder();
        var connectionString = builder.Configuration.GetConnectionString ("DefaultConnection");
        optionsBuilder.UseSqlServer(connectionString);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reservat__3214EC076EC6496D");

            entity.Property(e => e.ReservationDate).HasColumnType("datetime");

            entity.HasOne(d => d.Room).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservati__RoomI__398D8EEE");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.RoomId).HasName("PK__rooms__6C3BF5DEFA22C6B9");

            entity.ToTable("rooms");

            entity.Property(e => e.RoomId).HasColumnName("roomID");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.RoomName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("roomName");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
