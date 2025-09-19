using System;
using System.Collections.Generic;

namespace Teo_Em05.Models;

public partial class Producto
{
    public int ProductoId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal Precio { get; set; }

    public int? CategoriaId { get; set; }

    public bool? Disponible { get; set; }

    public int? TiempoPreparacion { get; set; }

    public virtual Categoria? Categoria { get; set; }

    public virtual ICollection<DetallesPedido> DetallesPedidos { get; set; } = new List<DetallesPedido>();

    public virtual ICollection<Receta> Receta { get; set; } = new List<Receta>();
}
