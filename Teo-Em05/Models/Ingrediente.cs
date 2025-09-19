using System;
using System.Collections.Generic;

namespace Teo_Em05.Models;

public partial class Ingrediente
{
    public int IngredienteId { get; set; }

    public string Nombre { get; set; } = null!;

    public string UnidadMedida { get; set; } = null!;

    public decimal? StockActual { get; set; }

    public decimal? StockMinimo { get; set; }

    public decimal? PrecioCompra { get; set; }

    public virtual ICollection<MovimientosInventario> MovimientosInventarios { get; set; } = new List<MovimientosInventario>();

    public virtual ICollection<Receta> Receta { get; set; } = new List<Receta>();
}
