using GerenciarDespesas.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciarDespesas.Mapeamento
{
    public class DespesasMap : IEntityTypeConfiguration<Despesas>
    {
        public void Configure(EntityTypeBuilder<Despesas> builder)
        {
            builder.HasKey(d => d.DespesaId); //Minha chave primaria
            builder.Property(d => d.Valor).IsRequired(); // Minha propriedade sendo ela obrigatoria 
            //Relacionamento de 1 para muitos, ou seja, eu tenho um mes com varias despesas (Minha vida)
            builder.HasOne(d => d.Meses).WithMany(d => d.Despesas).HasForeignKey(d => d.MesId);
            //Relacionamento de 1 para muitos, ou seja, eu tenho um tipo de despesas pra varias depesas 
            builder.HasOne(d => d.TipoDespesas).WithMany(d => d.Despesas).HasForeignKey(d => d.TipoDespesasId);
            
            //Nome da tabela
            builder.ToTable("Despesas"); 




        }
    }
}
