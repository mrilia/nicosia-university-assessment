﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Nicosia.Assessment.Persistence.Context;

#nullable disable

namespace Nicosia.Assessment.Persistence.Migrations
{
    [DbContext(typeof(SqliteDbContext))]
    [Migration("20221205220409_rereh token")]
    partial class rerehtoken
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.0");

            modelBuilder.Entity("LecturerSection", b =>
                {
                    b.Property<Guid>("LecturersLecturerId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("SectionsSectionId")
                        .HasColumnType("TEXT");

                    b.HasKey("LecturersLecturerId", "SectionsSectionId");

                    b.HasIndex("SectionsSectionId");

                    b.ToTable("LecturerSection");
                });

            modelBuilder.Entity("Nicosia.Assessment.Domain.Models.Course.Course", b =>
                {
                    b.Property<Guid>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("CourseId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("Nicosia.Assessment.Domain.Models.Period.Period", b =>
                {
                    b.Property<Guid>("PeriodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateOnly>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("TEXT");

                    b.HasKey("PeriodId");

                    b.ToTable("Periods");
                });

            modelBuilder.Entity("Nicosia.Assessment.Domain.Models.Section.Section", b =>
                {
                    b.Property<Guid>("SectionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CourseId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Details")
                        .HasColumnType("TEXT");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PeriodId")
                        .HasColumnType("TEXT");

                    b.HasKey("SectionId");

                    b.HasIndex("CourseId");

                    b.HasIndex("PeriodId");

                    b.ToTable("Sections");
                });

            modelBuilder.Entity("Nicosia.Assessment.Domain.Models.Security.RefreshToken", b =>
                {
                    b.Property<Guid>("RefreshTokenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("AdminId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedByIp")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Expires")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("LecturerId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ReasonRevoked")
                        .HasColumnType("TEXT");

                    b.Property<string>("ReplacedByToken")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("Revoked")
                        .HasColumnType("TEXT");

                    b.Property<string>("RevokedByIp")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("StudentId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("RefreshTokenId");

                    b.HasIndex("AdminId");

                    b.HasIndex("LecturerId");

                    b.HasIndex("StudentId");

                    b.ToTable("RefreshToken");
                });

            modelBuilder.Entity("Nicosia.Assessment.Domain.Models.User.Admin", b =>
                {
                    b.Property<Guid>("AdminId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("AdminId");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("Nicosia.Assessment.Domain.Models.User.Lecturer", b =>
                {
                    b.Property<Guid>("LecturerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SocialInsuranceNumber")
                        .HasColumnType("TEXT");

                    b.HasKey("LecturerId");

                    b.ToTable("Lecturers");
                });

            modelBuilder.Entity("Nicosia.Assessment.Domain.Models.User.Student", b =>
                {
                    b.Property<Guid>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("StudentId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("SectionStudent", b =>
                {
                    b.Property<Guid>("SectionsSectionId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("StudentsStudentId")
                        .HasColumnType("TEXT");

                    b.HasKey("SectionsSectionId", "StudentsStudentId");

                    b.HasIndex("StudentsStudentId");

                    b.ToTable("SectionStudent");
                });

            modelBuilder.Entity("LecturerSection", b =>
                {
                    b.HasOne("Nicosia.Assessment.Domain.Models.User.Lecturer", null)
                        .WithMany()
                        .HasForeignKey("LecturersLecturerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Nicosia.Assessment.Domain.Models.Section.Section", null)
                        .WithMany()
                        .HasForeignKey("SectionsSectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Nicosia.Assessment.Domain.Models.Section.Section", b =>
                {
                    b.HasOne("Nicosia.Assessment.Domain.Models.Course.Course", "Course")
                        .WithMany("Sections")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Nicosia.Assessment.Domain.Models.Period.Period", "Period")
                        .WithMany("Sections")
                        .HasForeignKey("PeriodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Period");
                });

            modelBuilder.Entity("Nicosia.Assessment.Domain.Models.Security.RefreshToken", b =>
                {
                    b.HasOne("Nicosia.Assessment.Domain.Models.User.Admin", "Admin")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("AdminId");

                    b.HasOne("Nicosia.Assessment.Domain.Models.User.Lecturer", "Lecturer")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("LecturerId");

                    b.HasOne("Nicosia.Assessment.Domain.Models.User.Student", "Student")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("StudentId");

                    b.Navigation("Admin");

                    b.Navigation("Lecturer");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("SectionStudent", b =>
                {
                    b.HasOne("Nicosia.Assessment.Domain.Models.Section.Section", null)
                        .WithMany()
                        .HasForeignKey("SectionsSectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Nicosia.Assessment.Domain.Models.User.Student", null)
                        .WithMany()
                        .HasForeignKey("StudentsStudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Nicosia.Assessment.Domain.Models.Course.Course", b =>
                {
                    b.Navigation("Sections");
                });

            modelBuilder.Entity("Nicosia.Assessment.Domain.Models.Period.Period", b =>
                {
                    b.Navigation("Sections");
                });

            modelBuilder.Entity("Nicosia.Assessment.Domain.Models.User.Admin", b =>
                {
                    b.Navigation("RefreshTokens");
                });

            modelBuilder.Entity("Nicosia.Assessment.Domain.Models.User.Lecturer", b =>
                {
                    b.Navigation("RefreshTokens");
                });

            modelBuilder.Entity("Nicosia.Assessment.Domain.Models.User.Student", b =>
                {
                    b.Navigation("RefreshTokens");
                });
#pragma warning restore 612, 618
        }
    }
}