using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Teo_Em05.Models;

namespace Teo_Em05.Data;

public partial class RestauranteDbContext : DbContext
{
    public RestauranteDbContext()
    {
    }

    public RestauranteDbContext(DbContextOptions<RestauranteDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AlertasStockBajo> AlertasStockBajos { get; set; }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<DetallesPedido> DetallesPedidos { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<Ingrediente> Ingredientes { get; set; }

    public virtual DbSet<Mesa> Mesas { get; set; }

    public virtual DbSet<MesasDisponible> MesasDisponibles { get; set; }

    public virtual DbSet<MovimientosInventario> MovimientosInventarios { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Receta> Recetas { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=restaurante_db;User Id=postgres;Password=la1vida1es1oro");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AlertasStockBajo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("alertas_stock_bajo");

            entity.Property(e => e.Diferencia).HasColumnName("diferencia");
            entity.Property(e => e.IngredienteId).HasColumnName("ingrediente_id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.PrecioCompra)
                .HasPrecision(10, 2)
                .HasColumnName("precio_compra");
            entity.Property(e => e.StockActual)
                .HasPrecision(10, 3)
                .HasColumnName("stock_actual");
            entity.Property(e => e.StockMinimo)
                .HasPrecision(10, 3)
                .HasColumnName("stock_minimo");
            entity.Property(e => e.UnidadMedida)
                .HasMaxLength(20)
                .HasColumnName("unidad_medida");
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.CategoriaId).HasName("categorias_pkey");

            entity.ToTable("categorias");

            entity.Property(e => e.CategoriaId).HasColumnName("categoria_id");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.ClienteId).HasName("clientes_pkey");

            entity.ToTable("clientes");

            entity.Property(e => e.ClienteId).HasColumnName("cliente_id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<DetallesPedido>(entity =>
        {
            entity.HasKey(e => e.DetalleId).HasName("detalles_pedido_pkey");

            entity.ToTable("detalles_pedido");

            entity.HasIndex(e => e.Estado, "idx_detalles_pedido_estado");

            entity.Property(e => e.DetalleId).HasColumnName("detalle_id");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValueSql("'PENDIENTE'::character varying")
                .HasColumnName("estado");
            entity.Property(e => e.PedidoId).HasColumnName("pedido_id");
            entity.Property(e => e.PrecioUnitario)
                .HasPrecision(10, 2)
                .HasColumnName("precio_unitario");
            entity.Property(e => e.ProductoId).HasColumnName("producto_id");
            entity.Property(e => e.Subtotal)
                .HasPrecision(10, 2)
                .HasComputedColumnSql("((cantidad)::numeric * precio_unitario)", true)
                .HasColumnName("subtotal");

            entity.HasOne(d => d.Pedido).WithMany(p => p.DetallesPedidos)
                .HasForeignKey(d => d.PedidoId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("detalles_pedido_pedido_id_fkey");

            entity.HasOne(d => d.Producto).WithMany(p => p.DetallesPedidos)
                .HasForeignKey(d => d.ProductoId)
                .HasConstraintName("detalles_pedido_producto_id_fkey");
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.FacturaId).HasName("facturas_pkey");

            entity.ToTable("facturas");

            entity.Property(e => e.FacturaId).HasColumnName("factura_id");
            entity.Property(e => e.ClienteId).HasColumnName("cliente_id");
            entity.Property(e => e.EstadoPago)
                .HasMaxLength(20)
                .HasDefaultValueSql("'PENDIENTE'::character varying")
                .HasColumnName("estado_pago");
            entity.Property(e => e.FechaFactura)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_factura");
            entity.Property(e => e.Iva)
                .HasPrecision(10, 2)
                .HasColumnName("iva");
            entity.Property(e => e.MetodoPago)
                .HasMaxLength(20)
                .HasColumnName("metodo_pago");
            entity.Property(e => e.PedidoId).HasColumnName("pedido_id");
            entity.Property(e => e.Subtotal)
                .HasPrecision(10, 2)
                .HasColumnName("subtotal");
            entity.Property(e => e.Total)
                .HasPrecision(10, 2)
                .HasColumnName("total");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.ClienteId)
                .HasConstraintName("facturas_cliente_id_fkey");

            entity.HasOne(d => d.Pedido).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.PedidoId)
                .HasConstraintName("facturas_pedido_id_fkey");
        });

        modelBuilder.Entity<Ingrediente>(entity =>
        {
            entity.HasKey(e => e.IngredienteId).HasName("ingredientes_pkey");

            entity.ToTable("ingredientes");

            entity.HasIndex(e => e.StockActual, "idx_ingredientes_stock");

            entity.Property(e => e.IngredienteId).HasColumnName("ingrediente_id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.PrecioCompra)
                .HasPrecision(10, 2)
                .HasColumnName("precio_compra");
            entity.Property(e => e.StockActual)
                .HasPrecision(10, 3)
                .HasDefaultValueSql("0")
                .HasColumnName("stock_actual");
            entity.Property(e => e.StockMinimo)
                .HasPrecision(10, 3)
                .HasDefaultValueSql("0")
                .HasColumnName("stock_minimo");
            entity.Property(e => e.UnidadMedida)
                .HasMaxLength(20)
                .HasColumnName("unidad_medida");
        });

        modelBuilder.Entity<Mesa>(entity =>
        {
            entity.HasKey(e => e.MesaId).HasName("mesas_pkey");

            entity.ToTable("mesas");

            entity.HasIndex(e => e.Estado, "idx_mesas_estado");

            entity.HasIndex(e => e.NumeroMesa, "mesas_numero_mesa_key").IsUnique();

            entity.Property(e => e.MesaId).HasColumnName("mesa_id");
            entity.Property(e => e.Capacidad).HasColumnName("capacidad");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValueSql("'DISPONIBLE'::character varying")
                .HasColumnName("estado");
            entity.Property(e => e.NumeroMesa).HasColumnName("numero_mesa");
            entity.Property(e => e.Ubicacion)
                .HasMaxLength(50)
                .HasColumnName("ubicacion");
        });

        modelBuilder.Entity<MesasDisponible>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("mesas_disponibles");

            entity.Property(e => e.Capacidad).HasColumnName("capacidad");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasColumnName("estado");
            entity.Property(e => e.MesaId).HasColumnName("mesa_id");
            entity.Property(e => e.NumeroMesa).HasColumnName("numero_mesa");
            entity.Property(e => e.Ubicacion)
                .HasMaxLength(50)
                .HasColumnName("ubicacion");
        });

        modelBuilder.Entity<MovimientosInventario>(entity =>
        {
            entity.HasKey(e => e.MovimientoId).HasName("movimientos_inventario_pkey");

            entity.ToTable("movimientos_inventario");

            entity.Property(e => e.MovimientoId).HasColumnName("movimiento_id");
            entity.Property(e => e.Cantidad)
                .HasPrecision(10, 3)
                .HasColumnName("cantidad");
            entity.Property(e => e.FechaMovimiento)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_movimiento");
            entity.Property(e => e.IngredienteId).HasColumnName("ingrediente_id");
            entity.Property(e => e.Motivo)
                .HasMaxLength(100)
                .HasColumnName("motivo");
            entity.Property(e => e.TipoMovimiento)
                .HasMaxLength(20)
                .HasColumnName("tipo_movimiento");
            entity.Property(e => e.Usuario)
                .HasMaxLength(50)
                .HasColumnName("usuario");

            entity.HasOne(d => d.Ingrediente).WithMany(p => p.MovimientosInventarios)
                .HasForeignKey(d => d.IngredienteId)
                .HasConstraintName("movimientos_inventario_ingrediente_id_fkey");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.PedidoId).HasName("pedidos_pkey");

            entity.ToTable("pedidos");

            entity.HasIndex(e => e.Estado, "idx_pedidos_estado");

            entity.HasIndex(e => e.FechaPedido, "idx_pedidos_fecha");

            entity.Property(e => e.PedidoId).HasColumnName("pedido_id");
            entity.Property(e => e.ClienteId).HasColumnName("cliente_id");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValueSql("'PENDIENTE'::character varying")
                .HasColumnName("estado");
            entity.Property(e => e.FechaPedido)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_pedido");
            entity.Property(e => e.MesaId).HasColumnName("mesa_id");
            entity.Property(e => e.Notas).HasColumnName("notas");
            entity.Property(e => e.Total)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("total");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.ClienteId)
                .HasConstraintName("pedidos_cliente_id_fkey");

            entity.HasOne(d => d.Mesa).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.MesaId)
                .HasConstraintName("pedidos_mesa_id_fkey");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.ProductoId).HasName("productos_pkey");

            entity.ToTable("productos");

            entity.Property(e => e.ProductoId).HasColumnName("producto_id");
            entity.Property(e => e.CategoriaId).HasColumnName("categoria_id");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.Disponible)
                .HasDefaultValue(true)
                .HasColumnName("disponible");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio)
                .HasPrecision(10, 2)
                .HasColumnName("precio");
            entity.Property(e => e.TiempoPreparacion).HasColumnName("tiempo_preparacion");

            entity.HasOne(d => d.Categoria).WithMany(p => p.Productos)
                .HasForeignKey(d => d.CategoriaId)
                .HasConstraintName("productos_categoria_id_fkey");
        });

        modelBuilder.Entity<Receta>(entity =>
        {
            entity.HasKey(e => e.RecetaId).HasName("recetas_pkey");

            entity.ToTable("recetas");

            entity.Property(e => e.RecetaId).HasColumnName("receta_id");
            entity.Property(e => e.CantidadRequerida)
                .HasPrecision(10, 3)
                .HasColumnName("cantidad_requerida");
            entity.Property(e => e.IngredienteId).HasColumnName("ingrediente_id");
            entity.Property(e => e.ProductoId).HasColumnName("producto_id");

            entity.HasOne(d => d.Ingrediente).WithMany(p => p.Receta)
                .HasForeignKey(d => d.IngredienteId)
                .HasConstraintName("recetas_ingrediente_id_fkey");

            entity.HasOne(d => d.Producto).WithMany(p => p.Receta)
                .HasForeignKey(d => d.ProductoId)
                .HasConstraintName("recetas_producto_id_fkey");
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.ReservaId).HasName("reservas_pkey");

            entity.ToTable("reservas");

            entity.HasIndex(e => e.FechaReserva, "idx_reservas_fecha");

            entity.Property(e => e.ReservaId).HasColumnName("reserva_id");
            entity.Property(e => e.ClienteId).HasColumnName("cliente_id");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValueSql("'CONFIRMADA'::character varying")
                .HasColumnName("estado");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaReserva)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_reserva");
            entity.Property(e => e.MesaId).HasColumnName("mesa_id");
            entity.Property(e => e.Notas).HasColumnName("notas");
            entity.Property(e => e.NumeroPersonas).HasColumnName("numero_personas");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.ClienteId)
                .HasConstraintName("reservas_cliente_id_fkey");

            entity.HasOne(d => d.Mesa).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.MesaId)
                .HasConstraintName("reservas_mesa_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
