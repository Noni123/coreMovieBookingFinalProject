using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace coreUserPanel.Migrations
{
    public partial class ClassesTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    LocationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LocationName = table.Column<string>(nullable: true),
                    LocationImage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.LocationId);
                });

            migrationBuilder.CreateTable(
                name: "UserDetails",
                columns: table => new
                {
                    UserDetailID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    ContactNo = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDetails", x => x.UserDetailID);
                });

            migrationBuilder.CreateTable(
                name: "Multiplexes",
                columns: table => new
                {
                    MultiplexId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MultiplexName = table.Column<string>(nullable: true),
                    MultiplexDescription = table.Column<string>(nullable: true),
                    LocationId = table.Column<int>(nullable: false),
                    MultiplexImage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Multiplexes", x => x.MultiplexId);
                    table.ForeignKey(
                        name: "FK_Multiplexes_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BookingAmount = table.Column<double>(nullable: false),
                    BookingDate = table.Column<DateTime>(nullable: false),
                    UserDetailId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_Bookings_UserDetails_UserDetailId",
                        column: x => x.UserDetailId,
                        principalTable: "UserDetails",
                        principalColumn: "UserDetailID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Auditoriums",
                columns: table => new
                {
                    AuditoriumId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuditoriumName = table.Column<string>(nullable: true),
                    AuditoriumDescription = table.Column<string>(nullable: true),
                    Seats = table.Column<int>(nullable: false),
                    Time1 = table.Column<string>(nullable: true),
                    Time2 = table.Column<string>(nullable: true),
                    Time3 = table.Column<string>(nullable: true),
                    MultiplexId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auditoriums", x => x.AuditoriumId);
                    table.ForeignKey(
                        name: "FK_Auditoriums_Multiplexes_MultiplexId",
                        column: x => x.MultiplexId,
                        principalTable: "Multiplexes",
                        principalColumn: "MultiplexId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    MovieId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MovieName = table.Column<string>(nullable: true),
                    MoviePrice = table.Column<double>(nullable: false),
                    MovieImage = table.Column<string>(nullable: true),
                    MovieDuration = table.Column<string>(nullable: true),
                    MovieDescription = table.Column<string>(nullable: true),
                    MultiplexId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.MovieId);
                    table.ForeignKey(
                        name: "FK_Movies_Multiplexes_MultiplexId",
                        column: x => x.MultiplexId,
                        principalTable: "Multiplexes",
                        principalColumn: "MultiplexId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    pay = table.Column<double>(nullable: false),
                    BookingId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payments_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "BookingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookingDetails",
                columns: table => new
                {
                    BookingId = table.Column<int>(nullable: false),
                    MovieId = table.Column<int>(nullable: false),
                    QtySeats = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingDetails", x => new { x.BookingId, x.MovieId });
                    table.ForeignKey(
                        name: "FK_BookingDetails_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "BookingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingDetails_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieDetails",
                columns: table => new
                {
                    MovieDetailId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MovieId = table.Column<int>(nullable: false),
                    Casting = table.Column<string>(nullable: true),
                    Director = table.Column<string>(nullable: true),
                    Producer = table.Column<string>(nullable: true),
                    MovieType = table.Column<string>(nullable: true),
                    MovieImage = table.Column<string>(nullable: true),
                    MovieDuration = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieDetails", x => x.MovieDetailId);
                    table.ForeignKey(
                        name: "FK_MovieDetails_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    UserDetailID = table.Column<int>(nullable: false),
                    MovieId = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => new { x.UserDetailID, x.MovieId });
                    table.ForeignKey(
                        name: "FK_Reviews_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_UserDetails_UserDetailID",
                        column: x => x.UserDetailID,
                        principalTable: "UserDetails",
                        principalColumn: "UserDetailID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Auditoriums_MultiplexId",
                table: "Auditoriums",
                column: "MultiplexId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetails_MovieId",
                table: "BookingDetails",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserDetailId",
                table: "Bookings",
                column: "UserDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieDetails_MovieId",
                table: "MovieDetails",
                column: "MovieId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movies_MultiplexId",
                table: "Movies",
                column: "MultiplexId");

            migrationBuilder.CreateIndex(
                name: "IX_Multiplexes_LocationId",
                table: "Multiplexes",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_BookingId",
                table: "Payments",
                column: "BookingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_MovieId",
                table: "Reviews",
                column: "MovieId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserDetailID",
                table: "Reviews",
                column: "UserDetailID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "AK_Reviews_MovieId_UserDetailID",
                table: "Reviews",
                columns: new[] { "MovieId", "UserDetailID" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Auditoriums");

            migrationBuilder.DropTable(
                name: "BookingDetails");

            migrationBuilder.DropTable(
                name: "MovieDetails");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "UserDetails");

            migrationBuilder.DropTable(
                name: "Multiplexes");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
