using System;
using System.Collections.Generic;

namespace Teo_Em05.Models;

public partial class DetallesPedido
{
    public int DetalleId { get; set; }

    public int? PedidoId { get; set; }

    public int? ProductoId { get; set; }

    public int Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public decimal? Subtotal { get; set; }

    public string? Estado { get; set; }

    public virtual Pedido? Pedido { get; set; }

    public virtual Producto? Producto { get; set; }
}
