using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FerreiraCostaAv.Migrations
{
    public partial class LastAdjustments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Credentials_CredencialsId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "CredencialsId",
                table: "Users",
                newName: "CredentialId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_CredencialsId",
                table: "Users",
                newName: "IX_Users_CredentialId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangeDate",
                table: "Users",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Credentials_CredentialId",
                table: "Users",
                column: "CredentialId",
                principalTable: "Credentials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Credentials_CredentialId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "CredentialId",
                table: "Users",
                newName: "CredencialsId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_CredentialId",
                table: "Users",
                newName: "IX_Users_CredencialsId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangeDate",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Credentials_CredencialsId",
                table: "Users",
                column: "CredencialsId",
                principalTable: "Credentials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
