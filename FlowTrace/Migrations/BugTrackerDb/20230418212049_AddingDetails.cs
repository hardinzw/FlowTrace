using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlowTrace.Migrations.BugTrackerDb
{
    /// <inheritdoc />
    public partial class AddingDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "Bugs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Details",
                table: "Bugs");
        }
    }
}
