using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class AddOneToOneComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "654af813-00a2-4d12-a6c9-08072bfe2c95");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "79c3e221-0b24-46cc-a5e5-479926fa8dea");

            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "Comments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "Comments",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2dbf9767-b794-4842-8680-e6ffd26aff39", null, "User", "USER" },
                    { "bebb39ff-10e6-480c-8bd3-315c560f8416", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_AppUserId1",
                table: "Comments",
                column: "AppUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_AppUserId1",
                table: "Comments",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_AppUserId1",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_AppUserId1",
                table: "Comments");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2dbf9767-b794-4842-8680-e6ffd26aff39");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bebb39ff-10e6-480c-8bd3-315c560f8416");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "Comments");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "654af813-00a2-4d12-a6c9-08072bfe2c95", null, "User", "USER" },
                    { "79c3e221-0b24-46cc-a5e5-479926fa8dea", null, "Admin", "ADMIN" }
                });
        }
    }
}
