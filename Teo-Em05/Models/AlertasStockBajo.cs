using System;
using System.Collections.Generic;

namespace Teo_Em05.Models;

public partial class AlertasStockBajo
{
    public int? IngredienteId { get; set; }

    public string? Nombre { get; set; }

    public string? UnidadMedida { get; set; }

    public decimal? StockActual { get; set; }

    public decimal? StockMinimo { get; set; }

    public decimal? PrecioCompra { get; set; }

    public decimal? Diferencia { get; set; }
}
