using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyNote.Migrations
{
  public partial class Added_ModifiedBy_ToFile : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AddColumn<string>(
        name: "ModifiedBy",
        table: "Files",
        nullable: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropColumn(
        name: "ModifiedBy",
        table: "Files");
    }
  }
}
