﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RestaurantOpdracht_EF.Model;

#nullable disable

namespace RestaurantOpdracht_EF.Migrations
{
    [DbContext(typeof(RestaurantContext))]
    [Migration("20231208195023_initDB")]
    partial class initDB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RestaurantOpdracht_EF.Model.KlantEF", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Gemeentenaam")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("HuisNr")
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Postcode")
                        .HasMaxLength(4)
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("Straatnaam")
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Tel")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Klant");
                });

            modelBuilder.Entity("RestaurantOpdracht_EF.Model.ReservatieEF", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("AantalPlaatsen")
                        .HasColumnType("int");

                    b.Property<DateTime>("Datum")
                        .HasColumnType("datetime2");

                    b.Property<int>("KlantID")
                        .HasColumnType("int");

                    b.Property<int>("RestaurantID")
                        .HasColumnType("int");

                    b.Property<int>("TafelNr")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("KlantID");

                    b.HasIndex("RestaurantID");

                    b.ToTable("Reservatie");
                });

            modelBuilder.Entity("RestaurantOpdracht_EF.Model.RestaurantEF", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Gemeentenaam")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("HuisNr")
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("Keuken")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Postcode")
                        .HasMaxLength(4)
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("Straatnaam")
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Tel")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Restaurant");
                });

            modelBuilder.Entity("RestaurantOpdracht_EF.Model.TafelEF", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("AantalPlaatsen")
                        .HasColumnType("int");

                    b.Property<int>("RestaurantID")
                        .HasColumnType("int");

                    b.Property<int>("TafelNr")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("RestaurantID");

                    b.ToTable("Tafel");
                });

            modelBuilder.Entity("RestaurantOpdracht_EF.Model.ReservatieEF", b =>
                {
                    b.HasOne("RestaurantOpdracht_EF.Model.KlantEF", "Klant")
                        .WithMany("Reservaties")
                        .HasForeignKey("KlantID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("RestaurantOpdracht_EF.Model.RestaurantEF", "Restaurant")
                        .WithMany("Reservaties")
                        .HasForeignKey("RestaurantID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Klant");

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("RestaurantOpdracht_EF.Model.TafelEF", b =>
                {
                    b.HasOne("RestaurantOpdracht_EF.Model.RestaurantEF", "Restaurant")
                        .WithMany("Tafels")
                        .HasForeignKey("RestaurantID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("RestaurantOpdracht_EF.Model.KlantEF", b =>
                {
                    b.Navigation("Reservaties");
                });

            modelBuilder.Entity("RestaurantOpdracht_EF.Model.RestaurantEF", b =>
                {
                    b.Navigation("Reservaties");

                    b.Navigation("Tafels");
                });
#pragma warning restore 612, 618
        }
    }
}
