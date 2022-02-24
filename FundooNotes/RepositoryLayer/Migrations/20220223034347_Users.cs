using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class Users : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mobilNo = table.Column<long>(type: "bigint", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cpassword = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Address_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Note",
                columns: table => new
                {
                    NotesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsReminder = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsTrash = table.Column<bool>(type: "bit", nullable: false),
                    IsArchive = table.Column<bool>(type: "bit", nullable: false),
                    IsPin = table.Column<bool>(type: "bit", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Note", x => x.NotesId);
                    table.ForeignKey(
                        name: "FK_Note_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Collab",
                columns: table => new
                {
                    CollabId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollabEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotesId = table.Column<int>(type: "int", nullable: true),
                    userId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collab", x => x.CollabId);
                    table.ForeignKey(
                        name: "FK_Collab_Note_NotesId",
                        column: x => x.NotesId,
                        principalTable: "Note",
                        principalColumn: "NotesId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Collab_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Label",
                columns: table => new
                {
                    LabelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LabelName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NotesId = table.Column<int>(type: "int", nullable: true),
                    userId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Label", x => x.LabelId);
                    table.ForeignKey(
                        name: "FK_Label_Note_NotesId",
                        column: x => x.NotesId,
                        principalTable: "Note",
                        principalColumn: "NotesId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Label_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_userId",
                table: "Address",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Collab_NotesId",
                table: "Collab",
                column: "NotesId");

            migrationBuilder.CreateIndex(
                name: "IX_Collab_userId",
                table: "Collab",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Label_NotesId",
                table: "Label",
                column: "NotesId");

            migrationBuilder.CreateIndex(
                name: "IX_Label_userId",
                table: "Label",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Note_userId",
                table: "Note",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_email",
                table: "Users",
                column: "email",
                unique: true,
                filter: "[email] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Collab");

            migrationBuilder.DropTable(
                name: "Label");

            migrationBuilder.DropTable(
                name: "Note");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
