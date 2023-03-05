using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCInventarios.Migrations
{
    /// <inheritdoc />
    public partial class ImagenesProductoUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Foto",
                table: "Usuario",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Imagen",
                table: "Producto",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Foto",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Imagen",
                table: "Producto");
        }
    }
}
