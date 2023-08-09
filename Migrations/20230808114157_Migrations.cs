using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace makeyourtrip.Migrations
{
    /// <inheritdoc />
    public partial class Migrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adminis",
                columns: table => new
                {
                    IId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Images = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adminis", x => x.IId);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TourId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    No_Of_Persons = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingId);
                });

            migrationBuilder.CreateTable(
                name: "Tours",
                columns: table => new
                {
                    TourId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TravelAgencyId = table.Column<int>(type: "int", nullable: false),
                    TourName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TourCountry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TourState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TourCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Itenary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type_of_package = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PackagePrice = table.Column<int>(type: "int", nullable: false),
                    Extra_Person_Price = table.Column<int>(type: "int", nullable: false),
                    AgencyImages = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tours", x => x.TourId);
                });

            migrationBuilder.CreateTable(
                name: "Travellers",
                columns: table => new
                {
                    TravellerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TravellerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TravellerEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TravellerState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TravellerCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TravellerPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TravellerPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Travellers", x => x.TravellerId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApprovalStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Travelagencies",
                columns: table => new
                {
                    TraveAgencyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TravelAgencyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TravelAgencyEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TravelAgencyCountry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TravelAgencyState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TravelAgencyCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TravelAgencyPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TravelAgencyPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveStatus = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Travelagencies", x => x.TraveAgencyId);
                    table.ForeignKey(
                        name: "FK_Travelagencies_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Travelagencies_UserId",
                table: "Travelagencies",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Adminis");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Tours");

            migrationBuilder.DropTable(
                name: "Travelagencies");

            migrationBuilder.DropTable(
                name: "Travellers");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
