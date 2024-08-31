﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace coin_api.Migrations
{
    [DbContext(typeof(ConnectionContext))]
    partial class ConnectionContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Transaction", b =>
                {
                    b.Property<int>("idTransaction")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("idTransaction"));

                    b.Property<string>("Category")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<double>("Value")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("type")
                        .HasColumnType("boolean");

                    b.HasKey("idTransaction");

                    b.ToTable("transaction");
                });

            modelBuilder.Entity("coin_api.Domain.Model.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("expiraToken")
                        .HasColumnType("timestamp with time zone");

                    b.Property<byte[]>("senha")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<byte[]>("senhaHash")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("token")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("users");
                });
#pragma warning restore 612, 618
        }
    }
}
