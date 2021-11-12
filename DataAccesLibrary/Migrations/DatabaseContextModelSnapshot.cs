﻿// <auto-generated />
using System;
using DataAccesLibrary.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataAccesLibrary.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.11");

            modelBuilder.Entity("DataAccesLibrary.Models.Creator", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Bio")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<int>("Tokens")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.Property<string>("Website")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Creators");
                });

            modelBuilder.Entity("DataAccesLibrary.Models.CreatorFans", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("CreatorId")
                        .HasColumnType("bigint");

                    b.Property<long>("FanId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("FanId");

                    b.ToTable("CreatorFans");
                });

            modelBuilder.Entity("DataAccesLibrary.Models.Fan", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long?>("CreatorId")
                        .HasColumnType("bigint");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<int>("Tokens")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Fans");
                });

            modelBuilder.Entity("DataAccesLibrary.Models.LikedPosts", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("FanId")
                        .HasColumnType("bigint");

                    b.Property<long>("PostId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("FanId");

                    b.HasIndex("PostId");

                    b.ToTable("LikedPosts");
                });

            modelBuilder.Entity("DataAccesLibrary.Models.Post", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("CreatorId")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Image")
                        .HasColumnType("text");

                    b.Property<int>("Likes")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("DataAccesLibrary.Models.Reaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<long>("FanId")
                        .HasColumnType("bigint");

                    b.Property<string>("Message")
                        .HasColumnType("text");

                    b.Property<long>("PostId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("FanId");

                    b.HasIndex("PostId");

                    b.ToTable("Reactions");
                });

            modelBuilder.Entity("DataAccesLibrary.Models.SavedPosts", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("FanId")
                        .HasColumnType("bigint");

                    b.Property<long>("PostId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("FanId");

                    b.HasIndex("PostId");

                    b.ToTable("SavedPosts");
                });

            modelBuilder.Entity("DataAccesLibrary.Models.CreatorFans", b =>
                {
                    b.HasOne("DataAccesLibrary.Models.Creator", "Creator")
                        .WithMany("Followers")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccesLibrary.Models.Fan", "Fan")
                        .WithMany("CreatorFans")
                        .HasForeignKey("FanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");

                    b.Navigation("Fan");
                });

            modelBuilder.Entity("DataAccesLibrary.Models.Fan", b =>
                {
                    b.HasOne("DataAccesLibrary.Models.Creator", null)
                        .WithMany("BannedFans")
                        .HasForeignKey("CreatorId");
                });

            modelBuilder.Entity("DataAccesLibrary.Models.LikedPosts", b =>
                {
                    b.HasOne("DataAccesLibrary.Models.Fan", "Fan")
                        .WithMany("LikedPosts")
                        .HasForeignKey("FanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccesLibrary.Models.Post", "Post")
                        .WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Fan");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("DataAccesLibrary.Models.Post", b =>
                {
                    b.HasOne("DataAccesLibrary.Models.Creator", "Creator")
                        .WithMany("Posts")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("DataAccesLibrary.Models.Reaction", b =>
                {
                    b.HasOne("DataAccesLibrary.Models.Fan", "Fan")
                        .WithMany("Reactions")
                        .HasForeignKey("FanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccesLibrary.Models.Post", "Post")
                        .WithMany("Reactions")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Fan");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("DataAccesLibrary.Models.SavedPosts", b =>
                {
                    b.HasOne("DataAccesLibrary.Models.Fan", "Fan")
                        .WithMany("SavedPosts")
                        .HasForeignKey("FanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccesLibrary.Models.Post", "Post")
                        .WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Fan");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("DataAccesLibrary.Models.Creator", b =>
                {
                    b.Navigation("BannedFans");

                    b.Navigation("Followers");

                    b.Navigation("Posts");
                });

            modelBuilder.Entity("DataAccesLibrary.Models.Fan", b =>
                {
                    b.Navigation("CreatorFans");

                    b.Navigation("LikedPosts");

                    b.Navigation("Reactions");

                    b.Navigation("SavedPosts");
                });

            modelBuilder.Entity("DataAccesLibrary.Models.Post", b =>
                {
                    b.Navigation("Reactions");
                });
#pragma warning restore 612, 618
        }
    }
}
