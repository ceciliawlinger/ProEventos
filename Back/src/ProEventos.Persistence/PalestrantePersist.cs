using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contextos;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Persistence
{
    public class PalestrantePersist : IPalestrantePersist
    {
        private readonly ProEventosContext _context;
        public PalestrantePersist(ProEventosContext context)
        {
            _context = context;

        }

        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos = false)
        {
             IQueryable<Palestrante> query = _context.Palestrantes.AsNoTracking()
                .Include(palestrante => palestrante.RedesSociais);
            
            if (includeEventos) 
            {
                query.Include(palestrante => palestrante.PalestrantesEventos)
                .ThenInclude(palestranteEvento => palestranteEvento.Evento);
            }

            query = query.OrderBy(palestrante => palestrante.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes.AsNoTracking()
                .Include(palestrante => palestrante.RedesSociais);
            
            if (includeEventos) 
            {
                query.Include(palestrante => palestrante.PalestrantesEventos)
                .ThenInclude(palestranteEvento => palestranteEvento.Evento);
            }

            query = query.OrderBy(palestrante => palestrante.Id)
                .Where(palestrante => palestrante.Nome.ToLower().Contains(nome.ToLower()));


            return await query.ToArrayAsync();
        }


        public async Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes.AsNoTracking()
                .Include(palestrante => palestrante.RedesSociais);
            
            if (includeEventos) 
            {
                query.Include(palestrante => palestrante.PalestrantesEventos)
                .ThenInclude(palestranteEvento => palestranteEvento.Evento);
            }

            query = query.OrderBy(palestrante => palestrante.Id)
                .Where(palestrante => palestrante.Id == palestranteId);

            return await query.FirstOrDefaultAsync();
        }
    }
}