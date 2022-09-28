using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contextos;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Persistence
{
    public class EventoPersist : IEventoPersist
    {
        private readonly ProEventosContext _context; 
        // an instance of DbContext represents a session with the database which can be used to query and save instances of the entities to the database.
        // it's a combinantion of the Unit Of Work and Repository patterns.

        public EventoPersist(ProEventosContext context) //construtor da classe.
        {
            _context = context;
            //_context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

        }
        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(evento => evento.Lotes)
                .Include(evento => evento.RedesSociais);
            
            if (includePalestrantes) 
            {
                query.Include(evento => evento.PalestrantesEventos)
                .ThenInclude(palestranteEvento => palestranteEvento.Palestrante);
            }

            query = query.AsNoTracking().OrderBy(evento => evento.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(evento => evento.Lotes)
                .Include(evento => evento.RedesSociais);
            
            if (includePalestrantes) 
            {
                query.Include(evento => evento.PalestrantesEventos)
                .ThenInclude(palestranteEvento => palestranteEvento.Palestrante);
            }

            query = query.AsNoTracking().OrderBy(evento => evento.Id)
                .Where(evento => evento.Tema.ToLower().Contains(tema.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos.AsNoTracking()
                .Include(evento => evento.Lotes)
                .Include(evento => evento.RedesSociais);
            
            if (includePalestrantes) 
            {
                query.Include(evento => evento.PalestrantesEventos)
                .ThenInclude(palestranteEvento => palestranteEvento.Palestrante);
            }

            query = query.OrderBy(evento => evento.Id)
                .Where(evento => evento.Id == eventoId);

            return await query.FirstOrDefaultAsync();
        }
    }
}