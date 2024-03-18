﻿// <auto-generated />
using System;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MilesCarRental.Migrations
{
    [DbContext(typeof(CarRentalContext))]
    partial class CarRentalContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Entities.LocationEntity", b =>
                {
                    b.Property<Guid>("LocationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsDestination")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsOrigin")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LocationName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.Property<DateTime>("Registration")
                        .HasColumnType("datetime(6)");

                    b.HasKey("LocationId");

                    b.ToTable("Locations");

                    b.HasData(
                        new
                        {
                            LocationId = new Guid("b29f780f-c2fc-41e0-a5e5-dab0090a4f09"),
                            IsDestination = false,
                            IsOrigin = false,
                            LocationName = "location",
                            Registration = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("Entities.LogEntity", b =>
                {
                    b.Property<Guid>("LogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("LocationDestination")
                        .HasMaxLength(50)
                        .HasColumnType("tinyint(50)");

                    b.Property<bool>("LocationOrigin")
                        .HasMaxLength(50)
                        .HasColumnType("tinyint(50)");

                    b.Property<DateTime>("Registration")
                        .HasColumnType("datetime(6)");

                    b.HasKey("LogId");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("Entities.VehicleEntity", b =>
                {
                    b.Property<Guid>("VehicleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("QuantityLargeSuitcases")
                        .HasColumnType("int");

                    b.Property<int>("QuantitySmallSuitcases")
                        .HasColumnType("int");

                    b.Property<string>("Transmission")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<int>("VehicleCapacity")
                        .HasColumnType("int");

                    b.Property<string>("VehicleName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("VehicleId");

                    b.ToTable("Vehicles");

                    b.HasData(
                        new
                        {
                            VehicleId = new Guid("2a4f2245-f6ab-4626-bf5f-c16216dd6822"),
                            IsAvailable = true,
                            QuantityLargeSuitcases = 2,
                            QuantitySmallSuitcases = 1,
                            Transmission = "Automática",
                            VehicleCapacity = 5,
                            VehicleName = "Hyundai Accent"
                        },
                        new
                        {
                            VehicleId = new Guid("0a2d473b-fe62-4f66-9537-96848c6d5c4b"),
                            IsAvailable = true,
                            QuantityLargeSuitcases = 1,
                            QuantitySmallSuitcases = 1,
                            Transmission = "Manual",
                            VehicleCapacity = 5,
                            VehicleName = "Kia Picanto"
                        },
                        new
                        {
                            VehicleId = new Guid("b1820ce5-f0a1-4923-b362-b10ebddc7ad4"),
                            IsAvailable = true,
                            QuantityLargeSuitcases = 1,
                            QuantitySmallSuitcases = 1,
                            Transmission = "Manual",
                            VehicleCapacity = 5,
                            VehicleName = "Suzuki Swift"
                        },
                        new
                        {
                            VehicleId = new Guid("74180e03-50cb-4bf3-b645-ef538f769493"),
                            IsAvailable = true,
                            QuantityLargeSuitcases = 3,
                            QuantitySmallSuitcases = 0,
                            Transmission = "Manual",
                            VehicleCapacity = 5,
                            VehicleName = "Renault Duster"
                        },
                        new
                        {
                            VehicleId = new Guid("7cb616c3-d5d1-4d34-8dd5-d83e51ea3fa8"),
                            IsAvailable = true,
                            QuantityLargeSuitcases = 1,
                            QuantitySmallSuitcases = 1,
                            Transmission = "Automática",
                            VehicleCapacity = 5,
                            VehicleName = "Changan E-Sta"
                        },
                        new
                        {
                            VehicleId = new Guid("d70f3c46-0055-4b30-946a-98486ec75993"),
                            IsAvailable = true,
                            QuantityLargeSuitcases = 3,
                            QuantitySmallSuitcases = 0,
                            Transmission = "Automática",
                            VehicleCapacity = 5,
                            VehicleName = "Renault Koleos"
                        },
                        new
                        {
                            VehicleId = new Guid("0c4c3455-4330-4e35-b0a7-9ec7e42c6490"),
                            IsAvailable = true,
                            QuantityLargeSuitcases = 1,
                            QuantitySmallSuitcases = 1,
                            Transmission = "Automática",
                            VehicleCapacity = 5,
                            VehicleName = "Hyundai Creta"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
