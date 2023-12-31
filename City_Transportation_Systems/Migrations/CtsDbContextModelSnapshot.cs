﻿// <auto-generated />
using System;
using City_Transportation_Systems.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace City_Transportation_Systems.Migrations
{
    [DbContext(typeof(CtsDbContext))]
    partial class CtsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("City_Transportation_Systems.Models.Bus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RouteId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RouteId");

                    b.ToTable("Buses");
                });

            modelBuilder.Entity("City_Transportation_Systems.Models.Driver", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BusId")
                        .HasColumnType("int");

                    b.Property<string>("First_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Last_name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BusId");

                    b.ToTable("Drivers");
                });

            modelBuilder.Entity("City_Transportation_Systems.Models.Route", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Routes");
                });

            modelBuilder.Entity("City_Transportation_Systems.Models.Schedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("RouteId")
                        .HasColumnType("int");

                    b.Property<int>("StationId")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("TimeStamp")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.HasIndex("RouteId");

                    b.HasIndex("StationId");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("City_Transportation_Systems.Models.Station", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Stations");
                });

            modelBuilder.Entity("City_Transportation_Systems.Models.Bus", b =>
                {
                    b.HasOne("City_Transportation_Systems.Models.Route", "Route")
                        .WithMany()
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Route");
                });

            modelBuilder.Entity("City_Transportation_Systems.Models.Driver", b =>
                {
                    b.HasOne("City_Transportation_Systems.Models.Bus", "Bus")
                        .WithMany()
                        .HasForeignKey("BusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bus");
                });

            modelBuilder.Entity("City_Transportation_Systems.Models.Schedule", b =>
                {
                    b.HasOne("City_Transportation_Systems.Models.Route", "Route")
                        .WithMany("Schedules")
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("City_Transportation_Systems.Models.Station", "Station")
                        .WithMany("Schedules")
                        .HasForeignKey("StationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Route");

                    b.Navigation("Station");
                });

            modelBuilder.Entity("City_Transportation_Systems.Models.Route", b =>
                {
                    b.Navigation("Schedules");
                });

            modelBuilder.Entity("City_Transportation_Systems.Models.Station", b =>
                {
                    b.Navigation("Schedules");
                });
#pragma warning restore 612, 618
        }
    }
}
