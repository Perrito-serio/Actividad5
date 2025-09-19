// Archivo: Controllers/ReservasController.cs
using Microsoft.AspNetCore.Mvc;
using Teo_Em05.DTOs;
using Teo_Em05.Models;
using Teo_Em05.Repositories;

namespace Teo_Em05.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservasController : ControllerBase
{
    private readonly IReservaRepository _reservaRepository;

    // Usando Inyección de Dependencias, pedimos el "contrato"
    public ReservasController(IReservaRepository reservaRepository)
    {
        _reservaRepository = reservaRepository;
    }

    // GET: api/reservas
    [HttpGet]
    public async Task<IActionResult> GetReservas()
    {
        var reservas = await _reservaRepository.GetAllAsync();
        return Ok(reservas);
    }

    // POST: api/reservas
    [HttpPost]
    public async Task<IActionResult> CreateReserva(CrearReservaDto reservaDto)
    {
        // "Mapeamos" los datos del DTO a nuestro modelo de base de datos
        var nuevaReserva = new Reserva
        {
            ClienteId = reservaDto.ClienteId,
            MesaId = reservaDto.MesaId,
            FechaReserva = reservaDto.FechaReserva,
            NumeroPersonas = reservaDto.NumeroPersonas,
            Notas = reservaDto.Notas,
            // El estado y fecha de creación usarán los valores por defecto de la DB
        };

        await _reservaRepository.AddAsync(nuevaReserva);

        // Devolvemos una respuesta HTTP 201 (Created)
        return CreatedAtAction(nameof(GetReservas), new { id = nuevaReserva.ReservaId }, nuevaReserva);
    }
}