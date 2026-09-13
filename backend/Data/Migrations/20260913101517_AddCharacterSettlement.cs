using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCharacterSettlement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SettlementId",
                table: "Characters",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.Sql(
                """
                IF EXISTS (SELECT 1 FROM Characters)
                BEGIN
                    DECLARE @WorldId uniqueidentifier = (SELECT TOP 1 Id FROM Worlds ORDER BY Id);
                    IF @WorldId IS NULL
                    BEGIN
                        SET @WorldId = '72591491-cdf2-44d9-a16a-9ad35913f5ba';
                        INSERT INTO Worlds (Id, Name, Description)
                        VALUES (@WorldId, 'Legacy World', 'Created to preserve existing characters during migration.');
                    END;

                    DECLARE @SettlementId uniqueidentifier = (SELECT TOP 1 Id FROM Settlements ORDER BY Id);
                    IF @SettlementId IS NULL
                    BEGIN
                        SET @SettlementId = 'df01ff63-f935-4bc9-9dd0-6942be4b8d52';
                        INSERT INTO Settlements (Id, Name, Type, Description, Population, WorldId)
                        VALUES (@SettlementId, 'Legacy Haven', 0, 'Home of characters created before settlements were required.', 0, @WorldId);
                    END;

                    UPDATE Characters SET SettlementId = @SettlementId WHERE SettlementId IS NULL;
                END;
                """);

            migrationBuilder.AlterColumn<Guid>(
                name: "SettlementId",
                table: "Characters",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Characters_SettlementId",
                table: "Characters",
                column: "SettlementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Settlements_SettlementId",
                table: "Characters",
                column: "SettlementId",
                principalTable: "Settlements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Settlements_SettlementId",
                table: "Characters");

            migrationBuilder.DropIndex(
                name: "IX_Characters_SettlementId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "SettlementId",
                table: "Characters");
        }
    }
}
