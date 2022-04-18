using Microsoft.EntityFrameworkCore.Migrations;

namespace GerenciarDespesas.Migrations
{
    public partial class corrigindo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Despesas_TipoDespesas_MesId",
                table: "Despesas");

            migrationBuilder.CreateIndex(
                name: "IX_Despesas_TipoDespesaId",
                table: "Despesas",
                column: "TipoDespesaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Despesas_TipoDespesas_TipoDespesaId",
                table: "Despesas",
                column: "TipoDespesaId",
                principalTable: "TipoDespesas",
                principalColumn: "TipoDespesasId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Despesas_TipoDespesas_TipoDespesaId",
                table: "Despesas");

            migrationBuilder.DropIndex(
                name: "IX_Despesas_TipoDespesaId",
                table: "Despesas");

            migrationBuilder.AddForeignKey(
                name: "FK_Despesas_TipoDespesas_MesId",
                table: "Despesas",
                column: "MesId",
                principalTable: "TipoDespesas",
                principalColumn: "TipoDespesasId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
