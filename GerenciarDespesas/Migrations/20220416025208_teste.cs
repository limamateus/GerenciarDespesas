using Microsoft.EntityFrameworkCore.Migrations;

namespace GerenciarDespesas.Migrations
{
    public partial class teste : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Despesas_TipoDespesas_TipoDespesaId",
                table: "Despesas");

            migrationBuilder.RenameColumn(
                name: "TipoDespesaId",
                table: "Despesas",
                newName: "TipoDespesasId");

            migrationBuilder.RenameIndex(
                name: "IX_Despesas_TipoDespesaId",
                table: "Despesas",
                newName: "IX_Despesas_TipoDespesasId");

            migrationBuilder.AddForeignKey(
                name: "FK_Despesas_TipoDespesas_TipoDespesasId",
                table: "Despesas",
                column: "TipoDespesasId",
                principalTable: "TipoDespesas",
                principalColumn: "TipoDespesasId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Despesas_TipoDespesas_TipoDespesasId",
                table: "Despesas");

            migrationBuilder.RenameColumn(
                name: "TipoDespesasId",
                table: "Despesas",
                newName: "TipoDespesaId");

            migrationBuilder.RenameIndex(
                name: "IX_Despesas_TipoDespesasId",
                table: "Despesas",
                newName: "IX_Despesas_TipoDespesaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Despesas_TipoDespesas_TipoDespesaId",
                table: "Despesas",
                column: "TipoDespesaId",
                principalTable: "TipoDespesas",
                principalColumn: "TipoDespesasId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
