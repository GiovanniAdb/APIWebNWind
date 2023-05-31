using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFinalAPI.Data;
using ProyectoFinalAPI.Models;
using System.Globalization;

namespace ProyectoFinalAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly NorthwindContext contexto;

        public ProductController(NorthwindContext context)
        {
            contexto = context;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return contexto.Products.OrderBy(p => p.ProductName);

        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> getAll()
        {
            var result = await contexto.Products.OrderBy(p => p.ProductName).ToListAsync();


            return Ok(result);

        }

        [HttpGet]
        [Route("GetNameAndPrice")]
        public IEnumerable<object> GetNameAndPrice()
        {
            IEnumerable<object> lista =
                from producto in contexto.Products
                select new
                {
                    Name = producto.ProductName,
                    Price = producto.UnitPrice,
                    Category = producto.Category != null ? producto.Category.CategoryName : null
                };
            return lista;
        }

        [HttpGet]
        [Route("GetNameAndPrice2")]
        public IEnumerable<Product> GetNameAndPrice2()
        {
            IEnumerable<Product> listaP =
                from producto in contexto.Products
                select new Product()
                {
                    ProductName = producto.ProductName,
                    UnitPrice = producto.UnitPrice
                };
            return listaP;
        }
        [HttpGet]
        [Route("GetProductStatistics")]
        public IEnumerable<object> GetProductStatistics()
        {
            var result =
                from od in contexto.OrderDetails
                join o in contexto.Order on od.OrderId equals o.OrderId
                join p in contexto.Products on od.ProductId equals p.ProductId
                where o.OrderDate.HasValue && o.OrderDate.Value.Year == 1996
                group od by new
                {
                    p.ProductName,
                    p.QuantityPerUnit,
                    p.UnitPrice
                } into g
                select new
                {
                    Articulo = g.Key.ProductName,
                    Unidades = g.Key.QuantityPerUnit,
                    PrecioUnitario = g.Key.UnitPrice,
                    VolumenDemandadoEn1996 = g.Sum(od => od.Quantity)
                };

            return result;
        }

        [HttpGet]
        [Route("GetProductsNoDiscontinued")]
        public IEnumerable<object> GetProductsNoDiscontinued()
        {
            var result =
                from c in contexto.Categories
                join p in contexto.Products on c.CategoryId equals p.CategoryId
                where p.Discontinued == 0
                select new
                {
                    Nombre = p.ProductName,
                    Categoria = c.CategoryName,
                    Existencias = p.UnitsInStock
                };
            return result;
        }

        [HttpGet]
        [Route("GetProductsNoDiscontinued2")]
        public IEnumerable<object> GetProductsNoDiscontinued2()
        {
            var result =
                contexto.Products.
                Where(p => p.Discontinued == 0).
                Join(contexto.Categories,
                (p) => p.CategoryId,
                (c) => c.CategoryId,
                (p, c) =>
            new
            {
                Nombre = p.ProductName,
                Categoria = c.CategoryName,
                Existencias = p.UnitsInStock
            });

            return result;
        }

        [HttpGet]
        [Route("GetProductsNoDiscontinued3")]
        public IEnumerable<object> GetProductsNoDiscontinued3()
        {
            var result =
                contexto.Products.
                Where(p => p.Discontinued == 0).
                Join(contexto.Categories,
                (p) => p.CategoryId,
                (c) => c.CategoryId,
                (p, c) =>
            new
            {
                Nombre = p.ProductName,
                Categoria = c.CategoryName,
                Existencias = p.UnitsInStock,
                Activo = p.Discontinued
            }).Where(p => p.Activo == 0);

            return result;
        }

        [HttpGet]
        [Route("GetCategoryInventory")]
        public IEnumerable<object> GetCategoryInventory()
        {
            var result =
                from p in contexto.Products
                join c in contexto.Categories on p.CategoryId equals c.CategoryId
                group p by c.CategoryName into g
                select new
                {
                    Categoria = g.Key,
                    Productos = g.Count(),
                    ValorInventario = g.Sum(p => p.UnitsInStock * p.UnitPrice)
                };

            return result;
        }

        [HttpGet]
        [Route("GetCategoryInventory2")]
        public IEnumerable<object> GetCategoryInventory2()
        {
            var result = contexto.Products
                .Join(
                    contexto.Categories,
                    p => p.CategoryId,
                    c => c.CategoryId,
                    (p, c) => new { Product = p, Category = c })
                .GroupBy(
                    pc => pc.Category.CategoryName,
                    (key, g) => new
                    {
                        Categoria = key,
                        Productos = g.Count(),
                        ValorInventario = g.Sum(pc => pc.Product.UnitsInStock * pc.Product.UnitPrice)
                    });

            return result;
        }

        [HttpGet]
        [Route("GetInventarioCategoria")]
        public IEnumerable<object> GetInventarioCategoria()
        {
            IEnumerable<object> lista =
                contexto.Products.
                Join(contexto.Categories,
                (p) => p.CategoryId,
                (c) => c.CategoryId,
                (p, c) =>
                    new
                    {
                        Categoria = c.CategoryName,
                        Existencia = p.UnitPrice
                    }
                ).GroupBy(pc => pc.Categoria)
                .Select(grupo =>
                    new {
                        Categoria = grupo.Key,
                        Inventario = grupo.Sum(g => g.Existencia)
                    }
                );

            return lista;
        }

 

        [HttpGet]
        [Route("GetTopFiveProducts/{year}")]
        public IEnumerable<object> GetTopFiveProducts(int year)
        {
            var startDate = new DateTime(year, 1, 1);
            var endDate = startDate.AddYears(1).AddDays(-1);

            var result = new List<object>();

            for (int quarter = 1; quarter <= 4; quarter++)
            {
                var quarterlyResult = contexto.Products
                    .Join(contexto.OrderDetails,
                        p => p.ProductId,
                        od => od.ProductId,
                        (p, od) => new { Product = p, OrderDetail = od })
                    .Join(contexto.Order,
                        join1 => join1.OrderDetail.OrderId,
                        o => o.OrderId,
                        (join1, o) => new { join1.Product, join1.OrderDetail, Order = o })
                    .Where(join2 => join2.Order.OrderDate.Value >= startDate &&
                                     join2.Order.OrderDate.Value <= endDate &&
                                     ((join2.Order.OrderDate.Value.Month - 1) / 3 + 1) == quarter)
                    .GroupBy(join2 => new { join2.Product.ProductName, Quarter = quarter })
                    .Select(group => new
                    {
                        Nombre = group.Key.ProductName,
                        Trimestre = group.Key.Quarter,
                        UnidadesVendidas = group.Sum(g => g.OrderDetail.Quantity)
                    })
                    .OrderByDescending(item => item.UnidadesVendidas)
                    .Take(5);

                result.AddRange(quarterlyResult);
            }

            return result;
        }

        [HttpGet]
        [Route("GetDesgloseVentasMensuales")]
        public IEnumerable<object> GetDesgloseVentasMensuales(string productName)
        {
            IEnumerable<object> lista =
                contexto.Products
                .Join(contexto.OrderDetails,
                    p => p.ProductId,
                    od => od.ProductId,
                    (p, od) => new
                    {
                        Producto = p.ProductName,
                        Fecha = od.Order.OrderDate,
                        Ventas = od.Quantity * od.UnitPrice
                    })
                .Where(pod => pod.Producto == productName)
                .ToList() // Ejecutar la consulta y obtener los datos en memoria
                .GroupBy(pod => new { Fecha = pod.Fecha.HasValue ? new { Year = pod.Fecha.Value.Year, Month = pod.Fecha.Value.Month } : null })
                .OrderBy(podGroup => podGroup.Key.Fecha != null ? podGroup.Key.Fecha.Year : 0)
                .ThenBy(podGroup => podGroup.Key.Fecha != null ? podGroup.Key.Fecha.Month : 0)
                .Select(podGroup =>
                    new
                    {
                        Mes = podGroup.Key.Fecha != null ? $"{podGroup.Key.Fecha.Year}-{podGroup.Key.Fecha.Month:00}" : string.Empty,
                        Ventas = podGroup.Sum(pod => pod.Ventas)
                    });

            return lista;
        }

        [HttpPost]
        [Route("GetDesgloseVentasMensualesChart")]
        public IEnumerable<object> GetDesgloseVentasMensualesChart(string productName)
        {

            var results = contexto.Products
                .Join(contexto.OrderDetails,
                    p => p.ProductId,
                    od => od.ProductId,
                    (p, od) => new
                    {
                        Producto = p.ProductName,
                        Fecha = od.Order.OrderDate,
                        Ventas = od.Quantity * od.UnitPrice
                    })
                .Where(pod => pod.Producto == productName)
                .ToList()
                .GroupBy(pod => new { Fecha = pod.Fecha.HasValue ? new { Year = pod.Fecha.Value.Year, Month = pod.Fecha.Value.Month } : null })
                .OrderBy(podGroup => podGroup.Key.Fecha != null ? podGroup.Key.Fecha.Year : 0)
                .ThenBy(podGroup => podGroup.Key.Fecha != null ? podGroup.Key.Fecha.Month : 0)
                .Select(podGroup =>
                    new
                    {
                        Mes = podGroup.Key.Fecha.Month,
                        Anio = podGroup.Key.Fecha.Year,
                        Ventas = podGroup.Sum(pod => pod.Ventas)
                    });
            return results;
        }
        [HttpPost]
        [Route("GetSales")]
        public IEnumerable<object> GetSales([FromBody] SalesRequest request)
        {
            DateTime startDate = request.StartDate;
            DateTime endDate = request.EndDate;
            var result =
                from o in contexto.Order
                join od in contexto.OrderDetails on o.OrderId equals od.OrderId
                where o.OrderDate >= startDate && o.OrderDate <= endDate
                group new { od.UnitPrice, od.Quantity } by new { Year = o.OrderDate.Value.Year, Month = o.OrderDate.Value.Month } into g
                orderby g.Key.Year, g.Key.Month
                select new
                {
                    month = g.Key.Month,
                    year = g.Key.Year,
                    sales = g.Sum(x => x.UnitPrice * x.Quantity)
                };

            return result;
        }


        public class SalesRequest
        {
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
        }

    }
}
