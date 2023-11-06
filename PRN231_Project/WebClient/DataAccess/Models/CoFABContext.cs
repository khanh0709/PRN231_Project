using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CoFAB.DataAccess.Models
{
    public partial class CoFABContext : DbContext
    {
        public CoFABContext()
        {
        }

        public CoFABContext(DbContextOptions<CoFABContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Attemp> Attemps { get; set; } = null!;
        public virtual DbSet<Format> Formats { get; set; } = null!;
        public virtual DbSet<Match> Matches { get; set; } = null!;
        public virtual DbSet<Round> Rounds { get; set; } = null!;
        public virtual DbSet<Tournament> Tournaments { get; set; } = null!;
        public virtual DbSet<Type> Types { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                                          .SetBasePath(Directory.GetCurrentDirectory())
                                          .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DB"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attemp>(entity =>
            {
                entity.ToTable("Attemp");
                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Xpgained).HasColumnName("XPGained");
                entity.HasOne(d => d.Tournament)
                    .WithMany(p => p.Attemps)
                    .HasForeignKey(d => d.TournamentId)
                    .HasConstraintName("FK__Attemp__Tourname__3F466844");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Attemps)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Attemp__UserId__3E52440B");
            });

            modelBuilder.Entity<Format>(entity =>
            {
                entity.ToTable("Format");
            });
            modelBuilder.Entity<Match>(entity =>
            {
                entity.ToTable("Match");
                entity.HasOne(d => d.Player1Navigation)
                    .WithMany(p => p.MatchPlayer1Navigations)
                    .HasForeignKey(d => d.Player1)
                    .HasConstraintName("FK__Match__Player1__4F7CD00D");
                entity.HasOne(d => d.Player2Navigation)
                    .WithMany(p => p.MatchPlayer2Navigations)
                    .HasForeignKey(d => d.Player2)
                    .HasConstraintName("FK__Match__Player2__5070F446");
                entity.HasOne(d => d.Round)
                    .WithMany(p => p.Matches)
                    .HasForeignKey(d => d.RoundId)
                    .HasConstraintName("FK__Match__RoundId__5165187F");

                entity.HasOne(d => d.WinerNavigation)
                    .WithMany(p => p.MatchWinerNavigations)
                    .HasForeignKey(d => d.Winer)
                    .HasConstraintName("FK__Match__Winer__52593CB8");
            });

            modelBuilder.Entity<Round>(entity =>
            {
                entity.ToTable("Round");
                entity.Property(e => e.RoundName).HasMaxLength(200);
                entity.HasOne(d => d.Tournament)
                    .WithMany(p => p.Rounds)
                    .HasForeignKey(d => d.TournamentId)
                    .HasConstraintName("FK__Round__Tournamen__4CA06362")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Tournament>(entity =>
            {
                entity.ToTable("Tournament");
                entity.Property(e => e.StartTime).HasColumnType("datetime");
                entity.Property(e => e.Xpmodifier).HasColumnName("XPModifier");
                entity.HasOne(d => d.Format)
                    .WithMany(p => p.Tournaments)
                    .HasForeignKey(d => d.FormatId)
                    .HasConstraintName("FK__Tournamen__Forma__5FB337D6");
                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Tournaments)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK__Tournamen__TypeI__5EBF139D");
                entity.HasOne(d => d.User)
                    .WithMany(p => p.Tournaments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Tournamen__UserI__3B75D760");
            });

            modelBuilder.Entity<Type>(entity =>
            {
                entity.ToTable("Type");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");
                entity.Property(e => e.Account)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.Authorization).HasMaxLength(100);
                entity.Property(e => e.City).HasMaxLength(100);
                entity.Property(e => e.Password).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}