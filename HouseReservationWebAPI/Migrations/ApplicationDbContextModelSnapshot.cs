﻿// <auto-generated />
using System;
using HouseReservationWebAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HouseReservationWebAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HouseReservationWebAPI.Models.House", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Amenity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Area")
                        .HasColumnType("float");

                    b.Property<double>("ChargeRate")
                        .HasColumnType("float");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("DetailedInfo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Occupancy")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Houses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amenity = "",
                            Area = 0.0,
                            ChargeRate = 500.0,
                            CreatedAt = new DateTime(2023, 4, 9, 21, 59, 31, 256, DateTimeKind.Local).AddTicks(106),
                            DetailedInfo = "Some detailed info about the house",
                            ImageUrl = "",
                            Name = "Luxury House",
                            Occupancy = 0,
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            Amenity = "",
                            Area = 0.0,
                            ChargeRate = 300.0,
                            CreatedAt = new DateTime(2023, 4, 9, 21, 59, 31, 256, DateTimeKind.Local).AddTicks(128),
                            DetailedInfo = "Some detailed info about the house",
                            ImageUrl = "",
                            Name = "Standard House",
                            Occupancy = 0,
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            Amenity = "",
                            Area = 0.0,
                            ChargeRate = 400.0,
                            CreatedAt = new DateTime(2023, 4, 9, 21, 59, 31, 256, DateTimeKind.Local).AddTicks(130),
                            DetailedInfo = "Some detailed info about the house",
                            ImageUrl = "",
                            Name = "Forest House",
                            Occupancy = 0,
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
