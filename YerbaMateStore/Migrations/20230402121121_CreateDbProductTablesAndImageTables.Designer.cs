﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using YerbaMateStore.Models.DataAccess;

#nullable disable

namespace YerbaMateStore.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230402121121_CreateDbProductTablesAndImageTables")]
    partial class CreateDbProductTablesAndImageTables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("YerbaMateStore.Models.Entities.BombillaImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("BombillaImages");
                });

            modelBuilder.Entity("YerbaMateStore.Models.Entities.BombillaProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<double?>("DiscountPrice")
                        .HasPrecision(10, 2)
                        .HasColumnType("double");

                    b.Property<bool>("IsUnscrewed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Length")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Material")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<double>("Price")
                        .HasPrecision(10, 2)
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("BombillaProducts");
                });

            modelBuilder.Entity("YerbaMateStore.Models.Entities.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CountryIsoAlfa2Code")
                        .HasMaxLength(2)
                        .HasColumnType("varchar(2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("YerbaMateStore.Models.Entities.CupImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("CupImages");
                });

            modelBuilder.Entity("YerbaMateStore.Models.Entities.CupProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<double?>("DiscountPrice")
                        .HasPrecision(10, 2)
                        .HasColumnType("double");

                    b.Property<string>("Material")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<double>("Price")
                        .HasPrecision(10, 2)
                        .HasColumnType("double");

                    b.Property<string>("Volume")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("CupProducts");
                });

            modelBuilder.Entity("YerbaMateStore.Models.Entities.YerbaMateImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("YerbaMateImages");
                });

            modelBuilder.Entity("YerbaMateStore.Models.Entities.YerbaMateProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Composition")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<double?>("DiscountPrice")
                        .HasPrecision(10, 2)
                        .HasColumnType("double");

                    b.Property<bool>("HasAdditives")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<double>("Price")
                        .HasPrecision(10, 2)
                        .HasColumnType("double");

                    b.Property<string>("Weight")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("YerbaMateProducts");
                });

            modelBuilder.Entity("YerbaMateStore.Models.Entities.BombillaImage", b =>
                {
                    b.HasOne("YerbaMateStore.Models.Entities.BombillaProduct", "Product")
                        .WithMany("Images")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("YerbaMateStore.Models.Entities.BombillaProduct", b =>
                {
                    b.HasOne("YerbaMateStore.Models.Entities.Country", "Country")
                        .WithMany("BombillaProducts")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("YerbaMateStore.Models.Entities.CupImage", b =>
                {
                    b.HasOne("YerbaMateStore.Models.Entities.CupProduct", "Product")
                        .WithMany("Images")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("YerbaMateStore.Models.Entities.CupProduct", b =>
                {
                    b.HasOne("YerbaMateStore.Models.Entities.Country", "Country")
                        .WithMany("CupProducts")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("YerbaMateStore.Models.Entities.YerbaMateImage", b =>
                {
                    b.HasOne("YerbaMateStore.Models.Entities.YerbaMateProduct", "Product")
                        .WithMany("Images")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("YerbaMateStore.Models.Entities.YerbaMateProduct", b =>
                {
                    b.HasOne("YerbaMateStore.Models.Entities.Country", "Country")
                        .WithMany("YerbaMateProducts")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("YerbaMateStore.Models.Entities.BombillaProduct", b =>
                {
                    b.Navigation("Images");
                });

            modelBuilder.Entity("YerbaMateStore.Models.Entities.Country", b =>
                {
                    b.Navigation("BombillaProducts");

                    b.Navigation("CupProducts");

                    b.Navigation("YerbaMateProducts");
                });

            modelBuilder.Entity("YerbaMateStore.Models.Entities.CupProduct", b =>
                {
                    b.Navigation("Images");
                });

            modelBuilder.Entity("YerbaMateStore.Models.Entities.YerbaMateProduct", b =>
                {
                    b.Navigation("Images");
                });
#pragma warning restore 612, 618
        }
    }
}
