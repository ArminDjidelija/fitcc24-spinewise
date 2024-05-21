﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SpineWise.Web.Data;

#nullable disable

namespace SpineWise.Web.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240207174107_addedChangeOnLogs")]
    partial class addedChangeOnLogs
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SpineWise.ClassLibrary.Models.Chair", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ChairModelId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfCreating")
                        .HasColumnType("datetime2");

                    b.Property<string>("SerialNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ChairModelId");

                    b.ToTable("Chairs");
                });

            modelBuilder.Entity("SpineWise.ClassLibrary.Models.ChairModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateOfCreating")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ChairModels");
                });

            modelBuilder.Entity("SpineWise.ClassLibrary.Models.ChairUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ChairId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfAcquiring")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChairId");

                    b.HasIndex("UserId");

                    b.ToTable("ChairsUsers");
                });

            modelBuilder.Entity("SpineWise.ClassLibrary.Models.FingerprintLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("LogDateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Successful")
                        .HasColumnType("bit");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("FingerprintLogs");
                });

            modelBuilder.Entity("SpineWise.ClassLibrary.Models.SignInLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("SuccessfullSignIn")
                        .HasColumnType("bit");

                    b.Property<DateTime>("TimeOfSignIn")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserAccountID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserAccountID");

                    b.ToTable("SignInLogs");
                });

            modelBuilder.Entity("SpineWise.ClassLibrary.Models.SignOutLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("TimeOfSignOut")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserAccountID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserAccountID");

                    b.ToTable("SignOutLogs");
                });

            modelBuilder.Entity("SpineWise.ClassLibrary.Models.SpinePostureDataLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ChairId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Good")
                        .HasColumnType("bit");

                    b.Property<float>("LegDistance")
                        .HasColumnType("real");

                    b.Property<bool>("PressureSensor1")
                        .HasColumnType("bit");

                    b.Property<bool>("PressureSensor2")
                        .HasColumnType("bit");

                    b.Property<bool>("PressureSensor3")
                        .HasColumnType("bit");

                    b.Property<float>("UpperBackDistance")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("ChairId");

                    b.ToTable("SpinePostureDataLogs");
                });

            modelBuilder.Entity("SpineWise.ClassLibrary.Models.SpineWiseDataLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ChairId")
                        .HasColumnType("int");

                    b.Property<float>("LegDistance")
                        .HasColumnType("real");

                    b.Property<DateTime>("LogDateTime")
                        .HasColumnType("datetime2");

                    b.Property<float>("LumbarBackDistance")
                        .HasColumnType("real");

                    b.Property<float>("ThoracicBackDistance")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("ChairId");

                    b.ToTable("SpineWiseDataLogs");
                });

            modelBuilder.Entity("SpineWise.ClassLibrary.Models.UserAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfCreation")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserAccounts");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("SpineWise.ClassLibrary.Models.UserActionLog", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("ExceptionMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsException")
                        .HasColumnType("bit");

                    b.Property<string>("PostData")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QueryPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserAccountID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("UserAccountID");

                    b.ToTable("UserActionLogs");
                });

            modelBuilder.Entity("SpineWise.ClassLibrary.Models.UserToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("TimeOfRecording")
                        .HasColumnType("datetime2");

                    b.Property<string>("TokenValue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserAccountId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserAccountId");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("SpineWise.ClassLibrary.Models.SuperAdmin", b =>
                {
                    b.HasBaseType("SpineWise.ClassLibrary.Models.UserAccount");

                    b.ToTable("SuperAdmins");
                });

            modelBuilder.Entity("SpineWise.ClassLibrary.Models.User", b =>
                {
                    b.HasBaseType("SpineWise.ClassLibrary.Models.UserAccount");

                    b.Property<int?>("ChairId")
                        .HasColumnType("int");

                    b.HasIndex("ChairId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SpineWise.ClassLibrary.Models.Chair", b =>
                {
                    b.HasOne("SpineWise.ClassLibrary.Models.ChairModel", "ChairModel")
                        .WithMany()
                        .HasForeignKey("ChairModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChairModel");
                });

            modelBuilder.Entity("SpineWise.ClassLibrary.Models.ChairUser", b =>
                {
                    b.HasOne("SpineWise.ClassLibrary.Models.Chair", "Chair")
                        .WithMany()
                        .HasForeignKey("ChairId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SpineWise.ClassLibrary.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chair");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SpineWise.ClassLibrary.Models.FingerprintLog", b =>
                {
                    b.HasOne("SpineWise.ClassLibrary.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SpineWise.ClassLibrary.Models.SignInLog", b =>
                {
                    b.HasOne("SpineWise.ClassLibrary.Models.UserAccount", "UserAccount")
                        .WithMany()
                        .HasForeignKey("UserAccountID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserAccount");
                });

            modelBuilder.Entity("SpineWise.ClassLibrary.Models.SignOutLog", b =>
                {
                    b.HasOne("SpineWise.ClassLibrary.Models.UserAccount", "UserAccount")
                        .WithMany()
                        .HasForeignKey("UserAccountID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserAccount");
                });

            modelBuilder.Entity("SpineWise.ClassLibrary.Models.SpinePostureDataLog", b =>
                {
                    b.HasOne("SpineWise.ClassLibrary.Models.Chair", "Chair")
                        .WithMany()
                        .HasForeignKey("ChairId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chair");
                });

            modelBuilder.Entity("SpineWise.ClassLibrary.Models.SpineWiseDataLog", b =>
                {
                    b.HasOne("SpineWise.ClassLibrary.Models.Chair", "Chair")
                        .WithMany()
                        .HasForeignKey("ChairId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chair");
                });

            modelBuilder.Entity("SpineWise.ClassLibrary.Models.UserActionLog", b =>
                {
                    b.HasOne("SpineWise.ClassLibrary.Models.UserAccount", "UserAccount")
                        .WithMany()
                        .HasForeignKey("UserAccountID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserAccount");
                });

            modelBuilder.Entity("SpineWise.ClassLibrary.Models.UserToken", b =>
                {
                    b.HasOne("SpineWise.ClassLibrary.Models.UserAccount", "UserAccount")
                        .WithMany()
                        .HasForeignKey("UserAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserAccount");
                });

            modelBuilder.Entity("SpineWise.ClassLibrary.Models.SuperAdmin", b =>
                {
                    b.HasOne("SpineWise.ClassLibrary.Models.UserAccount", null)
                        .WithOne()
                        .HasForeignKey("SpineWise.ClassLibrary.Models.SuperAdmin", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SpineWise.ClassLibrary.Models.User", b =>
                {
                    b.HasOne("SpineWise.ClassLibrary.Models.Chair", "Chair")
                        .WithMany()
                        .HasForeignKey("ChairId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("SpineWise.ClassLibrary.Models.UserAccount", null)
                        .WithOne()
                        .HasForeignKey("SpineWise.ClassLibrary.Models.User", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chair");
                });
#pragma warning restore 612, 618
        }
    }
}
