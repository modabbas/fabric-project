using Microsoft.EntityFrameworkCore.Migrations;

namespace FabricProject.SqlServer.Migrations
{
    public partial class ChangeInMachineTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_AspNetUserTokens_Id",
                table: "AspNetUserTokens");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_AspNetUserLogins_Id",
                table: "AspNetUserLogins");

            migrationBuilder.AlterColumn<int>(
                name: "Number",
                table: "Machines",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "MachineType",
                table: "Machines",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "3cce4047-804e-459f-826e-e62117a51753");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "00c68b10-6d53-4f6d-be59-4ab840585f70");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "fed079f3-5836-4e09-96d8-32e65bdbca0c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "e0f618d1-4f8d-4c4a-90d1-199064113590");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 5,
                column: "ConcurrencyStamp",
                value: "7855eb2f-daa3-45f1-9ff8-7910e2edf515");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Number",
                table: "Machines",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MachineType",
                table: "Machines",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_AspNetUserTokens_Id",
                table: "AspNetUserTokens",
                column: "Id");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_AspNetUserLogins_Id",
                table: "AspNetUserLogins",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "82582d95-30d7-404b-a7b8-70304b9af9ad");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "9adb409c-77cf-475a-bfbc-8f7ed48d257b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "e94a6b8b-e79d-48cc-866a-87d1a531e702");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "350de13e-2fe3-45b4-b2e8-b1c640f84f7d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 5,
                column: "ConcurrencyStamp",
                value: "de29662a-2a26-4560-b9d1-0f9381fadd2c");
        }
    }
}
