using GerenciarDespesas.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciarDespesas.Mapeamento
{
    public class MesesMap : IEntityTypeConfiguration<Meses>
    {
        public void Configure(EntityTypeBuilder<Meses> builder)
        {
            builder.HasKey(m => m.MesId); // Chave primaria
            builder.Property(m => m.MesId).ValueGeneratedNever(); // Banco não gerenciar o valor dele
            builder.Property(m => m.Nome).HasMaxLength(50).IsRequired(); // priedade nome , contendo no maximo 50 caracter e é obrigatorio
            // Relacionando muitos para 1, ou seja, varias despesas , para um mes e definindo que se deletar deleta tudo 
            builder.HasMany(m => m.Despesas).WithOne(m => m.Meses).HasForeignKey(m => m.MesId).OnDelete(DeleteBehavior.Cascade);
            // Relacionamento de 1 para 1 , ou seja, eu tenho um salario de um mes.
            builder.HasOne(m => m.Salarios).WithOne(m => m.Meses).OnDelete(DeleteBehavior.Cascade);

            //Definir o nome do salario 
            builder.ToTable("Meses");
        }
    }
}
