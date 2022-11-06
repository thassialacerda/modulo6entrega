using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agencia.Migrations
{
    public partial class PopulaTabela : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "Cep", "Destino", "Email", "Endereco", "Formapag", "Nome", "Quantidade" },
                values: new object[] { 1, 25051230, "Sao Paulo", "fabiords@live.com", "rua caravelas ", "Boleto", "Fabio Rodrigues", 2 });

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "Cep", "Destino", "Email", "Endereco", "Formapag", "Nome", "Quantidade" },
                values: new object[] { 2, 25051230, "Recife", "feliperds@live.com", "rua guaruja ", "Cartão", "Felipe Rodrigues", 4 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
