function CarregarDadosGastosTotais() {
    $.ajax({
        url: 'Despesas/GastoTotais',
        method: 'POST',
        success: function (dados) {
            new Chart(document.getElementById("GraficoGastoTotais"), {
                type: 'line',

                data: {
                    labels: PegarMeses(dados),

                    datasets: [{
                        label: "Total gasto",
                        data: PegarValores(dados),
                        backgroundColor: "#ecf0f1",
                        borderColor: "#2980b9",
                        fill: false,
                        spanGaps: false
                    }]
                },
                options: {
                    title: {
                        display: true,
                        text: "Total gasto"
                    }
                }
            });
        }
    
    });
}