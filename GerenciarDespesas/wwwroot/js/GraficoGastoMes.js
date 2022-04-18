$(".escolhaMes").on('change', function () {
    var mesId = $(".escolhaMes").val();

    $.ajax({
        url: "Despesas/GastosMes",
        method: "POST",
        data: { mesId: mesId },
        success: function (dados) {
            $("canvas#GraficoGastoMes").remove();
            $("div.GraficoGastoMes").append(' <canvas id="GraficoGastoMes" style="width: 400px; height: 400px;"></canvas>');
            var ctx = document.getElementById('GraficoGastoMes').getContext('2d');
            var grafico = new Chart(ctx, {
                type: 'doughnut',

                data:
                {
                    labels: PegarTipoDespesas(dados),
                    datasets: [
                        {
                            labels: "Gastos por Despesas",
                            backgroundColor: PegarCores(dados.length),
                            hoverBackgroundColor: PegarCores(dados.length),

                            data: PegarValores(dados)
                        }
                    ]
                },
                options: {
                    responsive: false,
                    title: {
                        display: true,
                        text: "Gasto por mes"
                    }
                }
            });
        }
    });
});

function CarregarDadosGastosMes() {

    $.ajax({
        url: "Despesas/GastosMes",
        method: "POST",
        data: { mesId: 1 },
        success: function (dados) {
            $("canvas#GraficoGastoMes").remove();
            $("div.GraficoGastoMes").append(' <canvas id="GraficoGastoMes" style="width: 400px; height: 400px;"></canvas>');
            var ctx = document.getElementById('GraficoGastoMes').getContext('2d');
            var grafico = new Chart(ctx, {
                type: 'doughnut',

                data:
                {
                    labels: PegarTipoDespesas(dados),
                    datasets: [
                        {
                            labels: "Gastos por Despesas",
                            backgroundColor: PegarCores(dados.length),
                            hoverBackgroundColor: PegarCores(dados.length),

                            data: PegarValores(dados)
                        }
                    ]
                },
                options: {
                    responsive: false,
                    title: {
                        display: true,
                        text: "Gasto por mes"
                    }
                }
            });
        }
    });
}