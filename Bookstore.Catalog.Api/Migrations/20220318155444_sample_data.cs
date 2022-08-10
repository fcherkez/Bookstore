using Microsoft.EntityFrameworkCore.Migrations;

namespace Bookstore.Catalog.Api.Migrations
{
    public partial class sample_data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "PublisherID", "CompanyName", "Country", "Website" },
                values: new object[] { 1, "Test Publisher", "UK", "http://google.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "PublisherID",
                keyValue: 1);
        }
    }
}
