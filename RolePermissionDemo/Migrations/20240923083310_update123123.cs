using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RolePermissionDemo.Migrations
{
    /// <inheritdoc />
    public partial class update123123 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KeyPermission_KeyPermission_KeyPermissionId",
                schema: "auth",
                table: "KeyPermission");

            migrationBuilder.DropForeignKey(
                name: "FK_PermissionForApiEndpoint_KeyPermission_KeyPermissionId",
                schema: "auth",
                table: "PermissionForApiEndpoint");

            migrationBuilder.DropIndex(
                name: "IX_KeyPermission_KeyPermissionId",
                schema: "auth",
                table: "KeyPermission");

            migrationBuilder.DropColumn(
                name: "KeyPermissionId",
                schema: "auth",
                table: "KeyPermission");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "auth",
                table: "UserRole",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 9, 23, 15, 33, 9, 584, DateTimeKind.Local).AddTicks(1399),
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
                defaultValue: new DateTime(2024, 9, 23, 15, 33, 9, 584, DateTimeKind.Local).AddTicks(279),
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
                defaultValue: new DateTime(2024, 9, 23, 15, 33, 9, 584, DateTimeKind.Local).AddTicks(2090),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 9, 20, 9, 10, 48, 325, DateTimeKind.Local).AddTicks(2542));

            migrationBuilder.AlterColumn<int>(
                name: "KeyPermissionId",
                schema: "auth",
                table: "PermissionForApiEndpoint",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_KeyPermission_KeyPermission_ParentId",
                schema: "auth",
                table: "KeyPermission",
                column: "ParentId",
                principalSchema: "auth",
                principalTable: "KeyPermission",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionForApiEndpoint_KeyPermission_KeyPermissionId",
                schema: "auth",
                table: "PermissionForApiEndpoint",
                column: "KeyPermissionId",
                principalSchema: "auth",
                principalTable: "KeyPermission",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KeyPermission_KeyPermission_ParentId",
                schema: "auth",
                table: "KeyPermission");

            migrationBuilder.DropForeignKey(
                name: "FK_PermissionForApiEndpoint_KeyPermission_KeyPermissionId",
                schema: "auth",
                table: "PermissionForApiEndpoint");

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
                oldDefaultValue: new DateTime(2024, 9, 23, 15, 33, 9, 584, DateTimeKind.Local).AddTicks(1399));

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
                oldDefaultValue: new DateTime(2024, 9, 23, 15, 33, 9, 584, DateTimeKind.Local).AddTicks(279));

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
                oldDefaultValue: new DateTime(2024, 9, 23, 15, 33, 9, 584, DateTimeKind.Local).AddTicks(2090));

            migrationBuilder.AlterColumn<int>(
                name: "KeyPermissionId",
                schema: "auth",
                table: "PermissionForApiEndpoint",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "KeyPermissionId",
                schema: "auth",
                table: "KeyPermission",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_KeyPermission_KeyPermissionId",
                schema: "auth",
                table: "KeyPermission",
                column: "KeyPermissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_KeyPermission_KeyPermission_KeyPermissionId",
                schema: "auth",
                table: "KeyPermission",
                column: "KeyPermissionId",
                principalSchema: "auth",
                principalTable: "KeyPermission",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionForApiEndpoint_KeyPermission_KeyPermissionId",
                schema: "auth",
                table: "PermissionForApiEndpoint",
                column: "KeyPermissionId",
                principalSchema: "auth",
                principalTable: "KeyPermission",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
