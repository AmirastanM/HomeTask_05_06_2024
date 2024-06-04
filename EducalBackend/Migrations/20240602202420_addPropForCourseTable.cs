using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducalBackend.Migrations
{
    public partial class addPropForCourseTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Categories_CategoryID",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "Courses",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_CategoryID",
                table: "Courses",
                newName: "IX_Courses_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Categories_CategoryId",
                table: "Courses",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Categories_CategoryId",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Courses",
                newName: "CategoryID");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_CategoryId",
                table: "Courses",
                newName: "IX_Courses_CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Categories_CategoryID",
                table: "Courses",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
