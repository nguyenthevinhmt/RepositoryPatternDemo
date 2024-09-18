using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RolePermissionDemo.Migrations
{
    /// <inheritdoc />
    public partial class ChangeColumnNameTblPermissionKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "KeyPermissionName",
                schema: "auth",
                table: "KeyPermission",
                newName: "PermissionKey");

            migrationBuilder.RenameColumn(
                name: "KeyPermissionLabel",
                schema: "auth",
                table: "KeyPermission",
                newName: "PermissionLabel");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "auth",
                table: "UserRole",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 9, 17, 15, 21, 59, 806, DateTimeKind.Local).AddTicks(8561),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 9, 17, 11, 16, 49, 744, DateTimeKind.Local).AddTicks(4262));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "auth",
                table: "User",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 9, 17, 15, 21, 59, 806, DateTimeKind.Local).AddTicks(7055),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 9, 17, 11, 16, 49, 744, DateTimeKind.Local).AddTicks(1662));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "auth",
                table: "Role",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 9, 17, 15, 21, 59, 806, DateTimeKind.Local).AddTicks(9376),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 9, 17, 11, 16, 49, 744, DateTimeKind.Local).AddTicks(5502));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PermissionLabel",
                schema: "auth",
                table: "KeyPermission",
                newName: "KeyPermissionLabel");

            migrationBuilder.RenameColumn(
                name: "PermissionKey",
                schema: "auth",
                table: "KeyPermission",
                newName: "KeyPermissionName");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "auth",
                table: "UserRole",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 9, 17, 11, 16, 49, 744, DateTimeKind.Local).AddTicks(4262),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 9, 17, 15, 21, 59, 806, DateTimeKind.Local).AddTicks(8561));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "auth",
                table: "User",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 9, 17, 11, 16, 49, 744, DateTimeKind.Local).AddTicks(1662),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 9, 17, 15, 21, 59, 806, DateTimeKind.Local).AddTicks(7055));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "auth",
                table: "Role",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 9, 17, 11, 16, 49, 744, DateTimeKind.Local).AddTicks(5502),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 9, 17, 15, 21, 59, 806, DateTimeKind.Local).AddTicks(9376));
        }
    }
}
