using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookWiki.Shared.Data.Migrations
{
    /// <inheritdoc />
    public partial class AuthorDataEntry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("Author", new string[] { "Name", "Nationality", "BookId" }, new object[] { "TestName1", "TestNationality1", 1 });
            migrationBuilder.InsertData("Author", new string[] { "Name", "Nationality", "BookId" }, new object[] { "TestName2", "TestNationality2", 2 });
            migrationBuilder.InsertData("Author", new string[] { "Name", "Nationality", "BookId" }, new object[] { "TestName3", "TestNationality3", 3 });
            migrationBuilder.InsertData("Author", new string[] { "Name", "Nationality", "BookId" }, new object[] { "TestName4", "TestNationality4", 4 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
