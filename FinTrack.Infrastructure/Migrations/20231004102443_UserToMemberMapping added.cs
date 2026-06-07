using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinTrack.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserToMemberMappingadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserToMemberMappings",
                columns: table => new
                {
                    Mapping_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_ID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BP_ID = table.Column<int>(type: "int", nullable: false),
                    Is_Active = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Request_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Processed_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Processed_By = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Is_Primary = table.Column<bool>(type: "bit", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_By = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Is_Suspended = table.Column<bool>(type: "bit", nullable: false),
                    LinkedAs = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToMemberMappings", x => x.Mapping_ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserToMemberMappings");
        }
    }
}
