const getSales = () => {
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
const cargarGraficaVentas = (info) => {

    var data = new google.visualization.DataTable();
    data.addColumn('string', 'Mes');
    data.addColumn('number', 'Ventas');
    data.addColumn('number', "Anio")
    info.forEach(i => {
        console.log(i);
        if (i.month !== undefined) {
            data.addRow([i.year.toString() + '/' + i.month.toString(), i.sales, i.year]);
          }
      });
      
  
    var dashboard = new google.visualization.Dashboard(
      document.getElementById('dashboard_div')
    );
  
    var rangeSlider = new google.visualization.ControlWrapper({
      'controlType': 'NumberRangeFilter',
      'containerId': 'filter_div',
      'options': {
        'filterColumnLabel': 'Anio'
      }
    });
  
    var pieChart = new google.visualization.ChartWrapper({
      'chartType': 'PieChart',
      'containerId': 'chart_div',
      'options': {
        'width': 500,
        'height': 300,
        'pieSliceText': 'value',
        'legend': 'right'
      }
    });
  
    dashboard.bind(rangeSlider, pieChart);
    dashboard.draw(data);

}

const getDesgloseVentasMensuales = () => {
    var name = document.getElementById('productName').value;
    console.log(name);
    
    google.charts.load('current', {'packages':['corechart', 'controls']});
    google.charts.setOnLoadCallback(() => {
        fetch("http://localhost:83/Product/GetDesgloseVentasMensualesChart?productName=" + name.toString(),{
            headers: { "Content-Type": "application/json" },
            credentials: 'include',
            method: 'POST',
        })
            .then(response => {
                if (!response.ok) {
                    throw response;
                }
                return response.json();
            })
            .then(info => {
                console.log(info);
                cargarGraficaProducto(info);
            })
            .catch(error => console.log(error));
    });
}

const cargarGraficaProducto = (info) => {

    var data = new google.visualization.DataTable();
    data.addColumn('string', 'Anio');
    data.addColumn('number', 'Ventas');
    data.addColumn('number', 'Mes');
    info.forEach(f => {
        console.log(f);
        if (f.anio !== undefined) {
            data.addRow([f.anio.toString() + '/' + f.mes.toString(), f.ventas, f.mes]);
        }
    });
      
  
    var dashboard = new google.visualization.Dashboard(
      document.getElementById('dashboard_div3')
    );
  
    var rangeSlider = new google.visualization.ControlWrapper({
      'controlType': 'NumberRangeFilter',
      'containerId': 'filter_div3',
      'options': {
        'filterColumnLabel': 'Mes'
      }
    });
  
    var pieChart = new google.visualization.ChartWrapper({
      'chartType': 'PieChart',
      'containerId': 'chart_div3',
      'options': {
        'width': 500,
        'height': 300,
        'pieSliceText': 'value',
        'legend': 'right'
      }
    });
  
    dashboard.bind(rangeSlider, pieChart);
    dashboard.draw(data);

}