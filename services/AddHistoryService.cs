using Microsoft.EntityFrameworkCore;
using webapi.Models;

namespace webapi.Services
{
    public class AddHistoryService : IAddHistoryService
    {
        private readonly TareasContext _context;

        public AddHistoryService(TareasContext dbContext)
        {
            _context = dbContext;
        }

        // Obtener todas las historias
        public async Task<IEnumerable<History>> Get()
        {
            return await _context.Histories.ToListAsync();
        }

        // Generar un código de acceso aleatorio de 6 caracteres
        private string GenerateAccessCode(int length = 6)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        // Guardar las historias de una sesión de estimación
        public async Task<(string accessCode, Guid estimationId)> Save(AddHistory addHistory)
        {
            if (addHistory.Items == null || !addHistory.Items.Any())
                throw new ArgumentException("No se encontraron historias para guardar.");

            // Generar código y ID para la sesión
            var accessCode = GenerateAccessCode();
            var estimationId = Guid.NewGuid();

            var histories = addHistory.Items.Select(item => new History
            {
                Id = Guid.NewGuid(),
                UserId = addHistory.UserId,
                Title = item.Title,
                Description = item.Description,
                Date = item.Date,
                SprintName = addHistory.SprintName,
                EstimationId = estimationId,
                AccessCode = accessCode
            });

            await _context.Histories.AddRangeAsync(histories);
            await _context.SaveChangesAsync();

            return (accessCode, estimationId);
        }
    }

    // Interfaz actualizada
    public interface IAddHistoryService
    {
        Task<IEnumerable<History>> Get();
        Task<(string accessCode, Guid estimationId)> Save(AddHistory addHistory);
    }
}
