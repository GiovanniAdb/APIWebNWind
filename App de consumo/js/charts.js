const getSales = () => {
  const start = document.getElementById('start').value;
  const end = document.getElementById('end').value;
  const bodyPost = {
    startDate: start,
    endDate: end
  }
  // Load the Visualization API and the controls package.
  google.charts.load('current', { 'packages': ['corechart', 'controls'] });
  google.charts.setOnLoadCallback(() => {
    fetch("http://192.168.155.17:83/Product/GetSales",
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
        cargarTablaVentas(info)
      })
      .catch(error => console.log(error));
  });
}


const cargarGraficaVentas = (info) => {
  var data = new google.visualization.DataTable();

  data.addColumn('date', 'Fecha');
  data.addColumn('number', 'Ventas');

  info.forEach((item) => {
    data.addRow([new Date(item.year, item.month), item.sales]);
  });

  var options = {
    title: 'Line Chart',
    curveType: 'function',
    legend: { position: 'bottom' }
  };
  var chart = new google.visualization.LineChart(document.getElementById('chart_div'));
  chart.draw(data, options);

  /*data.addColumn('string', 'Mes');
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
  */
}
const cargarTablaVentas = (info) => {
  const tableDiv = document.getElementById('table-div');
  tableDiv.innerHTML = ''; // Clear any existing content in the div

  // Create the table element
  const table = document.createElement('table');
  table.classList.add('table');
  table.classList.add('table-striped');

  // Create the table header
  const thead = document.createElement('thead');
  const headerRow = document.createElement('tr');
  ['Año', 'Mes', 'Total de Ventas'].forEach(title => {
    const th = document.createElement('th');
    th.textContent = title;
    headerRow.appendChild(th);
  });
  thead.appendChild(headerRow);
  table.appendChild(thead);

  // Create the table body
  const tbody = document.createElement('tbody');
  info.forEach(item => {
    const row = document.createElement('tr');
    ['year', 'month', 'sales'].forEach(key => {
      const cell = document.createElement('td');
      cell.textContent = item[key];
      row.appendChild(cell);
    });
    tbody.appendChild(row);
  });
  table.appendChild(tbody);

  // Append the table to the tableDiv
  tableDiv.appendChild(table);
};



const getDesgloseVentasMensuales = () => {
  var name = document.getElementById('productsCombo').value;
  console.log(name);

  google.charts.load('current', { 'packages': ['corechart', 'controls'] });
  google.charts.setOnLoadCallback(() => {
    fetch("http://192.168.155.17:83/Product/GetDesgloseVentasMensualesChart?productName=" + name.toString(), {
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
        cargarGraficaProducto(info);
        cargarTablaProducto(info)
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

const cargarTablaProducto = (info) => {
  const tableDiv = document.getElementById('table-producto-div');
  tableDiv.innerHTML = ''; // Clear any existing content in the div

  // Create the table element
  const table = document.createElement('table');
  table.classList.add('table');
  table.classList.add('table-striped');

  // Create the table header
  const thead = document.createElement('thead');
  const headerRow = document.createElement('tr');
  ['Año', 'Mes', 'Total de Ventas'].forEach(title => {
    const th = document.createElement('th');
    th.textContent = title;
    headerRow.appendChild(th);
  });
  thead.appendChild(headerRow);
  table.appendChild(thead);

  // Create the table body
  const tbody = document.createElement('tbody');
  info.forEach(item => {
    const row = document.createElement('tr');
    ['anio', 'mes', 'ventas'].forEach(key => {
      const cell = document.createElement('td');
      cell.textContent = item[key];
      row.appendChild(cell);
    });
    tbody.appendChild(row);
  });
  table.appendChild(tbody);

  // Append the table to the tableDiv
  tableDiv.appendChild(table);
};

$('document').ready(() => {
  fetch('http://192.168.155.17:83/Product/getAll', {
    headers: { "Content-Type": "application/json" },
    credentials: 'include',
    method: 'get',
  }).then( response => response.json() )
  .then( data => {
    let options = data.map( p => p.productName )
    var comboBox = document.getElementById('productsCombo')
    options.forEach( o =>{
      var option = document.createElement('option')
      option.value = o
      option.text = o
      comboBox.appendChild(option)
    } )

  } )
})