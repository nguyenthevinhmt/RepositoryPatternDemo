using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RolePermissionDemo.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDb : Migration
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
                defaultValue: new DateTime(2024, 9, 5, 14, 44, 50, 87, DateTimeKind.Local).AddTicks(8333),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 9, 5, 10, 42, 1, 705, DateTimeKind.Local).AddTicks(2713));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "auth",
                table: "User",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 9, 5, 14, 44, 50, 87, DateTimeKind.Local).AddTicks(6938),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 9, 5, 10, 42, 1, 705, DateTimeKind.Local).AddTicks(1738));

            migrationBuilder.AddColumn<int>(
                name: "UserType",
                schema: "auth",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "auth",
                table: "Role",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 9, 5, 14, 44, 50, 87, DateTimeKind.Local).AddTicks(8910),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 9, 5, 10, 42, 1, 705, DateTimeKind.Local).AddTicks(3155));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserType",
                schema: "auth",
                table: "User");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "auth",
                table: "UserRole",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 9, 5, 10, 42, 1, 705, DateTimeKind.Local).AddTicks(2713),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 9, 5, 14, 44, 50, 87, DateTimeKind.Local).AddTicks(8333));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "auth",
                table: "User",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 9, 5, 10, 42, 1, 705, DateTimeKind.Local).AddTicks(1738),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 9, 5, 14, 44, 50, 87, DateTimeKind.Local).AddTicks(6938));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "auth",
                table: "Role",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 9, 5, 10, 42, 1, 705, DateTimeKind.Local).AddTicks(3155),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 9, 5, 14, 44, 50, 87, DateTimeKind.Local).AddTicks(8910));
        }
    }
}
