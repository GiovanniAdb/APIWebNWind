using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ProyectoFinalAPI.Models;

namespace ProyectoFinalAPI.Data;

public partial class NorthwindContext : DbContext
{
    public NorthwindContext(DbContextOptions<NorthwindContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aplicacione> Aplicaciones { get; set; }

    public virtual DbSet<Aspirante> Aspirantes { get; set; }

    public virtual DbSet<BancoReactivo> BancoReactivos { get; set; }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Configuracione> Configuraciones { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Customerdemographic> Customerdemographics { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Examene> Examenes { get; set; }

    public virtual DbSet<Opcione> Opciones { get; set; }

    public virtual DbSet<Order> Order { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<PuntuacionPorCategorium> PuntuacionPorCategoria { get; set; }

    public virtual DbSet<Reactivo> Reactivos { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<Respuesta> Respuestas { get; set; }

    public virtual DbSet<ResultadoDetallado> ResultadoDetallados { get; set; }

    public virtual DbSet<Shipper> Shippers { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<Territory> Territories { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Ventasporcategorium> Ventasporcategoria { get; set; }

    public virtual DbSet<Ventasproducto1997> Ventasproducto1997s { get; set; }

    public virtual DbSet<Vistum> Vista { get; set; }

    public virtual DbSet<Vwempleado> Vwempleados { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aplicacione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("aplicaciones");

            entity.HasIndex(e => e.ExamenId, "fk_puntuaciones_examenes1");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ExamenId).HasColumnName("examen_id");
            entity.Property(e => e.FechaFinalizacion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_finalizacion");
            entity.Property(e => e.Finalizado)
                .HasDefaultValueSql("'N'")
                .HasColumnType("enum('S','N')")
                .HasColumnName("finalizado");
            entity.Property(e => e.PuntuacionTotal)
                .HasDefaultValueSql("'0'")
                .HasColumnName("puntuacion_total");
            entity.Property(e => e.RegTimeStamp)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("reg_time_stamp");
            entity.Property(e => e.UltimoReactivoAbiertoId).HasColumnName("ultimo_reactivo_abierto_id");

            entity.HasOne(d => d.Examen).WithMany(p => p.Aplicaciones)
                .HasForeignKey(d => d.ExamenId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_puntuaciones_examenes1");
        });

        modelBuilder.Entity<Aspirante>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("aspirantes");

            entity.HasIndex(e => e.AplicacionId, "fk_aspirantes_aplicaciones1");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(15)
                .HasColumnName("apellido_materno");
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(15)
                .HasColumnName("apellido_paterno");
            entity.Property(e => e.AplicacionId).HasColumnName("aplicacion_id");
            entity.Property(e => e.Bachillerato)
                .HasMaxLength(50)
                .HasColumnName("bachillerato");
            entity.Property(e => e.CarClave).HasColumnName("car_Clave");
            entity.Property(e => e.CarreraSolicitada)
                .HasMaxLength(50)
                .HasColumnName("carrera_solicitada");
            entity.Property(e => e.Folio).HasColumnName("folio");
            entity.Property(e => e.NoControlItsur)
                .HasMaxLength(10)
                .HasComment("No se tiene desde SICE, pero se puede incorporar mas adelante para tener la rastreabilidad completa del alumno.")
                .HasColumnName("no_control_itsur");
            entity.Property(e => e.Nombre)
                .HasMaxLength(25)
                .HasColumnName("nombre");
            entity.Property(e => e.Password)
                .HasMaxLength(42)
                .HasColumnName("password");
            entity.Property(e => e.Periodo)
                .HasMaxLength(6)
                .HasComment("AD2019\nEJ2019\nAD2020\nEJ2020\n...\n..\n")
                .HasColumnName("periodo");
            entity.Property(e => e.PreparatoriaProcedencia)
                .HasMaxLength(30)
                .HasColumnName("preparatoria_procedencia");
            entity.Property(e => e.PromedioBachillerato)
                .HasPrecision(5)
                .HasColumnName("promedio_bachillerato");
            entity.Property(e => e.RegTimeStamp)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("reg_time_stamp");
            entity.Property(e => e.Sexo)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("sexo");

            entity.HasOne(d => d.Aplicacion).WithMany(p => p.Aspirantes)
                .HasForeignKey(d => d.AplicacionId)
                .HasConstraintName("fk_aspirantes_aplicaciones1");
        });

        modelBuilder.Entity<BancoReactivo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("banco_reactivos");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CategoriasSugeridas)
                .HasMaxLength(100)
                .HasColumnName("categorias_sugeridas");
            entity.Property(e => e.Texto)
                .HasColumnType("text")
                .HasColumnName("texto");
            entity.Property(e => e.Tipo)
                .HasDefaultValueSql("'MULTIPLECHOICE'")
                .HasColumnType("enum('MULTIPLECHOICE','TRUEFALSE')")
                .HasColumnName("tipo");
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.ExamenId }).HasName("PRIMARY");

            entity.ToTable("categorias");

            entity.HasIndex(e => e.ExamenId, "fk_categorias_examenes1");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.ExamenId).HasColumnName("examen_id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(45)
                .HasColumnName("nombre");
            entity.Property(e => e.TiempoEjecucion)
                .HasComment("En segundos")
                .HasColumnName("tiempo_ejecucion");

            entity.HasOne(d => d.Examen).WithMany(p => p.Categoria)
                .HasForeignKey(d => d.ExamenId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_categorias_examenes1");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PRIMARY");

            entity.ToTable("categories");

            entity.HasIndex(e => e.CategoryName, "CategoryName");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName).HasMaxLength(15);
        });

        modelBuilder.Entity<Configuracione>(entity =>
        {
            entity.HasKey(e => e.PeriodoActual).HasName("PRIMARY");

            entity.ToTable("configuraciones");

            entity.HasIndex(e => e.PeriodoActual, "periodo_actual_UNIQUE").IsUnique();

            entity.Property(e => e.PeriodoActual)
                .HasMaxLength(6)
                .HasComment("AD2019\nEJ2019\nAD2020\nEJ2020\n...\n..")
                .HasColumnName("periodo_actual");
            entity.Property(e => e.PasswordGeneral)
                .HasMaxLength(42)
                .HasColumnName("password_general");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PRIMARY");

            entity.ToTable("customers");

            entity.HasIndex(e => e.City, "City");

            entity.HasIndex(e => e.CompanyName, "CompanyName");

            entity.HasIndex(e => e.PostalCode, "PostalCode");

            entity.HasIndex(e => e.Region, "Region");

            entity.Property(e => e.CustomerId)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("CustomerID");
            entity.Property(e => e.Address).HasMaxLength(60);
            entity.Property(e => e.City).HasMaxLength(15);
            entity.Property(e => e.CompanyName).HasMaxLength(40);
            entity.Property(e => e.ContactName).HasMaxLength(30);
            entity.Property(e => e.ContactTitle).HasMaxLength(30);
            entity.Property(e => e.Country).HasMaxLength(15);
            entity.Property(e => e.Fax).HasMaxLength(24);
            entity.Property(e => e.Phone).HasMaxLength(24);
            entity.Property(e => e.PostalCode).HasMaxLength(10);
            entity.Property(e => e.Region).HasMaxLength(15);

            entity.HasMany(d => d.CustomerTypes).WithMany(p => p.Customers)
                .UsingEntity<Dictionary<string, object>>(
                    "Customercustomerdemo",
                    r => r.HasOne<Customerdemographic>().WithMany()
                        .HasForeignKey("CustomerTypeId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CustomerCustomerDemo"),
                    l => l.HasOne<Customer>().WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CustomerCustomerDemo_Customers"),
                    j =>
                    {
                        j.HasKey("CustomerId", "CustomerTypeId").HasName("PRIMARY");
                        j.ToTable("customercustomerdemo");
                        j.HasIndex(new[] { "CustomerTypeId" }, "FK_CustomerCustomerDemo");
                        j.IndexerProperty<string>("CustomerId")
                            .HasMaxLength(5)
                            .IsFixedLength()
                            .HasColumnName("CustomerID");
                        j.IndexerProperty<string>("CustomerTypeId")
                            .HasMaxLength(10)
                            .IsFixedLength()
                            .HasColumnName("CustomerTypeID");
                    });
        });

        modelBuilder.Entity<Customerdemographic>(entity =>
        {
            entity.HasKey(e => e.CustomerTypeId).HasName("PRIMARY");

            entity.ToTable("customerdemographics");

            entity.Property(e => e.CustomerTypeId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("CustomerTypeID");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PRIMARY");

            entity.ToTable("employees");

            entity.HasIndex(e => e.ReportsTo, "FK_Employees_Employees");

            entity.HasIndex(e => e.LastName, "LastName");

            entity.HasIndex(e => e.PostalCode, "PostalCode");

            entity.HasIndex(e => e.FirstName, "inde");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Address).HasMaxLength(60);
            entity.Property(e => e.BirthDate).HasColumnType("datetime");
            entity.Property(e => e.City).HasMaxLength(15);
            entity.Property(e => e.Country).HasMaxLength(15);
            entity.Property(e => e.Extension).HasMaxLength(4);
            entity.Property(e => e.FirstName).HasMaxLength(10);
            entity.Property(e => e.HireDate).HasColumnType("datetime");
            entity.Property(e => e.HomePhone).HasMaxLength(24);
            entity.Property(e => e.LastName).HasMaxLength(20);
            entity.Property(e => e.PhotoPath).HasMaxLength(255);
            entity.Property(e => e.PostalCode).HasMaxLength(10);
            entity.Property(e => e.Region).HasMaxLength(15);
            entity.Property(e => e.Title).HasMaxLength(30);
            entity.Property(e => e.TitleOfCourtesy).HasMaxLength(25);

            entity.HasOne(d => d.ReportsToNavigation).WithMany(p => p.InverseReportsToNavigation)
                .HasForeignKey(d => d.ReportsTo)
                .HasConstraintName("FK_Employees_Employees");

            entity.HasMany(d => d.Territories).WithMany(p => p.Employees)
                .UsingEntity<Dictionary<string, object>>(
                    "Employeeterritory",
                    r => r.HasOne<Territory>().WithMany()
                        .HasForeignKey("TerritoryId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_EmployeeTerritories_Territories"),
                    l => l.HasOne<Employee>().WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_EmployeeTerritories_Employees"),
                    j =>
                    {
                        j.HasKey("EmployeeId", "TerritoryId").HasName("PRIMARY");
                        j.ToTable("employeeterritories");
                        j.HasIndex(new[] { "TerritoryId" }, "FK_EmployeeTerritories_Territories");
                        j.IndexerProperty<int>("EmployeeId").HasColumnName("EmployeeID");
                        j.IndexerProperty<string>("TerritoryId")
                            .HasMaxLength(20)
                            .HasColumnName("TerritoryID");
                    });
        });

        modelBuilder.Entity<Examene>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("examenes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activo)
                .HasColumnType("enum('S','N')")
                .HasColumnName("activo");
            entity.Property(e => e.CarClave).HasColumnName("car_clave");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(150)
                .HasColumnName("descripcion");
            entity.Property(e => e.RegTimeStamp)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("reg_time_stamp");
            entity.Property(e => e.Titulo)
                .HasMaxLength(45)
                .HasColumnName("titulo");
        });

        modelBuilder.Entity<Opcione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("opciones");

            entity.HasIndex(e => e.ReactivoBancoId, "fk_opciones_banco_reactivos1");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EsCorrecta)
                .HasDefaultValueSql("'N'")
                .HasColumnType("enum('S','N')")
                .HasColumnName("es_correcta");
            entity.Property(e => e.ReactivoBancoId).HasColumnName("reactivo_banco_id");
            entity.Property(e => e.Texto)
                .HasMaxLength(255)
                .HasColumnName("texto");

            entity.HasOne(d => d.ReactivoBanco).WithMany(p => p.Opciones)
                .HasForeignKey(d => d.ReactivoBancoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_opciones_banco_reactivos1");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PRIMARY");

            entity.ToTable("orders");

            entity.HasIndex(e => e.CustomerId, "CustomerID");

            entity.HasIndex(e => e.CustomerId, "CustomersOrders");

            entity.HasIndex(e => e.EmployeeId, "EmployeeID");

            entity.HasIndex(e => e.EmployeeId, "EmployeesOrders");

            entity.HasIndex(e => e.OrderDate, "OrderDate");

            entity.HasIndex(e => e.ShipPostalCode, "ShipPostalCode");

            entity.HasIndex(e => e.ShippedDate, "ShippedDate");

            entity.HasIndex(e => e.ShipVia, "ShippersOrders");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.CustomerId)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("CustomerID");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Freight)
                .HasPrecision(19, 4)
                .HasDefaultValueSql("'0.0000'");
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.RequiredDate).HasColumnType("datetime");
            entity.Property(e => e.ShipAddress).HasMaxLength(60);
            entity.Property(e => e.ShipCity).HasMaxLength(15);
            entity.Property(e => e.ShipCountry).HasMaxLength(15);
            entity.Property(e => e.ShipName).HasMaxLength(40);
            entity.Property(e => e.ShipPostalCode).HasMaxLength(10);
            entity.Property(e => e.ShipRegion).HasMaxLength(15);
            entity.Property(e => e.ShippedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_Orders_Customers");

            entity.HasOne(d => d.Employee).WithMany(p => p.Orders)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_Orders_Employees");

            entity.HasOne(d => d.ShipViaNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ShipVia)
                .HasConstraintName("FK_Orders_Shippers");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.ProductId }).HasName("PRIMARY");

            entity.ToTable("order details");

            entity.HasIndex(e => e.OrderId, "OrderID");

            entity.HasIndex(e => e.OrderId, "OrdersOrder_Details");

            entity.HasIndex(e => e.ProductId, "ProductID");

            entity.HasIndex(e => e.ProductId, "ProductsOrder_Details");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.Quantity).HasDefaultValueSql("'1'");
            entity.Property(e => e.UnitPrice).HasPrecision(19, 4);

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Details_Orders");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Details_Products");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PRIMARY");

            entity.ToTable("products");

            entity.HasIndex(e => e.CategoryId, "CategoriesProducts");

            entity.HasIndex(e => e.CategoryId, "CategoryID");

            entity.HasIndex(e => e.ProductName, "ProductName");

            entity.HasIndex(e => e.SupplierId, "SupplierID");

            entity.HasIndex(e => e.SupplierId, "SuppliersProducts");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.ProductName).HasMaxLength(40);
            entity.Property(e => e.QuantityPerUnit).HasMaxLength(20);
            entity.Property(e => e.ReorderLevel).HasDefaultValueSql("'0'");
            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");
            entity.Property(e => e.UnitPrice)
                .HasPrecision(19, 4)
                .HasDefaultValueSql("'0.0000'");
            entity.Property(e => e.UnitsInStock).HasDefaultValueSql("'0'");
            entity.Property(e => e.UnitsOnOrder).HasDefaultValueSql("'0'");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Products_Categories");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Products)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("FK_Products_Suppliers");
        });

        modelBuilder.Entity<PuntuacionPorCategorium>(entity =>
        {
            entity.HasKey(e => new { e.CategoriaId, e.AplicacionId }).HasName("PRIMARY");

            entity.ToTable("puntuacion_por_categoria");

            entity.HasIndex(e => e.AplicacionId, "fk_puntuacion_por_categoria_aplicaciones1");

            entity.HasIndex(e => e.CategoriaId, "fk_puntuacion_por_categoria_categorias1");

            entity.Property(e => e.CategoriaId).HasColumnName("categoria_id");
            entity.Property(e => e.AplicacionId).HasColumnName("aplicacion_id");
            entity.Property(e => e.Puntuacion).HasColumnName("puntuacion");
            entity.Property(e => e.RegTimeStamp)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("reg_time_stamp");
            entity.Property(e => e.TiempoEjecucionRestante)
                .HasComment("En segundos")
                .HasColumnName("tiempo_ejecucion_restante");

            entity.HasOne(d => d.Aplicacion).WithMany(p => p.PuntuacionPorCategoria)
                .HasForeignKey(d => d.AplicacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_puntuacion_por_categoria_aplicaciones1");
        });

        modelBuilder.Entity<Reactivo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("reactivos");

            entity.HasIndex(e => e.CategoriaId, "fk_preguntas_categorias1");

            entity.HasIndex(e => e.ReactivoBancoId, "fk_reactivos_banco_reactivos1");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CategoriaId).HasColumnName("categoria_id");
            entity.Property(e => e.Ponderacion)
                .HasDefaultValueSql("'1'")
                .HasComment("Peso en puntos, del reactivo, por default 1, para que todos los reactivos tengan el mismo peso.")
                .HasColumnName("ponderacion");
            entity.Property(e => e.ReactivoBancoId).HasColumnName("reactivo_banco_id");

            entity.HasOne(d => d.ReactivoBanco).WithMany(p => p.Reactivos)
                .HasForeignKey(d => d.ReactivoBancoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_reactivos_banco_reactivos1");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.RegionId).HasName("PRIMARY");

            entity.ToTable("region");

            entity.Property(e => e.RegionId).HasColumnName("RegionID");
            entity.Property(e => e.RegionDescription)
                .HasMaxLength(50)
                .IsFixedLength();
        });

        modelBuilder.Entity<Respuesta>(entity =>
        {
            entity.HasKey(e => new { e.AplicacionId, e.ReactivoId }).HasName("PRIMARY");

            entity.ToTable("respuestas");

            entity.HasIndex(e => e.AplicacionId, "fk_respuestas_aplicaciones1");

            entity.HasIndex(e => e.OpcionId, "fk_respuestas_opciones1");

            entity.HasIndex(e => e.ReactivoId, "fk_respuestas_reactivos1");

            entity.Property(e => e.AplicacionId).HasColumnName("aplicacion_id");
            entity.Property(e => e.ReactivoId).HasColumnName("reactivo_id");
            entity.Property(e => e.EsAcierto)
                .HasColumnType("enum('S','N')")
                .HasColumnName("es_acierto");
            entity.Property(e => e.OpcionId).HasColumnName("opcion_id");
            entity.Property(e => e.RespuestaTimeStamp)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("respuesta_time_stamp");

            entity.HasOne(d => d.Aplicacion).WithMany(p => p.Respuesta)
                .HasForeignKey(d => d.AplicacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_respuestas_aplicaciones1");

            entity.HasOne(d => d.Opcion).WithMany(p => p.Respuesta)
                .HasForeignKey(d => d.OpcionId)
                .HasConstraintName("fk_respuestas_opciones1");

            entity.HasOne(d => d.Reactivo).WithMany(p => p.Respuesta)
                .HasForeignKey(d => d.ReactivoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_respuestas_reactivos1");
        });

        modelBuilder.Entity<ResultadoDetallado>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("resultado_detallado");

            entity.Property(e => e.Algebra).HasColumnName("ALGEBRA");
            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(15)
                .HasColumnName("apellido_materno");
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(15)
                .HasColumnName("apellido_paterno");
            entity.Property(e => e.Aritmetica).HasColumnName("ARITMETICA");
            entity.Property(e => e.ComprensionLectora).HasColumnName("COMPRENSION_LECTORA");
            entity.Property(e => e.Folio).HasColumnName("folio");
            entity.Property(e => e.Nombre)
                .HasMaxLength(25)
                .HasColumnName("nombre");
            entity.Property(e => e.Perfil).HasColumnName("PERFIL");
            entity.Property(e => e.PreparatoriaProcedencia)
                .HasMaxLength(30)
                .HasColumnName("preparatoria_procedencia");
            entity.Property(e => e.PuntuacionTotal)
                .HasDefaultValueSql("'0'")
                .HasColumnName("puntuacion_total");
        });

        modelBuilder.Entity<Shipper>(entity =>
        {
            entity.HasKey(e => e.ShipperId).HasName("PRIMARY");

            entity.ToTable("shippers");

            entity.Property(e => e.ShipperId).HasColumnName("ShipperID");
            entity.Property(e => e.CompanyName).HasMaxLength(40);
            entity.Property(e => e.Phone).HasMaxLength(24);
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("PRIMARY");

            entity.ToTable("suppliers");

            entity.HasIndex(e => e.CompanyName, "CompanyName");

            entity.HasIndex(e => e.PostalCode, "PostalCode");

            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");
            entity.Property(e => e.Address).HasMaxLength(60);
            entity.Property(e => e.City).HasMaxLength(15);
            entity.Property(e => e.CompanyName).HasMaxLength(40);
            entity.Property(e => e.ContactName).HasMaxLength(30);
            entity.Property(e => e.ContactTitle).HasMaxLength(30);
            entity.Property(e => e.Country).HasMaxLength(15);
            entity.Property(e => e.Fax).HasMaxLength(24);
            entity.Property(e => e.Phone).HasMaxLength(24);
            entity.Property(e => e.PostalCode).HasMaxLength(10);
            entity.Property(e => e.Region).HasMaxLength(15);
        });

        modelBuilder.Entity<Territory>(entity =>
        {
            entity.HasKey(e => e.TerritoryId).HasName("PRIMARY");

            entity.ToTable("territories");

            entity.HasIndex(e => e.RegionId, "FK_Territories_Region");

            entity.Property(e => e.TerritoryId)
                .HasMaxLength(20)
                .HasColumnName("TerritoryID");
            entity.Property(e => e.RegionId).HasColumnName("RegionID");
            entity.Property(e => e.TerritoryDescription)
                .HasMaxLength(50)
                .IsFixedLength();

            entity.HasOne(d => d.Region).WithMany(p => p.Territories)
                .HasForeignKey(d => d.RegionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Territories_Region");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usuarios");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Password)
                .HasMaxLength(42)
                .HasColumnName("password");
            entity.Property(e => e.Rol)
                .HasMaxLength(15)
                .HasDefaultValueSql("'NINGUNO'")
                .HasColumnName("rol");
            entity.Property(e => e.Username)
                .HasMaxLength(45)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Ventasporcategorium>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ventasporcategoria");

            entity.Property(e => e.Categoryname)
                .HasMaxLength(15)
                .HasColumnName("categoryname");
            entity.Property(e => e.SumVentasporproducto)
                .HasPrecision(65, 4)
                .HasColumnName("sum(ventasporproducto)");
        });

        modelBuilder.Entity<Ventasproducto1997>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ventasproducto1997");

            entity.Property(e => e.Categoryname)
                .HasMaxLength(15)
                .HasColumnName("categoryname");
            entity.Property(e => e.Productid)
                .HasDefaultValueSql("'0'")
                .HasColumnName("productid");
            entity.Property(e => e.Productname)
                .HasMaxLength(40)
                .HasColumnName("productname");
            entity.Property(e => e.VentasPorProducto).HasPrecision(46, 4);
        });

        modelBuilder.Entity<Vistum>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vista");

            entity.Property(e => e.Categoryname)
                .HasMaxLength(15)
                .HasColumnName("categoryname");
            entity.Property(e => e.Companyname)
                .HasMaxLength(40)
                .HasColumnName("companyname");
            entity.Property(e => e.Productid).HasColumnName("productid");
            entity.Property(e => e.Productname)
                .HasMaxLength(40)
                .HasColumnName("productname");
            entity.Property(e => e.Unitprice)
                .HasPrecision(19, 4)
                .HasDefaultValueSql("'0.0000'")
                .HasColumnName("unitprice");
        });

        modelBuilder.Entity<Vwempleado>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwempleados");

            entity.Property(e => e.Empleado).HasMaxLength(57);
            entity.Property(e => e.Jefe).HasMaxLength(57);
            entity.Property(e => e.País).HasMaxLength(14);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
