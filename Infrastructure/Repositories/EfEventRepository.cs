using Microsoft.EntityFrameworkCore;
using Project.Domain.Entities.Event;
using Project.Domain.Interfaces;
using Project.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.Repositories
{
    public class EfEventRepository : IEventAsyncRepository
    {
        private readonly AppDbContext _db;

        public EfEventRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Event> AddAsync(Event eventEntity, CancellationToken cancellationToken = default)
        {
            await _db.Events.AddAsync(eventEntity, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
            return eventEntity;
        }

        public async Task<Event?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _db.Events
                .AsNoTracking()
                .Include(e => e.EventMetadata)
                .Include(e => e.Participants)
                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public async Task<List<Event>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _db.Events
                .AsNoTracking()
                .Include(e => e.EventMetadata)
                .Include(e => e.Participants)
                .ToListAsync(cancellationToken);
        }
    }
}
