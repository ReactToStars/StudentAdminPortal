using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentAdminPortalAPI.Core.Migrations
{
    public partial class fixAddressEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhysicalAddess",
                table: "Addresses",
                newName: "PhysicalAddress");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhysicalAddress",
                table: "Addresses",
                newName: "PhysicalAddess");
        }
    }
}
