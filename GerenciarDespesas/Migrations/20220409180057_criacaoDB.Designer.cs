// <auto-generated />
using GerenciarDespesas.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GerenciarDespesas.Migrations
{
    [DbContext(typeof(Contexto))]
    [Migration("20220409180057_criacaoDB")]
    partial class criacaoDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.15")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GerenciarDespesas.Models.Despesas", b =>
                {
                    b.Property<int>("DespesaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MesId")
                        .HasColumnType("int");

                    b.Property<int>("TipoDespesaId")
                        .HasColumnType("int");

                    b.Property<double>("Valor")
                        .HasColumnType("float");

                    b.HasKey("DespesaId");

                    b.HasIndex("MesId");

                    b.ToTable("Despesas");
                });

            modelBuilder.Entity("GerenciarDespesas.Models.Meses", b =>
                {
                    b.Property<int>("MesId")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("MesId");

                    b.ToTable("Meses");
                });

            modelBuilder.Entity("GerenciarDespesas.Models.Salario", b =>
                {
                    b.Property<int>("SalarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MesId")
                        .HasColumnType("int");

                    b.Property<double>("Valor")
                        .HasColumnType("float");

                    b.HasKey("SalarioId");

                    b.HasIndex("MesId")
                        .IsUnique();

                    b.ToTable("Salarios");
                });

            modelBuilder.Entity("GerenciarDespesas.Models.TipoDespesas", b =>
                {
                    b.Property<int>("TipoDespesasId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("TipoDespesasId");

                    b.ToTable("TipoDespesas");
                });

            modelBuilder.Entity("GerenciarDespesas.Models.Despesas", b =>
                {
                    b.HasOne("GerenciarDespesas.Models.Meses", "Meses")
                        .WithMany("Despesas")
                        .HasForeignKey("MesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GerenciarDespesas.Models.TipoDespesas", "TipoDespesas")
                        .WithMany("Despesas")
                        .HasForeignKey("MesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Meses");

                    b.Navigation("TipoDespesas");
                });

            modelBuilder.Entity("GerenciarDespesas.Models.Salario", b =>
                {
                    b.HasOne("GerenciarDespesas.Models.Meses", "Meses")
                        .WithOne("Salarios")
                        .HasForeignKey("GerenciarDespesas.Models.Salario", "MesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Meses");
                });

            modelBuilder.Entity("GerenciarDespesas.Models.Meses", b =>
                {
                    b.Navigation("Despesas");

                    b.Navigation("Salarios");
                });

            modelBuilder.Entity("GerenciarDespesas.Models.TipoDespesas", b =>
                {
                    b.Navigation("Despesas");
                });
#pragma warning restore 612, 618
        }
    }
}
