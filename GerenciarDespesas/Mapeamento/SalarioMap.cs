using GerenciarDespesas.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciarDespesas.Mapeamento
{
    public class SalarioMap : IEntityTypeConfiguration<Salario>
    {
        public void Configure(EntityTypeBuilder<Salario> builder)
        {
            builder.HasKey(s => s.SalarioId); // Minha Chave Primaria
            builder.Property(s => s.Valor).IsRequired(); // minha propriedade sendo ela obrigatoria

            //Reçacionamento de 1 para 1 , ou seja, eu tenho um mes e um salario e minha chave estrangeira é MesId
            builder.HasOne(s => s.Meses).WithOne(s => s.Salarios).HasForeignKey<Salario>(s => s.MesId);
            //Nome da minha Tabela
            builder.ToTable("Salarios");

        }
    
    }
}
