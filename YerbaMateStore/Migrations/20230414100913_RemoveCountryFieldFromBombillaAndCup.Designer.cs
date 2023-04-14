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
    [Migration("20230414100913_RemoveCountryFieldFromBombillaAndCup")]
    partial class RemoveCountryFieldFromBombillaAndCup
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("YerbaMateStore.Models.Entities.Bombilla", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
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

                    b.ToTable("Bombilla");
                });

            modelBuilder.Entity("YerbaMateStore.Models.Entities.BombillaImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("BombillaImages");
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

            modelBuilder.Entity("YerbaMateStore.Models.Entities.Cup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
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

                    b.ToTable("Cup");
                });

            modelBuilder.Entity("YerbaMateStore.Models.Entities.CupImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("CupImages");
                });

            modelBuilder.Entity("YerbaMateStore.Models.Entities.YerbaMate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Composition")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

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
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<double>("Price")
                        .HasPrecision(10, 2)
                        .HasColumnType("double");

                    b.Property<string>("Weight")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("YerbaMate");
                });

            modelBuilder.Entity("YerbaMateStore.Models.Entities.YerbaMateImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("YerbaMateImages");
                });

            modelBuilder.Entity("YerbaMateStore.Models.Entities.BombillaImage", b =>
                {
                    b.HasOne("YerbaMateStore.Models.Entities.Bombilla", "Product")
                        .WithMany("Images")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("YerbaMateStore.Models.Entities.CupImage", b =>
                {
                    b.HasOne("YerbaMateStore.Models.Entities.Cup", "Product")
                        .WithMany("Images")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("YerbaMateStore.Models.Entities.YerbaMate", b =>
                {
                    b.HasOne("YerbaMateStore.Models.Entities.Country", "Country")
                        .WithMany("YerbaMate")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("YerbaMateStore.Models.Entities.YerbaMateImage", b =>
                {
                    b.HasOne("YerbaMateStore.Models.Entities.YerbaMate", "Product")
                        .WithMany("Images")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("YerbaMateStore.Models.Entities.Bombilla", b =>
                {
                    b.Navigation("Images");
                });

            modelBuilder.Entity("YerbaMateStore.Models.Entities.Country", b =>
                {
                    b.Navigation("YerbaMate");
                });

            modelBuilder.Entity("YerbaMateStore.Models.Entities.Cup", b =>
                {
                    b.Navigation("Images");
                });

            modelBuilder.Entity("YerbaMateStore.Models.Entities.YerbaMate", b =>
                {
                    b.Navigation("Images");
                });
#pragma warning restore 612, 618
        }
    }
}
