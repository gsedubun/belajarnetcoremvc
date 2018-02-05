﻿// <auto-generated />
using belajarnetcoremvc.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace belajarnetcoremvc.Migrations
{
    [DbContext(typeof(belajarDbContext))]
    [Migration("20180129104453_ganti_nama_table")]
    partial class ganti_nama_table
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("belajarnetcoremvc.Models.TblRole", b =>
                {
                    b.Property<int>("RoleID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("RoleName");

                    b.HasKey("RoleID");

                    b.ToTable("TblRole");
                });

            modelBuilder.Entity("belajarnetcoremvc.Models.TblUser", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EmailAddress");

                    b.Property<string>("Password");

                    b.Property<string>("UserName");

                    b.HasKey("UserID");

                    b.ToTable("TblUser");
                });

            modelBuilder.Entity("belajarnetcoremvc.Models.TblUserRole", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("TblRoleRoleID");

                    b.Property<int?>("TblUserUserID");

                    b.HasKey("ID");

                    b.HasIndex("TblRoleRoleID");

                    b.HasIndex("TblUserUserID");

                    b.ToTable("TblUserRole");
                });

            modelBuilder.Entity("belajarnetcoremvc.Models.TblUserRole", b =>
                {
                    b.HasOne("belajarnetcoremvc.Models.TblRole", "TblRole")
                        .WithMany()
                        .HasForeignKey("TblRoleRoleID");

                    b.HasOne("belajarnetcoremvc.Models.TblUser", "TblUser")
                        .WithMany()
                        .HasForeignKey("TblUserUserID");
                });
#pragma warning restore 612, 618
        }
    }
}
