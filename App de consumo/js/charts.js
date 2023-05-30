google.charts.load('current', { 'packages': ['corechart'] });
google.charts.setOnLoadCallback(drawChart);

function drawChart() {
    $.ajax({
        url: '/api/product/GetDesgloseVentasMensuales?productName=ikura',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var chartData = [['Mes', 'Ventas']];

            $.each(data, function (mes, ventas) {
                chartData.push([mes, ventas]);
            });

            var options = {
                title: 'Desglose de ventas mensuales',
                hAxis: { title: 'Mes' },
                vAxis: { title: 'Ventas' },
                legend: 'none'
            };

            var chart = new google.visualization.ColumnChart(document.getElementById('primera-consulta'));
            chart.draw(google.visualization.arrayToDataTable(chartData), options);
        },
        error: function () {
            console.error('Error al obtener los datos del servidor.');
        }
    });
}
