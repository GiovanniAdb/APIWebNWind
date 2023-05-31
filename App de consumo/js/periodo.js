var btnBuscar = document.getElementById('btnBuscar');
        var titulo = document.getElementById('titulo');
        var tabla;

        function getPeriodo() {
            var year = prompt("Ingresa el año");
            if (year !== null && year !== "") {
                var url = "http://192.168.155.17:83/Product/GetTopFiveProducts/" + year;

                fetch(url)
                    .then(response => response.json())
                    .then(info => {
                        var idTabla = '#tblProductos';

                        // Verificar si la DataTable ya existe y destruirla
                        if (tabla && $.fn.DataTable.isDataTable(idTabla)) {
                            tabla.destroy();
                        }

                        titulo.textContent = "Productos Top 5 del año: " + year;

                        tabla = $(idTabla).DataTable({
                            data: info,
                            columns: [
                                { title: 'Producto', data: 'nombre' },
                                { title: 'Trimestre', data: 'trimestre' },
                                { title: 'Unidades Vendidas', data: 'unidadesVendidas' }
                            ],
                            "fnRowCallback": function (nRow, aData, iDisplayIndex) {
                                // Añadir estilo a una fila o columna dependiendo de algún valor
                            },
                            "fnInitComplete": function (oSettings, json) {
                                // Configuración de los filtros individuales
                            },
                            dom: 'Bfrtip',
                            colReorder: true,
                            buttons: [
                                'colvis',
                                'excelHtml5',
                                'pdfHtml5',
                                'copy',
                                'csv'
                            ],
                            order: [[1, 'asc']],
                            rowGroup: {
                                startRender: null,
                                endRender: function (rows, group) {
                                    var sum = rows
                                        .data()
                                        .pluck('unidadesVendidas')
                                        .reduce(function (a, b) {
                                            return a + b;
                                        }, 0);
                                    return 'Total Trimestre ' + group + ': ' + sum;
                                },
                                dataSrc: 'trimestre'
                            },
                            lengthMenu: [[25, 50, 100, -1], [25, 50, 100, "All"]],
                            language: {
                                search: "_INPUT_",
                                searchPlaceholder: "Buscar...",
                                lengthMenu: "_MENU_",
                                info: "Mostrando _START_ a _END_ de _TOTAL_ registros",
                                infoEmpty: "Mostrando 0 a 0 de 0 registros",
                                zeroRecords: "No se encontraron registros",
                                infoFiltered: "(filtrado de _MAX_ registros totales)",
                                buttons: {
                                    colvis: 'Columnas Visibles',
                                    colvisRestore: 'Restaurar',
                                    colvisNone: 'Ninguna'
                                }
                            },
                        });
                    })
                    .catch(error => console.log(error));
            } else {
                alert("Debes ingresar un año válido");
            }
        };