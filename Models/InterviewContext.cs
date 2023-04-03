using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Models
{
    public class InterviewContext : DbContext
    {
        public InterviewContext() : base()
        {

        }
        public DbSet<Companies> Companies { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Rates> Rates { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<CDR> CDRs { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=MOTAZ_PC\\SQLEXPRESS;Database=Interview;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure CDR table
            modelBuilder.Entity<CDR>(entity =>
            {
                entity.HasKey(e => e.CarrierReference);
                entity.Property(e => e.ConnectDateTime).HasColumnType("datetime2");
                entity.Property(e => e.Duration).HasColumnType("int");
                entity.Property(e => e.SourceNumber).HasMaxLength(50);
                entity.Property(e => e.DestinationNumber).HasMaxLength(50);
                entity.Property(e => e.Direction).HasMaxLength(50);
            });

            // Configure Users table
            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.Name).HasMaxLength(50);
                entity.Property(e => e.EmailAddress).HasMaxLength(50);
                entity.Property(e => e.PhoneNumber).HasMaxLength(50);
                entity.HasOne(e => e.Company)
                      .WithMany(c => c.Users)
                      .HasForeignKey(e => e.CompanyID)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_Users_Companies");
            });

            // Configure Rates table
            modelBuilder.Entity<Rates>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.Name).HasMaxLength(50);
                entity.Property(e => e.RateType).HasMaxLength(50);
                entity.Property(e => e.Priority).HasMaxLength(50);
                entity.Property(e => e.Filter).HasMaxLength(50);
                entity.HasOne(e => e.Plan)
                      .WithMany(p => p.Rates)
                      .HasForeignKey(e => e.PlanID)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_Rates_Plans");
            });

            // Configure Plan table
            modelBuilder.Entity<Plan>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.Name).HasMaxLength(50);
                entity.HasMany(e => e.Companies)
                      .WithOne(c => c.Plan)
                      .HasForeignKey(e => e.PlanID)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_Companies_Plans");
            });
            // Configure Plan table
            
            modelBuilder.Entity<Companies>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.PlanID)
                    .IsRequired();
            });
        }

    }
}
