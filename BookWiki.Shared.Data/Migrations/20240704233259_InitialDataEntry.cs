using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace BookWiki.Shared.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialDataEntry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("Book", new string[] { "Title", "Summary", "PublicationYear" }, new object[] { "TestTitle1", "TestSummary1", 2024});
            migrationBuilder.InsertData("Book", new string[] { "Title", "Summary", "PublicationYear" }, new object[] { "TestTitle2", "TestSummary2", 2024 });
            migrationBuilder.InsertData("Book", new string[] { "Title", "Summary", "PublicationYear" }, new object[] { "TestTitle3", "TestSummary3", 2024 });
            migrationBuilder.InsertData("Book", new string[] { "Title", "Summary", "PublicationYear" }, new object[] { "TestTitle4", "TestSummary4", 2024 });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM book");
        }
    }
}
