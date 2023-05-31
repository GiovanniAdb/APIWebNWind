function getSales() {
    const start = document.getElementById('start').value;
    const end = document.getElementById('end').value;
    const bodyPost = {
        startDate: start,
        endDate: end
    }
    // Load the Visualization API and the controls package.
    google.charts.load('current', {'packages':['corechart', 'controls']});
    google.charts.setOnLoadCallback(() => {
        fetch("http://localhost:83/Product/GetSales",
            {
                headers: { "Content-Type": "application/json" },
                credentials: 'include',
                method: 'POST',
                body: JSON.stringify(bodyPost)
            }
        )
            .then(response => {
                if (!response.ok) {
                    throw response;
                }
                return response.json();
            })
            .then(info => {
                cargarGraficaVentas(info);
                console.log(info)
            })
            .catch(error => console.log(error));
    });
}
function cargarGraficaVentas(info) {

    var data = new google.visualization.DataTable();
    data.addColumn('string', 'Mes/año');
    data.addColumn('number', 'Ventas');
  
    info.forEach(f => {
        console.log(f);
        if (f.month !== undefined) {
            data.addRow([f.yearmonth.toString(), f.sales]);
          }
      });
      
  
    var dashboard = new google.visualization.Dashboard(
      document.getElementById('dashboard_div')
    );
  
    var rangeSlider = new google.visualization.ControlWrapper({
      'controlType': 'NumberRangeFilter',
      'containerId': 'filter_div',
      'options': {
        'filterColumnLabel': 'Año'
      }
    });
  
    var pieChart = new google.visualization.ChartWrapper({
      'chartType': 'PieChart',
      'containerId': 'chart_div',
      'options': {
        'width': 300,
        'height': 300,
        'pieSliceText': 'value',
        'legend': 'right'
      }
    });
  
    dashboard.bind(rangeSlider, pieChart);
    dashboard.draw(data);

}

function getDesgloseVentasMensuales(){
    var name = document.getElementById('productName').value;
    console.log(name);
    google.charts.load('current', {'packages':['corechart', 'controls']});
    google.charts.setOnLoadCallback(() => {
        fetch("http://localhost:83/Product/GetDesgloseVentasMensualesChart?productName=" + name)
            .then(response => {
                if (!response.ok) {
                    throw response;
                }
                return response.json();
            })
            .then(info => {
                console.log(info);
                console.log(info.type);
                cargarGraficaProducto(info);
            })
            .catch(error => console.log(error));
    });
}

const cargarGraficaProducto = (info) => {

    var data = new google.visualization.DataTable();
    data.addColumn('string', 'Mes');
    data.addColumn('number', 'Ventas');
    data.addColumn('number', 'Año');
    console.log(info);
    info.forEach(f => {
        console.log(f);
        if (f.month !== undefined) {
            data.addRow([f.month, f.sales, f.year]);
          }
      });
      
  
    var dashboard = new google.visualization.Dashboard(
      document.getElementById('dashboard_div3')
    );
  
    var rangeSlider = new google.visualization.ControlWrapper({
      'controlType': 'NumberRangeFilter',
      'containerId': 'filter_div3',
      'options': {
        'filterColumnLabel': 'Año'
      }
    });
  
    var pieChart = new google.visualization.ChartWrapper({
      'chartType': 'PieChart',
      'containerId': 'chart_div3',
      'options': {
        'width': 300,
        'height': 300,
        'pieSliceText': 'value',
        'legend': 'right'
      }
    });
  
    dashboard.bind(rangeSlider, pieChart);
    dashboard.draw(data);

}