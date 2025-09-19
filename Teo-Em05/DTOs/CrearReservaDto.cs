namespace Teo_Em05.DTOs;

public class CrearReservaDto
{
    public int? ClienteId { get; set; }
    public int? MesaId { get; set; }
    public DateTime FechaReserva { get; set; }
    public int NumeroPersonas { get; set; }
    public string? Notas { get; set; }
}