using Microsoft.EntityFrameworkCore.Migrations;

namespace IGRMgr.Modules.Administration.Infrastructure.Migrations
{
    public partial class AdministrationInitialOutbox : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "OutboxMessages",
                newName: "OutboxMessages",
                newSchema: "administration");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "OutboxMessages",
                schema: "administration",
                newName: "OutboxMessages");
        }
    }
}
