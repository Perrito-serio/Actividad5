using System;
using System.Collections.Generic;

namespace Teo_Em05.Models;

public partial class Reserva
{
    public int ReservaId { get; set; }

    public int? ClienteId { get; set; }

    public int? MesaId { get; set; }

    public DateTime FechaReserva { get; set; }

    public int NumeroPersonas { get; set; }

    public string? Estado { get; set; }

    public string? Notas { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual Cliente? Cliente { get; set; }

    public virtual Mesa? Mesa { get; set; }
}
