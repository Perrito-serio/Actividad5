using System;
using System.Collections.Generic;

namespace Teo_Em05.Models;

public partial class MovimientosInventario
{
    public int MovimientoId { get; set; }

    public int? IngredienteId { get; set; }

    public string? TipoMovimiento { get; set; }

    public decimal Cantidad { get; set; }

    public string? Motivo { get; set; }

    public DateTime? FechaMovimiento { get; set; }

    public string? Usuario { get; set; }

    public virtual Ingrediente? Ingrediente { get; set; }
}
