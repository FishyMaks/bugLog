using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace bugLog.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    TeamName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TeamMotto = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamId);
                });

            migrationBuilder.CreateTable(
                name: "UserAccesses",
                columns: table => new
                {
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    UniqueKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Access", x => x.TeamId);
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserPassword = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AllowEmailNotification = table.Column<bool>(type: "bit", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfile", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserProfile_Access",
                        column: x => x.TeamId,
                        principalTable: "UserAccesses",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserProfile_Teams",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Bugs",
                columns: table => new
                {
                    BugId = table.Column<int>(type: "int", nullable: false),
                    AssignedToTeamId = table.Column<int>(type: "int", nullable: false),
                    BugReportedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BugTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BugDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BugAssignedTo = table.Column<int>(type: "int", nullable: false),
                    BugStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BugSeverity = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    BugCreatedBy = table.Column<int>(type: "int", nullable: false),
                    BugCreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BugClosedBy = table.Column<int>(type: "int", nullable: true),
                    BugClosedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BugResolutionSummary = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bugs", x => x.BugId);
                    table.ForeignKey(
                        name: "FK_Bugs(Assigned)_UserProfile",
                        column: x => x.BugAssignedTo,
                        principalTable: "UserProfiles",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bugs(Closed)_UserProfile",
                        column: x => x.BugClosedBy,
                        principalTable: "UserProfiles",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bugs(Created)_UserProfile",
                        column: x => x.BugCreatedBy,
                        principalTable: "UserProfiles",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bugs_BugAssignedTo",
                table: "Bugs",
                column: "BugAssignedTo");

            migrationBuilder.CreateIndex(
                name: "IX_Bugs_BugClosedBy",
                table: "Bugs",
                column: "BugClosedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Bugs_BugCreatedBy",
                table: "Bugs",
                column: "BugCreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_TeamId",
                table: "UserProfiles",
                column: "TeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bugs");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropTable(
                name: "UserAccesses");

            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
