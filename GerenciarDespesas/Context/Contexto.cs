using GerenciarDespesas.Mapeamento;
using GerenciarDespesas.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciarDespesas.Context
{
    public class Contexto : DbContext
    {
        public DbSet<Meses> Meses { get; set; }  
        public DbSet<Salario> Salarios { get; set; }
        public DbSet<Despesas> Despesas { get; set; }
        public DbSet<TipoDespesas> TipoDespesas { get; set; }

        public Contexto(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TipoDespesasMap());
            modelBuilder.ApplyConfiguration(new DespesasMap());
            modelBuilder.ApplyConfiguration(new SalarioMap());
            modelBuilder.ApplyConfiguration(new MesesMap());
        }
    }
}
