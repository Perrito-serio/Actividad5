// Archivo: Repositories/ReservaRepository.cs
using Microsoft.EntityFrameworkCore;
using Teo_Em05.Data;
using Teo_Em05.Models;

namespace Teo_Em05.Repositories;

public class ReservaRepository : IReservaRepository
{
    private readonly RestauranteDbContext _context;

    // Inyectamos el DbContext para poder usarlo
    public ReservaRepository(RestauranteDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Reserva>> GetAllAsync()
    {
        return await _context.Reservas
            .Include(r => r.Cliente) // Incluimos datos del cliente
            .Include(r => r.Mesa)    // Incluimos datos de la mesa
            .ToListAsync();
    }

    public async Task<Reserva?> GetByIdAsync(int id)
    {
        return await _context.Reservas
            .Include(r => r.Cliente)
            .Include(r => r.Mesa)
            .FirstOrDefaultAsync(r => r.ReservaId == id);
    }

    public async Task AddAsync(Reserva reserva)
    {
        await _context.Reservas.AddAsync(reserva);
        await _context.SaveChangesAsync(); // Guardamos los cambios en la DB
    }
}