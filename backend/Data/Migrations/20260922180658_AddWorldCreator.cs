using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddWorldCreator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Worlds",
                type: "nvarchar(450)",
                nullable: true);

            // Existing worlds predate creator tracking; prefer the seeded administrator.
            migrationBuilder.Sql("""
                UPDATE [Worlds]
                SET [CreatorId] = (
                    SELECT TOP (1) [Id] FROM [AspNetUsers]
                    ORDER BY CASE WHEN [NormalizedUserName] = 'WORLDADMIN' THEN 0 ELSE 1 END, [Id]
                );
                IF EXISTS (SELECT 1 FROM [Worlds] WHERE [CreatorId] IS NULL)
                    THROW 50000, 'Create a user before migrating existing worlds.', 1;
                """);

            migrationBuilder.AlterColumn<string>(
                name: "CreatorId",
                table: "Worlds",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Worlds_CreatorId",
                table: "Worlds",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Worlds_AspNetUsers_CreatorId",
                table: "Worlds",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Worlds_AspNetUsers_CreatorId",
                table: "Worlds");

            migrationBuilder.DropIndex(
                name: "IX_Worlds_CreatorId",
                table: "Worlds");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Worlds");
        }
    }
}
