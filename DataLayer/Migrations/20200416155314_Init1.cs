using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class Init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Middlename = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Region = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    CompanyName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    SecondEmail = table.Column<string>(nullable: true),
                    Eyes = table.Column<string>(nullable: true),
                    Hair = table.Column<string>(nullable: true),
                    Nose = table.Column<string>(nullable: true),
                    Head = table.Column<string>(nullable: true),
                    Tattoo = table.Column<string>(nullable: true),
                    BestSkills = table.Column<string>(nullable: true),
                    Car = table.Column<string>(nullable: true),
                    LoveAnimal = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ChangeDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserWorkInformations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    Building = table.Column<string>(nullable: true),
                    Salary = table.Column<int>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Region = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    SecondPhone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    SecondEmail = table.Column<string>(nullable: true),
                    Rank = table.Column<string>(nullable: true),
                    Floor = table.Column<string>(nullable: true),
                    Colleagues = table.Column<string>(nullable: true),
                    KindOf = table.Column<string>(nullable: true),
                    TypeOfWork = table.Column<string>(nullable: true),
                    LCA = table.Column<string>(nullable: true),
                    NDA = table.Column<string>(nullable: true),
                    Parking = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ChangeDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWorkInformations", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UserWorkInformations");
        }
    }
}
