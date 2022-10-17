using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AirWayTicketBooking.Models
{
    public partial class AirWayTicketBookContext : DbContext
    {
        public AirWayTicketBookContext()
        {
        }

        public AirWayTicketBookContext(DbContextOptions<AirWayTicketBookContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BookingDetail> BookingDetails { get; set; } = null!;
        public virtual DbSet<Class> Classes { get; set; } = null!;
        public virtual DbSet<Economy> Economies { get; set; } = null!;
        public virtual DbSet<Flight> Flights { get; set; } = null!;
        public virtual DbSet<Login> Logins { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<Registration> Registrations { get; set; } = null!;
        public virtual DbSet<SearchHistory> SearchHistories { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=YKG-PC;Database=AirWayTicketBook;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookingDetail>(entity =>
            {
                entity.Property(e => e.PassengerName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.BookingDetails)
                    .HasForeignKey(d => d.PaymentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BookingDe__Payme__756D6ECB");
            });

            modelBuilder.Entity<Class>(entity =>
            {
                entity.Property(e => e.ClassId)
                    .ValueGeneratedNever()
                    .HasColumnName("Class_ID");

                entity.Property(e => e.BusinessId).HasColumnName("Business_ID");

                entity.Property(e => e.EconomyId).HasColumnName("Economy_ID");
            });

            modelBuilder.Entity<Economy>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Economy");

                entity.Property(e => e.EconomyId).HasColumnName("Economy_ID");

                entity.Property(e => e.TripType)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Flight>(entity =>
            {
                entity.Property(e => e.FlightId).HasColumnName("Flight_ID");

                entity.Property(e => e.Charges).HasColumnType("money");

                entity.Property(e => e.DateOfArriving).HasColumnType("date");

                entity.Property(e => e.DateOfDepature).HasColumnType("date");

                entity.Property(e => e.GoingTo)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Going_to");

                entity.Property(e => e.LeaveFrom)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Leave_from");

                entity.Property(e => e.Rate).HasColumnType("money");

                entity.Property(e => e.Timing).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.HasKey(e => e.LoginId)
                    .HasName("PK__Login__D78868677583E186");

                entity.ToTable("Login");

                entity.Property(e => e.LoginId).HasColumnName("Login_ID");

                entity.Property(e => e.EmailId)
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Password)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdNavigation)
                    .WithMany(p => p.Logins)
                    .HasForeignKey(d => d.Id)
                    .HasConstraintName("FK__Login__ID__160F4887");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payment");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.PaiedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Payee)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentDetails)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Flight)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.FlightId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Payment__FlightI__6DCC4D03");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Payment__UserId__6EC0713C");
            });

            modelBuilder.Entity<Registration>(entity =>
            {
                entity.ToTable("Registration");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ConfirmPassword)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(8)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SearchHistory>(entity =>
            {
                entity.ToTable("SearchHistory");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DepatureDate).HasColumnType("date");

                entity.Property(e => e.Destination)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FromSource)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ReturnTripDate).HasColumnType("date");

                entity.Property(e => e.TripType).HasDefaultValueSql("((0))");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
