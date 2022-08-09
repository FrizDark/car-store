using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DBGenerator.Models
{
    public partial class CarstoreDBContext : DbContext
    {
        public CarstoreDBContext(bool delete = false)
        {
            if (delete) Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public CarstoreDBContext(DbContextOptions<CarstoreDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Car> Cars { get; set; } = null!;
        public virtual DbSet<CarMark> CarMarks { get; set; } = null!;
        public virtual DbSet<CarModel> CarModels { get; set; } = null!;
        public virtual DbSet<CarPhoto> CarPhotos { get; set; } = null!;
        public virtual DbSet<CarProposition> CarPropositions { get; set; } = null!;
        public virtual DbSet<CarType> CarTypes { get; set; } = null!;
        public virtual DbSet<Color> Colors { get; set; } = null!;
        public virtual DbSet<Detail> Details { get; set; } = null!;
        public virtual DbSet<DetailProposition> DetailPropositions { get; set; } = null!;
        public virtual DbSet<DetailType> DetailTypes { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<Photo> Photos { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserType> UserTypes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Carstore;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>(entity =>
            {
                entity.ToTable("Car");

                entity.HasOne(d => d.Model)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.ModelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Car__ModelId__73BA3083");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Car__TypeId__44FF419A");

                entity.HasOne(d => d.Color)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.ColorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Car__ColorId__45AB4562");
            });

            modelBuilder.Entity<CarMark>(entity =>
            {
                entity.ToTable("CarMark");

                entity.Property(e => e.Name).HasMaxLength(90);
            });

            modelBuilder.Entity<CarModel>(entity =>
            {
                entity.ToTable("CarModel");

                entity.Property(e => e.Name).HasMaxLength(90);

                entity.HasOne(d => d.Mark)
                    .WithMany(p => p.CarModels)
                    .HasForeignKey(d => d.MarkId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CarModel__MarkId__75A278F5");
            });

            modelBuilder.Entity<CarPhoto>(entity =>
            {
                entity.ToTable("CarPhoto");

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.CarPhotos)
                    .HasForeignKey(d => d.CarId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CarPhoto__CarId__160F4887");

                entity.HasOne(d => d.Photo)
                    .WithMany(p => p.CarPhotos)
                    .HasForeignKey(d => d.PhotoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CarPhoto__PhotoI__17036CC0");
            });

            modelBuilder.Entity<CarProposition>(entity =>
            {
                entity.ToTable("CarProposition");

                entity.Property(e => e.CreationDate).HasColumnType("date");

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.CarPropositions)
                    .HasForeignKey(d => d.CarId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CarPropos__CarId__503BEA1C");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CarPropositions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CarPropos__UserI__4F47C5E3");
            });

            modelBuilder.Entity<CarType>(entity =>
            {
                entity.ToTable("CarType");

                entity.Property(e => e.Name).HasMaxLength(30);
            });

            modelBuilder.Entity<Color>(entity =>
            {
                entity.ToTable("Color");

                entity.Property(e => e.Name).HasMaxLength(30);
            });

            modelBuilder.Entity<Detail>(entity =>
            {
                entity.ToTable("Detail");

                entity.Property(e => e.Brand).HasMaxLength(100);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.Photo)
                    .WithMany(p => p.Details)
                    .HasForeignKey(d => d.PhotoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Detail__PhotoId__40058253");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Details)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Detail__TypeId__3F115E1A");
            });

            modelBuilder.Entity<DetailProposition>(entity =>
            {
                entity.ToTable("DetailProposition");

                entity.Property(e => e.CreationDate).HasColumnType("date");

                entity.HasOne(d => d.Detail)
                    .WithMany(p => p.DetailPropositions)
                    .HasForeignKey(d => d.DetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DetailPro__Detai__540C7B00");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.DetailPropositions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DetailPro__UserI__531856C7");
            });

            modelBuilder.Entity<DetailType>(entity =>
            {
                entity.ToTable("DetailType");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("Notification");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Description).HasMaxLength(300);

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.CarId)
                    .HasConstraintName("FK__Notificat__CarId__59FA5E80");

                entity.HasOne(d => d.Detail)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.DetailId)
                    .HasConstraintName("FK__Notificat__Detai__3E1D39E1");

                entity.HasOne(d => d.UserFrom)
                    .WithMany(p => p.NotificationUserFroms)
                    .HasForeignKey(d => d.UserFromId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Notificat__UserF__5812160E");

                entity.HasOne(d => d.UserTo)
                    .WithMany(p => p.NotificationUserTos)
                    .HasForeignKey(d => d.UserToId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Notificat__UserT__59063A47");
            });

            modelBuilder.Entity<Photo>(entity =>
            {
                entity.ToTable("Photo");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(e => e.Phone, "UQ__User__5C7E359EC3D72DF2")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__User__A9D1053450B6D4CF")
                    .IsUnique();

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Firstname).HasMaxLength(50);

                entity.Property(e => e.Lastname).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(25);

                entity.HasOne(d => d.Avatar)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.AvatarId)
                    .HasConstraintName("FK__User__AvatarId__3B75D760");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__User__TypeId__3A81B327");
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.ToTable("UserType");

                entity.Property(e => e.Name).HasMaxLength(20);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
