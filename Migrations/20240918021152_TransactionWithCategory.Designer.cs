﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace coin_api.Migrations
{
    [DbContext(typeof(ConnectionContext))]
    [Migration("20240918021152_TransactionWithCategory")]
    partial class TransactionWithCategory
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TransactionModel", b =>
                {
                    b.Property<int>("IdTransaction")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdTransaction"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("Type")
                        .HasColumnType("boolean");

                    b.Property<double>("Value")
                        .HasColumnType("double precision");

                    b.HasKey("IdTransaction");

                    b.ToTable("transaction");
                });

            modelBuilder.Entity("coin_api.Domain.Model.CategoryModel", b =>
                {
                    b.Property<int>("IdCategory")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdCategory"));

                    b.Property<string>("Category")
                        .HasColumnType("text");

                    b.Property<int>("TransactionModelId")
                        .HasColumnType("integer");

                    b.HasKey("IdCategory");

                    b.HasIndex("TransactionModelId")
                        .IsUnique();

                    b.ToTable("category");
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

                    b.Property<DateTime>("expiraToken")
                        .HasColumnType("date");

                    b.Property<byte[]>("senha")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<byte[]>("senhaHash")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("token")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("coin_api.Domain.Model.CategoryModel", b =>
                {
                    b.HasOne("TransactionModel", "TransactionModel")
                        .WithOne("Category")
                        .HasForeignKey("coin_api.Domain.Model.CategoryModel", "TransactionModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TransactionModel");
                });

            modelBuilder.Entity("TransactionModel", b =>
                {
                    b.Navigation("Category");
                });
#pragma warning restore 612, 618
        }
    }
}
