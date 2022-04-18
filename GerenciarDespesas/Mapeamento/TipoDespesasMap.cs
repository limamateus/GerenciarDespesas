using GerenciarDespesas.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciarDespesas.Mapeamento
{
    public class TipoDespesasMap : IEntityTypeConfiguration<TipoDespesas>
    {

        public void Configure(EntityTypeBuilder<TipoDespesas> builder)
        {
            builder.HasKey(td => td.TipoDespesasId); // Chave primaria
            builder.Property(td => td.Nome).HasMaxLength(50).IsRequired(); // Propriedade com definação de no maximo 50 e é obrigatorio
            //Relação de muitos para 1, ou seja, eu tenho varias depesas para um tipo de despesas
            builder.HasMany(td => td.Despesas).WithOne(td => td.TipoDespesas).HasForeignKey(td => td.TipoDespesasId);
            //Nome da tabela
            builder.ToTable("TipoDespesas");
        }
    }

}
