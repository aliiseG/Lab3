using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab2.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddAbstraction2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Cookbook_CookbooksId",
                table: "Recipes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cookbook",
                table: "Cookbook");

            migrationBuilder.RenameTable(
                name: "Cookbook",
                newName: "Cookbooks");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cookbooks",
                table: "Cookbooks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Cookbooks_CookbooksId",
                table: "Recipes",
                column: "CookbooksId",
                principalTable: "Cookbooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Cookbooks_CookbooksId",
                table: "Recipes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cookbooks",
                table: "Cookbooks");

            migrationBuilder.RenameTable(
                name: "Cookbooks",
                newName: "Cookbook");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cookbook",
                table: "Cookbook",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Cookbook_CookbooksId",
                table: "Recipes",
                column: "CookbooksId",
                principalTable: "Cookbook",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
