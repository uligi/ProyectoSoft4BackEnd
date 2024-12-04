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
        Task<IEnumerable<MensajeUsuario>> ActualizarPortafolio(Portafolio portafolio); // Cambiado para aceptar un objeto
        Task<IEnumerable<MensajeUsuario>> EliminarPortafolio(int idPortafolio);
    }



    public class PortafolioRepository : IPortafolioRepository
    {
        private readonly ContextData _context;

        public PortafolioRepository(ContextData context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Portafolio>> ObtenerPortafolios()
        {
            return await _context.Portafolio.ToListAsync();
        }

        public async Task<IEnumerable<MensajeUsuario>> CrearPortafolio(Portafolio portafolio)
        {
            var nombreParam = new SqlParameter("@NombrePortafolio", portafolio.NombrePortafolio);
            var descripcionParam = new SqlParameter("@Descripcion", portafolio.Descripcion);
            var activoParam = new SqlParameter("@Activo", portafolio.Activo);

            return await _context.MensajeUsuario
                .FromSqlRaw("EXEC Crear_Portafolio @NombrePortafolio, @Descripcion, @Activo",
                            nombreParam, descripcionParam, activoParam)
                .ToListAsync();
        }

        public async Task<IEnumerable<MensajeUsuario>> ActualizarPortafolio(Portafolio portafolio)
        {
            var idParam = new SqlParameter("@idPortafolio", portafolio.idPortafolio);
            var nombreParam = new SqlParameter("@NombrePortafolio", portafolio.NombrePortafolio);
            var descripcionParam = new SqlParameter("@Descripcion", portafolio.Descripcion);

            return await _context.MensajeUsuario
                .FromSqlRaw("EXEC Actualizar_Portafolio @idPortafolio, @NombrePortafolio, @Descripcion",
                            idParam, nombreParam, descripcionParam)
                .ToListAsync();
        }

        public async Task<IEnumerable<MensajeUsuario>> EliminarPortafolio(int idPortafolio)
        {
            var idParam = new SqlParameter("@idPortafolio", idPortafolio);

            return await _context.MensajeUsuario
                .FromSqlRaw("EXEC Eliminar_Portafolio @idPortafolio", idParam)
                .ToListAsync();
        }

    }

}
