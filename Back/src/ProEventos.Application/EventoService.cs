using System;
using System.Threading.Tasks;
using ProEventos.Application.Contratos;
using ProEventos.Domain;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Application
{
    public class EventoService : IEventoService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IEventoPersist _eventoPersist;
        public EventoService(IGeralPersist geralPersist, IEventoPersist eventoPersist)
        {
            _eventoPersist = eventoPersist;
            _geralPersist = geralPersist;

        }

        public async Task<Evento> AddEventos(Evento model)
        {
            try
            {
                _geralPersist.Add<Evento>(model);
                // opcional. retorna para o usuário, após inserção no banco, o objeto adicionado e o id gerado.
                if (await _geralPersist.SaveChangesAsync()) return await _eventoPersist.GetEventoByIdAsync(model.Id, false);
                return null;
            }
            catch (Exception ex)
            {
                
                throw new Exception($"Error while adding an event. {ex.Message}");
            }
        }

        public async Task<Evento> UpdateEvento(int eventoId, Evento model)
        {
            try
            {
                var evento = await _eventoPersist.GetEventoByIdAsync(eventoId, false); //busca o evento a ser atualizado
                if (evento == null) return null;

                model.Id = evento.Id; //substitui o id recebido pelo id do objeto encontrado

                _geralPersist.Update(model);
                if (await _geralPersist.SaveChangesAsync()) return await _eventoPersist.GetEventoByIdAsync(model.Id, false);
                return null;

            }
            catch (Exception ex)
            {
                
                throw new Exception($"Error while updating an event. {ex.Message}");
            }
        }

        public async Task<bool> DeleteEvento(int eventoId)
        {
            try
            {
                var evento = await _eventoPersist.GetEventoByIdAsync(eventoId, false); //busca o evento a ser deletado
                if (evento == null) throw new Exception("Evento não encontrado");

                _geralPersist.Delete<Evento>(evento);
                return await _geralPersist.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                
                throw new Exception($"Error while updating an event. {ex.Message}");
            }
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersist.GetAllEventosAsync(includePalestrantes);
                if (eventos == null) return null;

                return eventos;
            }
            catch (Exception ex)
            {
                
                throw new Exception($"Error while getting all events. {ex.Message}");
            }
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersist.GetAllEventosByTemaAsync(tema, includePalestrantes);
                if (eventos == null) return null;
                return eventos;
            }
            catch (Exception ex)
            {
                
                throw new Exception($"Error while getting all events. {ex.Message}");
            } 
        }

        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersist.GetEventoByIdAsync(eventoId, includePalestrantes);
                if (eventos == null) return null;
                return eventos;
            }
            catch (Exception ex)
            {
                
                throw new Exception($"Error while getting event by id. {ex.Message}");
            } 
            
        }


    }
}