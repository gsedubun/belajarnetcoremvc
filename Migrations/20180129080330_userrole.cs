using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace belajarnetcoremvc.Migrations
{
    public partial class userrole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TblRole",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblRole", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "TblUserRole",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TblRoleRoleID = table.Column<int>(type: "int", nullable: true),
                    TblUserUserID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblUserRole", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TblUserRole_TblRole_TblRoleRoleID",
                        column: x => x.TblRoleRoleID,
                        principalTable: "TblRole",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TblUserRole_TblUser_TblUserUserID",
                        column: x => x.TblUserUserID,
                        principalTable: "TblUser",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblUserRole_TblRoleRoleID",
                table: "TblUserRole",
                column: "TblRoleRoleID");

            migrationBuilder.CreateIndex(
                name: "IX_TblUserRole_TblUserUserID",
                table: "TblUserRole",
                column: "TblUserUserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblUserRole");

            migrationBuilder.DropTable(
                name: "TblRole");
        }
    }
}
