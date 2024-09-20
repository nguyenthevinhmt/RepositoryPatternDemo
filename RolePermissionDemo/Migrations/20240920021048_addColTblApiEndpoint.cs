using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RolePermissionDemo.Migrations
{
    /// <inheritdoc />
    public partial class addColTblApiEndpoint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "auth",
                table: "UserRole",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 9, 20, 9, 10, 48, 325, DateTimeKind.Local).AddTicks(1328),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 9, 18, 14, 10, 14, 149, DateTimeKind.Local).AddTicks(6874));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "auth",
                table: "User",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 9, 20, 9, 10, 48, 324, DateTimeKind.Local).AddTicks(8842),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 9, 18, 14, 10, 14, 149, DateTimeKind.Local).AddTicks(5336));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "auth",
                table: "Role",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 9, 20, 9, 10, 48, 325, DateTimeKind.Local).AddTicks(2542),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 9, 18, 14, 10, 14, 149, DateTimeKind.Local).AddTicks(7461));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "auth",
                table: "ApiEndpoint",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                schema: "auth",
                table: "ApiEndpoint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "auth",
                table: "UserRole",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 9, 18, 14, 10, 14, 149, DateTimeKind.Local).AddTicks(6874),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 9, 20, 9, 10, 48, 325, DateTimeKind.Local).AddTicks(1328));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "auth",
                table: "User",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 9, 18, 14, 10, 14, 149, DateTimeKind.Local).AddTicks(5336),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 9, 20, 9, 10, 48, 324, DateTimeKind.Local).AddTicks(8842));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "auth",
                table: "Role",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 9, 18, 14, 10, 14, 149, DateTimeKind.Local).AddTicks(7461),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 9, 20, 9, 10, 48, 325, DateTimeKind.Local).AddTicks(2542));
        }
    }
}
