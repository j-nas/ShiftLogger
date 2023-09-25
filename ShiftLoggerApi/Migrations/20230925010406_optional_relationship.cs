using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShiftLoggerApi.Migrations
{
    /// <inheritdoc />
    public partial class optional_relationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shifts_Workers_WorkerId",
                table: "Shifts");

            migrationBuilder.AlterColumn<long>(
                name: "WorkerId",
                table: "Shifts",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Shifts_Workers_WorkerId",
                table: "Shifts",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shifts_Workers_WorkerId",
                table: "Shifts");

            migrationBuilder.AlterColumn<long>(
                name: "WorkerId",
                table: "Shifts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Shifts_Workers_WorkerId",
                table: "Shifts",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
