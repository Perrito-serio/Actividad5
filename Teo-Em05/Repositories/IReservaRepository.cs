// Archivo: Repositories/IReservaRepository.cs
using Teo_Em05.Models;

namespace Teo_Em05.Repositories;

public interface IReservaRepository
{
    // Contrato para obtener todas las reservas
    Task<IEnumerable<Reserva>> GetAllAsync();
    
    // Contrato para obtener una reserva por su ID
    Task<Reserva?> GetByIdAsync(int id);
    
    // Contrato para crear una nueva reserva
    Task AddAsync(Reserva reserva);
    
    // (Opcional) Podríamos agregar más contratos después
    // Task UpdateAsync(Reserva reserva);
    // Task DeleteAsync(int id);
}