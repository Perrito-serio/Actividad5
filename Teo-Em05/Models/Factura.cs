using System;
using System.Collections.Generic;

namespace Teo_Em05.Models;

public partial class Factura
{
    public int FacturaId { get; set; }

    public int? PedidoId { get; set; }

    public int? ClienteId { get; set; }

    public DateTime? FechaFactura { get; set; }

    public decimal Subtotal { get; set; }

    public decimal Iva { get; set; }

    public decimal Total { get; set; }

    public string? EstadoPago { get; set; }

    public string? MetodoPago { get; set; }

    public virtual Cliente? Cliente { get; set; }

    public virtual Pedido? Pedido { get; set; }
}
