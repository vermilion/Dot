﻿// <auto-generated />
using System;
using Cofoundry.BasicTestSite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Web.Service.Identity.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210602114755_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Cofoundry")
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Cofoundry.Core.DistributedLocks.DistributedLockEntity", b =>
                {
                    b.Property<string>("DistributedLockId")
                        .HasColumnType("text");

                    b.Property<DateTime?>("ExpiryDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("LockDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("LockingId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("DistributedLockId");

                    b.ToTable("DistributedLock", "Cofoundry");
                });

            modelBuilder.Entity("Cofoundry.Domain.Data.EntityDefinition", b =>
                {
                    b.Property<string>("EntityDefinitionCode")
                        .HasMaxLength(6)
                        .HasColumnType("character varying(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("EntityDefinitionCode");

                    b.ToTable("EntityDefinition", "Cofoundry");
                });

            modelBuilder.Entity("Cofoundry.Domain.Data.FailedAuthenticationAttempt", b =>
                {
                    b.Property<int>("FailedAuthenticationAttemptId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("AttemptDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("IPAddress")
                        .HasMaxLength(45)
                        .IsUnicode(false)
                        .HasColumnType("character varying(45)");

                    b.Property<string>("UserName")
                        .HasMaxLength(45)
                        .IsUnicode(false)
                        .HasColumnType("character varying(45)");

                    b.HasKey("FailedAuthenticationAttemptId");

                    b.ToTable("FailedAuthenticationAttempt", "Cofoundry");
                });

            modelBuilder.Entity("Cofoundry.Domain.Data.Permission", b =>
                {
                    b.Property<int>("PermissionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("EntityDefinitionCode")
                        .HasMaxLength(6)
                        .HasColumnType("character varying(6)");

                    b.Property<string>("PermissionCode")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("character varying(6)");

                    b.HasKey("PermissionId");

                    b.HasIndex("EntityDefinitionCode");

                    b.ToTable("Permission", "Cofoundry");
                });

            modelBuilder.Entity("Cofoundry.Domain.Data.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("RoleCode")
                        .HasMaxLength(3)
                        .HasColumnType("character varying(3)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("RoleId");

                    b.ToTable("Role", "Cofoundry");
                });

            modelBuilder.Entity("Cofoundry.Domain.Data.RolePermission", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<int>("PermissionId")
                        .HasColumnType("integer");

                    b.HasKey("RoleId", "PermissionId");

                    b.HasIndex("PermissionId");

                    b.ToTable("RolePermission", "Cofoundry");
                });

            modelBuilder.Entity("Cofoundry.Domain.Data.Setting", b =>
                {
                    b.Property<int>("SettingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("SettingKey")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.Property<string>("SettingValue")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("SettingId");

                    b.ToTable("Setting", "Cofoundry");
                });

            modelBuilder.Entity("Cofoundry.Domain.Data.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("CreatorId")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsEmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsSystemAccount")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("LastLoginDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("LastName")
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.Property<DateTime>("LastPasswordChangeDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<DateTime?>("PreviousLoginDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("RequirePasswordChange")
                        .HasColumnType("boolean");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.HasKey("UserId");

                    b.HasIndex("CreatorId");

                    b.HasIndex("RoleId");

                    b.ToTable("User", "Cofoundry");
                });

            modelBuilder.Entity("Cofoundry.Domain.Data.UserPasswordResetRequest", b =>
                {
                    b.Property<Guid>("UserPasswordResetRequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("IPAddress")
                        .HasMaxLength(45)
                        .IsUnicode(false)
                        .HasColumnType("character varying(45)");

                    b.Property<bool>("IsComplete")
                        .HasColumnType("boolean");

                    b.Property<string>("Token")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("UserPasswordResetRequestId");

                    b.HasIndex("UserId");

                    b.ToTable("UserPasswordResetRequest", "Cofoundry");
                });

            modelBuilder.Entity("Cofoundry.Domain.Data.Permission", b =>
                {
                    b.HasOne("Cofoundry.Domain.Data.EntityDefinition", "EntityDefinition")
                        .WithMany("Permissions")
                        .HasForeignKey("EntityDefinitionCode");

                    b.Navigation("EntityDefinition");
                });

            modelBuilder.Entity("Cofoundry.Domain.Data.RolePermission", b =>
                {
                    b.HasOne("Cofoundry.Domain.Data.Permission", "Permission")
                        .WithMany("RolePermissions")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cofoundry.Domain.Data.Role", "Role")
                        .WithMany("RolePermissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Cofoundry.Domain.Data.User", b =>
                {
                    b.HasOne("Cofoundry.Domain.Data.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId");

                    b.HasOne("Cofoundry.Domain.Data.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Cofoundry.Domain.Data.UserPasswordResetRequest", b =>
                {
                    b.HasOne("Cofoundry.Domain.Data.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Cofoundry.Domain.Data.EntityDefinition", b =>
                {
                    b.Navigation("Permissions");
                });

            modelBuilder.Entity("Cofoundry.Domain.Data.Permission", b =>
                {
                    b.Navigation("RolePermissions");
                });

            modelBuilder.Entity("Cofoundry.Domain.Data.Role", b =>
                {
                    b.Navigation("RolePermissions");
                });
#pragma warning restore 612, 618
        }
    }
}
