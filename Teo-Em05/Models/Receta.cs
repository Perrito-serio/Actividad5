using System;
using System.Collections.Generic;

namespace Teo_Em05.Models;

public partial class Receta
{
    public int RecetaId { get; set; }

    public int? ProductoId { get; set; }

    public int? IngredienteId { get; set; }

    public decimal CantidadRequerida { get; set; }

    public virtual Ingrediente? Ingrediente { get; set; }

    public virtual Producto? Producto { get; set; }
}
