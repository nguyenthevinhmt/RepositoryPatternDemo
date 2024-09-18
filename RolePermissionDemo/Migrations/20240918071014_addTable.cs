using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RolePermissionDemo.Migrations
{
    /// <inheritdoc />
    public partial class addTable : Migration
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
                defaultValue: new DateTime(2024, 9, 18, 14, 10, 14, 149, DateTimeKind.Local).AddTicks(6874),
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
                defaultValue: new DateTime(2024, 9, 18, 14, 10, 14, 149, DateTimeKind.Local).AddTicks(5336),
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
                defaultValue: new DateTime(2024, 9, 18, 14, 10, 14, 149, DateTimeKind.Local).AddTicks(7461),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 9, 17, 15, 21, 59, 806, DateTimeKind.Local).AddTicks(9376));

            migrationBuilder.CreateTable(
                name: "ApiEndpoint",
                schema: "auth",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Path = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiEndpoint", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PermissionForApiEndpoint",
                schema: "auth",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KeyPermissionId = table.Column<int>(type: "int", nullable: false),
                    ApiEndpointId = table.Column<int>(type: "int", nullable: false),
                    IsAuthenticate = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionForApiEndpoint", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PermissionForApiEndpoint_ApiEndpoint_ApiEndpointId",
                        column: x => x.ApiEndpointId,
                        principalSchema: "auth",
                        principalTable: "ApiEndpoint",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PermissionForApiEndpoint_KeyPermission_KeyPermissionId",
                        column: x => x.KeyPermissionId,
                        principalSchema: "auth",
                        principalTable: "KeyPermission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PermissionForApiEndpoint_ApiEndpointId",
                schema: "auth",
                table: "PermissionForApiEndpoint",
                column: "ApiEndpointId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionForApiEndpoint_KeyPermissionId",
                schema: "auth",
                table: "PermissionForApiEndpoint",
                column: "KeyPermissionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PermissionForApiEndpoint",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "ApiEndpoint",
                schema: "auth");

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
                oldDefaultValue: new DateTime(2024, 9, 18, 14, 10, 14, 149, DateTimeKind.Local).AddTicks(6874));

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
                oldDefaultValue: new DateTime(2024, 9, 18, 14, 10, 14, 149, DateTimeKind.Local).AddTicks(5336));

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
                oldDefaultValue: new DateTime(2024, 9, 18, 14, 10, 14, 149, DateTimeKind.Local).AddTicks(7461));
        }
    }
}
