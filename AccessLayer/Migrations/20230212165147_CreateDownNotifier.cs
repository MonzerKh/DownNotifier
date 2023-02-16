using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AccessLayer.Migrations
{
    public partial class CreateDownNotifier : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SystemUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Phone_Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TargetApplications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastCheckIsUp = table.Column<bool>(type: "bit", nullable: false),
                    Interval = table.Column<int>(type: "int", nullable: false, defaultValue: 5),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    SystemUser_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TargetApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TargetApplications_SystemUsers_SystemUser_Id",
                        column: x => x.SystemUser_Id,
                        principalTable: "SystemUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppCheckHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExecuteTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsUp = table.Column<bool>(type: "bit", nullable: false),
                    TargetApplication_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCheckHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppCheckHistories_TargetApplications_TargetApplication_Id",
                        column: x => x.TargetApplication_Id,
                        principalTable: "TargetApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppCheckHistories_TargetApplication_Id",
                table: "AppCheckHistories",
                column: "TargetApplication_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TargetApplications_SystemUser_Id",
                table: "TargetApplications",
                column: "SystemUser_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppCheckHistories");

            migrationBuilder.DropTable(
                name: "TargetApplications");

            migrationBuilder.DropTable(
                name: "SystemUsers");
        }
    }
}
