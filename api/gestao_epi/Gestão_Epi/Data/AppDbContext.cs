using System;
using System.Collections.Generic;
using Gestão_Epi.Entities;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace Gestão_Epi.Data;

public partial class AppDbContext : DbContext
{
 

    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Colaborador> colaborador { get; set; }

    public virtual DbSet<Epi> epi { get; set; }

    public virtual DbSet<Estoque> estoque { get; set; }

    public virtual DbSet<Notificacao> notificacao { get; set; }

    public virtual DbSet<Perfil> perfil { get; set; } = null!;

    public virtual DbSet<Permissao> permissao { get; set; }

    public virtual DbSet<Retirada_devolucao> retirada_devolucao { get; set; }

    public virtual DbSet<Usuario> usuario { get; set; } = null!;

    public virtual DbSet<Visitante> visitante { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("name=DefaultConnection", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.42-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Colaborador>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.Property(e => e.nome).HasMaxLength(100);
            entity.Property(e => e.setor).HasMaxLength(100);
            entity.Property(e => e.matricula).HasMaxLength(30);
        });

        modelBuilder.Entity<Epi>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.Property(e => e.nome).HasMaxLength(100);
            entity.Property(c => c.ca).HasColumnType("int");
            entity.Property(t => t.tamanho).HasMaxLength(10);
            entity.Property(v => v.validade).HasColumnType("date");
            entity.Property(d => d.descricao).HasMaxLength(1000);

        });

        modelBuilder.Entity<Estoque>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.HasIndex(e => e.epi_id, "epi_id").IsUnique();

            entity.HasOne(d => d.epi).WithOne(p => p.estoque)
                .HasForeignKey<Estoque>(d => d.epi_id)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("estoque_ibfk_1");
        });

        modelBuilder.Entity<Notificacao>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.HasIndex(e => e.colaborador_id, "colaborador_id");

            entity.HasIndex(e => e.retirada_id, "retirada_id").IsUnique();

            entity.HasIndex(e => e.visitante_id, "visitante_id");

            entity.Property(e => e.data_limite).HasColumnType("datetime");
            entity.Property(e => e.mensagem).HasMaxLength(50);
            entity.Property(e => e.status)
                .HasMaxLength(30)
                .HasDefaultValueSql("'PENDENTE'");

            entity.HasOne(d => d.colaborador).WithMany(p => p.notificacao)
                .HasForeignKey(d => d.colaborador_id)
                .HasConstraintName("notificacao_ibfk_1");

            entity.HasOne(d => d.retirada).WithOne(p => p.notificacao)
                .HasForeignKey<Notificacao>(d => d.retirada_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("notificacao_ibfk_3");

            entity.HasOne(d => d.visitante).WithMany(p => p.notificacao)
                .HasForeignKey(d => d.visitante_id)
                .HasConstraintName("notificacao_ibfk_2");
        });

        modelBuilder.Entity<Perfil>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.Property(e => e.descricao).HasMaxLength(50);
            entity.Property(e => e.nome).HasMaxLength(50);

            entity.HasMany(d => d.permissao).WithMany(p => p.perfil)
                .UsingEntity<Dictionary<string, object>>(
                    "perfil_permissao",
                    r => r.HasOne<Permissao>().WithMany()
                        .HasForeignKey("permissao_id")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("perfil_permissao_ibfk_2"),
                    l => l.HasOne<Perfil>().WithMany()
                        .HasForeignKey("perfil_id")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("perfil_permissao_ibfk_1"),
                    j =>
                    {
                        j.HasKey("perfil_id", "permissao_id")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.HasIndex(new[] { "permissao_id" }, "permissao_id");
                    });
        });

        modelBuilder.Entity<Permissao>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.HasIndex(e => e.codigo, "codigo").IsUnique();

            entity.Property(e => e.codigo).HasMaxLength(50);
            entity.Property(e => e.descricao).HasMaxLength(100);
        });

        modelBuilder.Entity<Retirada_devolucao>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.HasIndex(e => e.colaborador_id, "colaborador_id");

            entity.HasIndex(e => e.epi_id, "epi_id");

            entity.HasIndex(e => e.usuario_id, "usuario_id");

            entity.HasIndex(e => e.visitante_id, "visitante_id");

            entity.Property(e => e.data_devolucao).HasColumnType("datetime");
            entity.Property(e => e.data_retirada).HasColumnType("datetime");
            entity.Property(e => e.justificativa_devolucao).HasMaxLength(200);
            entity.Property(e => e.justificativa_retirada).HasMaxLength(200);

            entity.HasOne(d => d.colaborador).WithMany(p => p.retirada_devolucao)
                .HasForeignKey(d => d.colaborador_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("retirada_devolucao_ibfk_3");

            entity.HasOne(d => d.epi).WithMany(p => p.retirada_devolucao)
                .HasForeignKey(d => d.epi_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("retirada_devolucao_ibfk_2");

            entity.HasOne(d => d.usuario).WithMany(p => p.retirada_devolucao)
                .HasForeignKey(d => d.usuario_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("retirada_devolucao_ibfk_1");

            entity.HasOne(d => d.visitante).WithMany(p => p.retirada_devolucao)
                .HasForeignKey(d => d.visitante_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("retirada_devolucao_ibfk_4");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.HasIndex(e => e.perfil_id, "fk_perfil");

            entity.Property(e => e.email).HasMaxLength(150);
            entity.Property(e => e.nome).HasMaxLength(100);
            entity.Property(e => e.senhaHash).HasColumnType("VARBINARY(64)").IsRequired();
            entity.Property(e => e.senhaSalt).HasColumnType("VARBINARY(128)").IsRequired();

            entity.HasOne(d => d.perfil).WithMany(p => p.usuario)
                .HasForeignKey(d => d.perfil_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_perfil");
        });

        modelBuilder.Entity<Visitante>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.Property<string>("nome")
              .HasColumnName("nome")
              .HasMaxLength(100)
              .IsRequired();

            entity.Property<string>("documento")
              .HasColumnName("documento")
              .HasMaxLength(11)
              .IsRequired();

            
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
