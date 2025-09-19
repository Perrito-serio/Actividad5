using System;
using System.Collections.Generic;

namespace Teo_Em05.Models;

public partial class MesasDisponible
{
    public int? MesaId { get; set; }

    public int? NumeroMesa { get; set; }

    public int? Capacidad { get; set; }

    public string? Estado { get; set; }

    public string? Ubicacion { get; set; }
}
