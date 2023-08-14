﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MovieTicketApi.Data;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MovieTicketApi.Migrations
{
    [DbContext(typeof(MovieTicketApiContext))]
    [Migration("20230811002243_MovieIdColumn")]
    partial class MovieIdColumn
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MovieTicketApi.Models.Movie", b =>
                {
                    b.Property<int>("MovieId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("MovieId"));

                    b.Property<string>("BannerUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Director")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Synopsis")
                        .IsRequired()
                        .HasMaxLength(1200)
                        .HasColumnType("character varying(1200)");

                    b.HasKey("MovieId");

                    b.ToTable("Movie");
                });

            modelBuilder.Entity("MovieTicketApi.Models.MovieSession", b =>
                {
                    b.Property<int>("MovieSessionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("MovieSessionId"));

                    b.Property<int>("MovieId")
                        .HasColumnType("integer");

                    b.Property<int>("SessionId")
                        .HasColumnType("integer");

                    b.HasKey("MovieSessionId");

                    b.HasIndex("MovieId");

                    b.HasIndex("SessionId");

                    b.ToTable("MovieSessions");
                });

            modelBuilder.Entity("MovieTicketApi.Models.Session", b =>
                {
                    b.Property<int>("SessionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("SessionId"));

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("MovieId")
                        .HasColumnType("integer");

                    b.Property<int>("Room")
                        .HasColumnType("integer");

                    b.HasKey("SessionId");

                    b.HasIndex("MovieId");

                    b.ToTable("Session");
                });

            modelBuilder.Entity("MovieTicketApi.Models.Ticket", b =>
                {
                    b.Property<int>("TicketId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TicketId"));

                    b.Property<string>("ChairCoord")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("TicketId");

                    b.ToTable("Ticket");
                });

            modelBuilder.Entity("MovieTicketApi.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("boolean");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.HasKey("UserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("MovieTicketApi.Models.MovieSession", b =>
                {
                    b.HasOne("MovieTicketApi.Models.Movie", "Movie")
                        .WithMany()
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MovieTicketApi.Models.Session", "Session")
                        .WithMany()
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");

                    b.Navigation("Session");
                });

            modelBuilder.Entity("MovieTicketApi.Models.Session", b =>
                {
                    b.HasOne("MovieTicketApi.Models.Movie", "Movie")
                        .WithMany("Sessions")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("MovieTicketApi.Models.Movie", b =>
                {
                    b.Navigation("Sessions");
                });
#pragma warning restore 612, 618
        }
    }
}
