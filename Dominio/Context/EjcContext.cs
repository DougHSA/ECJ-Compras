using System;
using System.Collections.Generic;
using Dominio.Models;
using Microsoft.EntityFrameworkCore;

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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-J4P1J3V;Database=EJC;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Doacao>(entity =>
        {
            entity.Property(e => e.Data).HasColumnType("datetime");

            entity.HasOne(d => d.IdPessoaNavigation).WithMany(p => p.Doacoes)
                .HasForeignKey(d => d.IdPessoa)
                .HasConstraintName("FK_Doacoes_Pessoas");

            entity.HasOne(d => d.IdProdutoNavigation).WithMany(p => p.Doacoes)
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
            entity.Property(e => e.Pontos)
                .HasMaxLength(10)
                .IsFixedLength();
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
