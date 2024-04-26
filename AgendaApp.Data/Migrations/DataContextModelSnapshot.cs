﻿// <auto-generated />
using System;
using AgendaApp.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AgendaApp.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AgendaApp.Data.Entities.Tarefa", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID");

                    b.Property<DateTime?>("DataHora")
                        .IsRequired()
                        .HasColumnType("datetime2")
                        .HasColumnName("DATAHORA");

                    b.Property<DateTime?>("DataHoraCadastro")
                        .IsRequired()
                        .HasColumnType("datetime2")
                        .HasColumnName("DATAHORACADASTRO");

                    b.Property<DateTime?>("DataHoraUltimaAtualizacao")
                        .IsRequired()
                        .HasColumnType("datetime2")
                        .HasColumnName("DATAHORAULTIMAATUALIZACAO");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("DESCRICAO");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("NOME");

                    b.Property<int>("Prioridade")
                        .HasColumnType("int")
                        .HasColumnName("PRIORIDADE");

                    b.Property<int?>("Status")
                        .IsRequired()
                        .HasColumnType("int")
                        .HasColumnName("STATUS");

                    b.HasKey("Id");

                    b.ToTable("TAREFA", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
