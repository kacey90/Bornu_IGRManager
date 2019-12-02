using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IGRMgr.Modules.UserAccess.Infrastructure.Migrations
{
    public partial class UserAccessInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "users");

            migrationBuilder.CreateTable(
                name: "InternalCommands",
                schema: "users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    Data = table.Column<string>(nullable: true),
                    ProcessedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternalCommands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OutboxMessages",
                schema: "users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OccurredOn = table.Column<DateTime>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    Data = table.Column<string>(nullable: true),
                    ProcessedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboxMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRegistrations",
                schema: "users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ConfirmedDate = table.Column<DateTime>(nullable: true),
                    RegisterDate = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    MiddleName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    StatusCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRegistrations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "users",
                columns: table => new
                {
                    RoleCode = table.Column<string>(nullable: false),
                    UserRegistrationId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserRegistrationId, x.RoleCode });
                    table.ForeignKey(
                        name: "FK_UserRoles_UserRegistrations_UserRegistrationId",
                        column: x => x.UserRegistrationId,
                        principalSchema: "users",
                        principalTable: "UserRegistrations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InternalCommands",
                schema: "users");

            migrationBuilder.DropTable(
                name: "OutboxMessages",
                schema: "users");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "users");

            migrationBuilder.DropTable(
                name: "UserRegistrations",
                schema: "users");
        }
    }
}
