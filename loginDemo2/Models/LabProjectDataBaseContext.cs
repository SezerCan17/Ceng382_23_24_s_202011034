using System;
using Lab10.Models;
using Microsoft.EntityFrameworkCore;

namespace loginDemo2.Models
{
    public partial class LabProjectDataBaseContext : DbContext
    {
        public LabProjectDataBaseContext()
        {
        }

        public LabProjectDataBaseContext(DbContextOptions<LabProjectDataBaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblTodo> TblTodos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Reservat__3214EC076EC6496D");

                entity.Property(e => e.ReservationDate).HasColumnType("datetime");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Reservati__RoomI__398D8EEE");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("Rooms");

                entity.Property(e => e.RoomId).HasColumnName("RoomID");
                entity.Property(e => e.RoomName).IsRequired().HasMaxLength(50);
             

                // Diğer ilişkiler ve kısıtlamalar buraya eklenebilir
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
