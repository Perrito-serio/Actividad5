using System;
using System.Collections.Generic;

namespace Teo_Em05.Models;

public partial class Pedido
{
    public int PedidoId { get; set; }

    public int? MesaId { get; set; }

    public int? ClienteId { get; set; }

    public DateTime? FechaPedido { get; set; }

    public string? Estado { get; set; }

    public decimal? Total { get; set; }

    public string? Notas { get; set; }

    public virtual Cliente? Cliente { get; set; }

    public virtual ICollection<DetallesPedido> DetallesPedidos { get; set; } = new List<DetallesPedido>();

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual Mesa? Mesa { get; set; }
}
