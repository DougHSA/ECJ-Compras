using System;
using System.Collections.Generic;
using Dominio.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Dominio.Context;

public partial class EjcContext : DbContext
{
    public EjcContext()
    {
    }
    
    public EjcContext(DbContextOptions<EjcContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Doacao> Doacoes { get; set; }

    public virtual DbSet<Pessoa> Pessoas { get; set; }

    public virtual DbSet<Produto> Produtos { get; set; }

    public virtual DbSet<Transacao> Transacoes { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //=> optionsBuilder.UseSqlServer("workstation id=ejc-compras.mssql.somee.com;packet size=4096;user id=EJC-Compras_SQLLogin_1;pwd=ea177ec8yu;data source=ejc-compras.mssql.somee.com;persist security info=False;initial catalog=ejc-compras;TrustServerCertificate=true;");
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-J4P1J3V;Database=EJC;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Doacao>(entity =>
        {
            entity.Property(e => e.Data).HasColumnType("datetime");

            entity.HasOne(d => d.Pessoa).WithMany(p => p.Doacoes)
                .HasForeignKey(d => d.IdPessoa)
                .HasConstraintName("FK_Doacoes_Pessoas");

            entity.HasOne(d => d.Produto).WithMany(p => p.Doacoes)
                .HasForeignKey(d => d.IdProduto)
                .HasConstraintName("FK_Doacoes_Produtos");
        });

        modelBuilder.Entity<Pessoa>(entity =>
        {
            entity.Property(e => e.Equipe)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Produto>(entity =>
        {
            entity.Property(e => e.Categoria)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Unidade)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Transacao>(entity =>
        {
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

            entity.HasOne(d => d.Usuario).WithMany(p => p.Transacoes)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade)
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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
