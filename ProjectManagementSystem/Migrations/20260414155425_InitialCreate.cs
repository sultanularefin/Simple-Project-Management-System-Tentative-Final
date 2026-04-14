using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Designations",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Designations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Priorities",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    priorityName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Priorities", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectStatuses",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectStatuses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "TEXT", nullable: false),
                    password = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    status = table.Column<bool>(type: "INTEGER", nullable: false),
                    designationId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                    table.ForeignKey(
                        name: "FK_Users_Designations_designationId",
                        column: x => x.designationId,
                        principalTable: "Designations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    codeName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "text", unicode: false, nullable: false),
                    startDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    endDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    duration = table.Column<int>(type: "INTEGER", nullable: false),
                    filesId = table.Column<int>(type: "INTEGER", nullable: true),
                    statusId = table.Column<int>(type: "INTEGER", nullable: true),
                    shortName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.id);
                    table.ForeignKey(
                        name: "FK_Projects_ProjectStatuses_statusId",
                        column: x => x.statusId,
                        principalTable: "ProjectStatuses",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Role = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    userId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.id);
                    table.ForeignKey(
                        name: "FK_Roles_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "AllFiles",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    projectId = table.Column<int>(type: "INTEGER", nullable: false),
                    fname = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllFiles", x => x.id);
                    table.ForeignKey(
                        name: "FK_AllFiles_Projects_projectId",
                        column: x => x.projectId,
                        principalTable: "Projects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assignments",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    projectId = table.Column<int>(type: "INTEGER", nullable: false),
                    userId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignments", x => x.id);
                    table.ForeignKey(
                        name: "FK_Assignments_Projects_projectId",
                        column: x => x.projectId,
                        principalTable: "Projects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assignments_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    projectId = table.Column<int>(type: "INTEGER", nullable: false),
                    taskHeading = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    assigneeid = table.Column<int>(type: "INTEGER", nullable: false),
                    priorityId = table.Column<int>(type: "INTEGER", nullable: false),
                    assignedbyId = table.Column<int>(type: "INTEGER", nullable: false),
                    dueDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "text", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.id);
                    table.ForeignKey(
                        name: "FK_Tasks_Priorities_priorityId",
                        column: x => x.priorityId,
                        principalTable: "Priorities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Tasks_Projects_projectId",
                        column: x => x.projectId,
                        principalTable: "Projects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_Users_assigneeid",
                        column: x => x.assigneeid,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    taskId = table.Column<int>(type: "INTEGER", nullable: false),
                    comment = table.Column<string>(type: "text", unicode: false, nullable: false),
                    commentDate = table.Column<DateTime>(type: "DateTime", nullable: false),
                    commenterid = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.id);
                    table.ForeignKey(
                        name: "FK_Comments_Tasks_taskId",
                        column: x => x.taskId,
                        principalTable: "Tasks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_commenterid",
                        column: x => x.commenterid,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AllFiles_projectId",
                table: "AllFiles",
                column: "projectId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_projectId",
                table: "Assignments",
                column: "projectId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_userId",
                table: "Assignments",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_commenterid",
                table: "Comments",
                column: "commenterid");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_taskId",
                table: "Comments",
                column: "taskId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_statusId",
                table: "Projects",
                column: "statusId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_userId",
                table: "Roles",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_assigneeid",
                table: "Tasks",
                column: "assigneeid");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_priorityId",
                table: "Tasks",
                column: "priorityId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_projectId",
                table: "Tasks",
                column: "projectId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_designationId",
                table: "Users",
                column: "designationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllFiles");

            migrationBuilder.DropTable(
                name: "Assignments");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Priorities");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ProjectStatuses");

            migrationBuilder.DropTable(
                name: "Designations");
        }
    }
}
