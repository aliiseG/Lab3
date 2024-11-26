using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab2.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddAbstraction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CookbooksId",
                table: "Recipes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Cookbook",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cookbook", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_CookbooksId",
                table: "Recipes",
                column: "CookbooksId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Cookbook_CookbooksId",
                table: "Recipes",
                column: "CookbooksId",
                principalTable: "Cookbook",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Cookbook_CookbooksId",
                table: "Recipes");

            migrationBuilder.DropTable(
                name: "Cookbook");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_CookbooksId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "CookbooksId",
                table: "Recipes");
        }
    }
}
