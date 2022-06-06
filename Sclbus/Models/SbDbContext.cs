using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Sclbus.Models
{
    public partial class SbDbContext : DbContext
    {
        public SbDbContext()
        {
        }

        public SbDbContext(DbContextOptions<SbDbContext> options)
            : base(options)
        {
        }
        private static SbDbContext _instace;
        public static SbDbContext GetInstace()
        {
            if (_instace == null)
            {
                _instace = new SbDbContext();
            }
            return _instace;
        }
        public virtual DbSet<Attendance> Attendances { get; set; } = null!;
        public virtual DbSet<Car> Cars { get; set; } = null!;
        public virtual DbSet<Driver> Drivers { get; set; } = null!;
        public virtual DbSet<Group> Groups { get; set; } = null!;
        public virtual DbSet<Holiday> Holidays { get; set; } = null!;
        public virtual DbSet<Parent> Parents { get; set; } = null!;
        public virtual DbSet<Ride> Rides { get; set; } = null!;
       
        public virtual DbSet<Student> Students { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=WIN-MK22ORIQEJJ;Initial Catalog=SbDb;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendance>(entity =>
            {
                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Attendances)
                    .HasForeignKey(d => d.StudentId);
            });

            modelBuilder.Entity<Car>(entity =>
            {
                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.DriverId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Driver>(entity =>
            {
                entity.HasOne(d => d.Car)
                    .WithMany(p => p.Drivers)
                    .HasForeignKey(d => d.CarId)
                    .HasConstraintName("FK__Drivers__CarId__46E78A0C");
            });

            modelBuilder.Entity<Ride>(entity =>
            {
                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.Rides)
                    .HasForeignKey(d => d.DriverId);
            });
                        

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.ParentId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
