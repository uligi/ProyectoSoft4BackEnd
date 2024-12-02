using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Negocio.Data;
using Negocio.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Negocio.Controllers
{
    public interface IPortafolioRepository
    {
        Task<IEnumerable<Portafolio>> ObtenerPortafolios();
        Task<IEnumerable<MensajeUsuario>> CrearPortafolio(Portafolio portafolio);
        Task<IEnumerable<MensajeUsuario>> ActualizarPortafolio(int idPortafolio, string nombrePortafolio, bool activo, string descripcion);
    }

    public class PortafolioRepository : IPortafolioRepository
    {
        private readonly ContextData _context;

        public PortafolioRepository(ContextData context)
        {
            _context = context;
        }

        // Método para obtener todos los portafolios
        public async Task<IEnumerable<Portafolio>> ObtenerPortafolios()
        {
            return await _context.Portafolio.ToListAsync();
        }

        // Método para crear un nuevo portafolio
        public async Task<IEnumerable<MensajeUsuario>> CrearPortafolio(Portafolio portafolio)
        {
            if (string.IsNullOrEmpty(portafolio.NombrePortafolio))
            {
                return new List<MensajeUsuario>
                {
                    new MensajeUsuario { Codigo = -3, Mensaje = "El nombre del portafolio no puede estar vacío o nulo" }
                };
            }
            else
            {
                var nombrePortafolioParam = new SqlParameter("@NombrePortafolio", portafolio.NombrePortafolio);
                var activoParam = new SqlParameter("@Activo", portafolio.Activo);
                var descripcionParam = new SqlParameter("@Descripcion", portafolio.Descripcion);
                var fechaCreacionParam = new SqlParameter("@FechaCreacion", portafolio.FechaCreacion);

                return await _context.MensajeUsuario
                    .FromSqlRaw("EXEC Crear_Portafolio @NombrePortafolio, @Activo, @Descripcion, @FechaCreacion",
                                nombrePortafolioParam, activoParam, descripcionParam, fechaCreacionParam)
                    .ToListAsync();
            }
        }

        // Método para actualizar un portafolio existente
        public async Task<IEnumerable<MensajeUsuario>> ActualizarPortafolio(int idPortafolio, string nombrePortafolio, bool activo, string descripcion)
        {
            if (string.IsNullOrEmpty(nombrePortafolio))
            {
                return new List<MensajeUsuario>
                {
                    new MensajeUsuario { Codigo = -3, Mensaje = "El nombre del portafolio no puede estar vacío o nulo" }
                };
            }
            else
            {
                var idPortafolioParam = new SqlParameter("@idPortafolio", idPortafolio);
                var nombrePortafolioParam = new SqlParameter("@NombrePortafolio", nombrePortafolio);
                var activoParam = new SqlParameter("@Activo", activo);
                var descripcionParam = new SqlParameter("@Descripcion", descripcion);

                return await _context.MensajeUsuario
                    .FromSqlRaw("EXEC Modificar_Portafolio @idPortafolio, @NombrePortafolio, @Activo, @Descripcion",
                                idPortafolioParam, nombrePortafolioParam, activoParam, descripcionParam)
                    .ToListAsync();
            }
        }
    }
}
