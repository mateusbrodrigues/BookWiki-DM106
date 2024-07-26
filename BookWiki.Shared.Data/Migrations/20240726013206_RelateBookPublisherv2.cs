using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookWiki.Shared.Data.Migrations
{
    /// <inheritdoc />
    public partial class RelateBookPublisherv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Publisher_PublisherId",
                table: "Book");

            migrationBuilder.DropIndex(
                name: "IX_Book_PublisherId",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "PublisherId",
                table: "Book");

            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "Publisher",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Publisher_BookId",
                table: "Publisher",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Publisher_Book_BookId",
                table: "Publisher",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publisher_Book_BookId",
                table: "Publisher");

            migrationBuilder.DropIndex(
                name: "IX_Publisher_BookId",
                table: "Publisher");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Publisher");

            migrationBuilder.AddColumn<int>(
                name: "PublisherId",
                table: "Book",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Book_PublisherId",
                table: "Book",
                column: "PublisherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Publisher_PublisherId",
                table: "Book",
                column: "PublisherId",
                principalTable: "Publisher",
                principalColumn: "Id");
        }
    }
}
