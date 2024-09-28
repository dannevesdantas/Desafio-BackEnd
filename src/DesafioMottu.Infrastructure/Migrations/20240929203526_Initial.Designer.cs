﻿// <auto-generated />
using System;
using System.Collections.Generic;
using DesafioMottu.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DesafioMottu.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240929203526_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DesafioMottu.Domain.DriversLicense.DriversLicense", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("number");

                    b.Property<List<char>>("Types")
                        .IsRequired()
                        .HasColumnType("character(1)[]")
                        .HasColumnName("types");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("Id")
                        .HasName("pk_drivers_licenses");

                    b.HasIndex("Number")
                        .IsUnique()
                        .HasDatabaseName("ix_drivers_licenses_number");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasDatabaseName("ix_drivers_licenses_user_id");

                    b.ToTable("drivers_licenses", (string)null);
                });

            modelBuilder.Entity("DesafioMottu.Domain.Rentals.Rental", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_on_utc");

                    b.Property<Guid>("MotoId")
                        .HasColumnType("uuid")
                        .HasColumnName("moto_id");

                    b.Property<DateTime>("PredictedEndDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("predicted_end_date");

                    b.Property<DateTime?>("ReturnedOnUtc")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("returned_on_utc");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("Id")
                        .HasName("pk_rentals");

                    b.HasIndex("MotoId")
                        .HasDatabaseName("ix_rentals_moto_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_rentals_user_id");

                    b.ToTable("rentals", (string)null);
                });

            modelBuilder.Entity("DesafioMottu.Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateOnly>("BirthDate")
                        .HasColumnType("date")
                        .HasColumnName("birth_date");

                    b.Property<string>("Cnpj")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("cnpj");

                    b.Property<int?>("DriversLicenseId")
                        .HasColumnType("integer")
                        .HasColumnName("drivers_license_id");

                    b.Property<string>("Email")
                        .HasMaxLength(400)
                        .HasColumnType("character varying(400)")
                        .HasColumnName("email");

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasIndex("Cnpj")
                        .IsUnique()
                        .HasDatabaseName("ix_users_cnpj");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("ix_users_email");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("DesafioMottu.Domain.Vehicles.Vehicle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime?>("LastRentedOnUtc")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("last_rented_on_utc");

                    b.Property<string>("LicensePlate")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("license_plate");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("model");

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.Property<int>("Year")
                        .HasColumnType("integer")
                        .HasColumnName("year");

                    b.HasKey("Id")
                        .HasName("pk_vehicles");

                    b.ToTable("vehicles", (string)null);
                });

            modelBuilder.Entity("DesafioMottu.Infrastructure.Outbox.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("jsonb")
                        .HasColumnName("content");

                    b.Property<string>("Error")
                        .HasColumnType("text")
                        .HasColumnName("error");

                    b.Property<DateTime>("OccurredOnUtc")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("occurred_on_utc");

                    b.Property<DateTime?>("ProcessedOnUtc")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("processed_on_utc");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("pk_outbox_messages");

                    b.ToTable("outbox_messages", (string)null);
                });

            modelBuilder.Entity("DesafioMottu.Domain.DriversLicense.DriversLicense", b =>
                {
                    b.HasOne("DesafioMottu.Domain.Users.User", "User")
                        .WithOne("DriversLicense")
                        .HasForeignKey("DesafioMottu.Domain.DriversLicense.DriversLicense", "UserId")
                        .HasConstraintName("fk_drivers_licenses_user_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DesafioMottu.Domain.Rentals.Rental", b =>
                {
                    b.HasOne("DesafioMottu.Domain.Vehicles.Vehicle", null)
                        .WithMany()
                        .HasForeignKey("MotoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_rentals_vehicle_moto_id");

                    b.HasOne("DesafioMottu.Domain.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_rentals_user_user_id");

                    b.OwnsOne("DesafioMottu.Domain.Rentals.DateRange", "Duration", b1 =>
                        {
                            b1.Property<Guid>("RentalId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<DateTime>("End")
                                .HasColumnType("timestamp without time zone")
                                .HasColumnName("duration_end");

                            b1.Property<DateTime>("Start")
                                .HasColumnType("timestamp without time zone")
                                .HasColumnName("duration_start");

                            b1.HasKey("RentalId");

                            b1.ToTable("rentals");

                            b1.WithOwner()
                                .HasForeignKey("RentalId")
                                .HasConstraintName("fk_rentals_rentals_id");
                        });

                    b.OwnsOne("DesafioMottu.Domain.Rentals.Plan", "Plan", b1 =>
                        {
                            b1.Property<Guid>("RentalId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<decimal>("AditionalFeePerDay")
                                .HasColumnType("numeric")
                                .HasColumnName("plan_aditional_fee_per_day");

                            b1.Property<decimal>("DailyFeePercentage")
                                .HasColumnType("numeric")
                                .HasColumnName("plan_daily_fee_percentage");

                            b1.Property<decimal>("DailyPrice")
                                .HasColumnType("numeric")
                                .HasColumnName("plan_daily_price");

                            b1.Property<int>("Days")
                                .HasColumnType("integer")
                                .HasColumnName("plan_days");

                            b1.HasKey("RentalId");

                            b1.ToTable("rentals");

                            b1.WithOwner()
                                .HasForeignKey("RentalId")
                                .HasConstraintName("fk_rentals_rentals_id");
                        });

                    b.OwnsOne("DesafioMottu.Domain.Shared.Money", "TotalPrice", b1 =>
                        {
                            b1.Property<Guid>("RentalId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("numeric")
                                .HasColumnName("total_price_amount");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("total_price_currency");

                            b1.HasKey("RentalId");

                            b1.ToTable("rentals");

                            b1.WithOwner()
                                .HasForeignKey("RentalId")
                                .HasConstraintName("fk_rentals_rentals_id");
                        });

                    b.Navigation("Duration")
                        .IsRequired();

                    b.Navigation("Plan")
                        .IsRequired();

                    b.Navigation("TotalPrice");
                });

            modelBuilder.Entity("DesafioMottu.Domain.Users.User", b =>
                {
                    b.OwnsOne("DesafioMottu.Domain.Users.Name", "Name", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("name_value");

                            b1.HasKey("UserId");

                            b1.ToTable("users");

                            b1.WithOwner()
                                .HasForeignKey("UserId")
                                .HasConstraintName("fk_users_users_id");
                        });

                    b.Navigation("Name")
                        .IsRequired();
                });

            modelBuilder.Entity("DesafioMottu.Domain.Users.User", b =>
                {
                    b.Navigation("DriversLicense");
                });
#pragma warning restore 612, 618
        }
    }
}
