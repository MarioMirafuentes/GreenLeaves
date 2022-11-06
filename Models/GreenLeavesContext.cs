using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace GreenLeaves.Models
{
    public partial class GreenLeavesContext : DbContext
    {
        public GreenLeavesContext()
        {
        }

        public GreenLeavesContext(DbContextOptions<GreenLeavesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Contacto> Contactos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=74.208.42.188;database=GreenLeaves; user=sa; pwd=R3v35a20.");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contacto>(entity =>
            {
                entity.HasKey(e => e.IdContact);

                entity.Property(e => e.IdContact).HasColumnName("Id_Contact");

                entity.Property(e => e.Ciudad)
                    .HasMaxLength(350)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Fecha)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
