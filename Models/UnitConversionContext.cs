using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace aYoUnitConversion.Models
{
    public partial class UnitConversionContext : DbContext
    {
        public UnitConversionContext()
        {
        }

        public UnitConversionContext(DbContextOptions<UnitConversionContext> options)
            : base(options)
        {
        }

        public virtual DbSet<UnitConversionFactor> UnitConversionFactors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=UnitConversion;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<UnitConversionFactor>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ConversionUnit)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SourceUnit)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
