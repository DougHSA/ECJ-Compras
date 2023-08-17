using System;
using System.Collections.Generic;
using Dominio.Models;
using Microsoft.EntityFrameworkCore;

namespace Dominio.Context;

public partial class EjcContextteste : DbContext
{
    public EjcContextteste()
    {
    }

    public EjcContextteste(DbContextOptions<EjcContextteste> options)
        : base(options)
    {
    }

    public virtual DbSet<Transacao> Transacoes { get; set; }
    public virtual DbSet<Pessoa> Pessoas { get; set; }
    public virtual DbSet<Doacao> Doacoes { get; set; }
    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-J4P1J3V;Database=EJC;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transacao>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Data).HasColumnType("datetime");
            entity.Property(e => e.Descricao)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.MetodoPagamento)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Tipo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Valor).HasColumnType("decimal(20, 2)");

            entity.HasOne(d => d.Usuario)
                .WithMany(u => u.Transacoes)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transacoes_Usuarios");
                });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.Property(e => e.Login)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nivel)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Senha)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Doacao>(entity =>
        {
            //entity.Property(e => e.Id).ValueGeneratedOnAdd();
            //entity.Property(e => e.Data).HasColumnType("datetime");
            //entity.Property(e => e.Descricao)
            //    .HasMaxLength(100)
            //    .IsUnicode(false);
            //entity.Property(e => e.MetodoPagamento)
            //    .HasMaxLength(20)
            //    .IsUnicode(false);
            //entity.Property(e => e.Tipo)
            //    .HasMaxLength(20)
            //    .IsUnicode(false);
            //entity.Property(e => e.Valor).HasColumnType("decimal(20, 2)");

            //entity.HasOne(d => d.Usuario)
            //    .WithMany(u => u.Transacoes)
            //    .HasForeignKey(d => d.IdUsuario)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_Transacoes_Usuarios");
        });

        modelBuilder.Entity<Pessoa>(entity =>
        {
            //entity.Property(e => e.Login)
            //    .HasMaxLength(100)
            //    .IsUnicode(false);
            //entity.Property(e => e.Nivel)
            //    .HasMaxLength(20)
            //    .IsUnicode(false);
            //entity.Property(e => e.Nome)
            //    .HasMaxLength(100)
            //    .IsUnicode(false);
            //entity.Property(e => e.Senha)
            //    .HasMaxLength(100)
            //    .IsUnicode(false);
        });

        modelBuilder.Entity<Produto>(entity =>
        {
            //entity.Property(e => e.Id).ValueGeneratedOnAdd();
            //entity.Property(e => e.Id)
            //    .HasMaxLength(100)
            //    .IsUnicode(false);
            //entity.Property(e => e.Nivel)
            //    .HasMaxLength(20)
            //    .IsUnicode(false);
            //entity.Property(e => e.Nome)
            //    .HasMaxLength(100)
            //    .IsUnicode(false);
            //entity.Property(e => e.Senha)
            //    .HasMaxLength(100)
            //    .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
