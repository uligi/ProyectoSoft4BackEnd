using Negocio.Data;
using Negocio.Modelos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Negocio.Controllers
{
    public interface IProyectosRepository
    {
        Task<IEnumerable<Proyectos>> ObtenerProyectos();
        Task<IEnumerable<MensajeUsuario>> CrearProyecto(Proyectos proyecto);
    }

    public class ProyectosRepository : IProyectosRepository
    {
        private readonly ContextData _context;
        public ProyectosRepository(ContextData context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MensajeUsuario>> CrearProyecto(Proyectos proyecto)
        {
            var nombreProyecto = new SqlParameter("@NombreProyecto", proyecto.NombreProyecto);
            var descripcion = new SqlParameter("@Descripcion", proyecto.Descripcion);
            var prioridad = new SqlParameter("@Prioridad", proyecto.Prioridad);
            var fechaInicio = new SqlParameter("@FechaInicio", proyecto.FechaInicio);
            var fechaFinal = new SqlParameter("@FechaFinal", proyecto.FechaFinal);
            var idPortafolio = new SqlParameter("@Portafolio_idPortafolio", proyecto.Portafolio_idPortafolio);

            return await _context.MensajeUsuario
                .FromSqlRaw("EXEC SP_NuevoProyecto @NombreProyecto, @Descripcion, @Prioridad, @FechaInicio, @FechaFinal, @Portafolio_idPortafolio",
                nombreProyecto, descripcion, prioridad, fechaInicio, fechaFinal, idPortafolio)
                .ToListAsync();
        }

        public async Task<IEnumerable<Proyectos>> ObtenerProyectos()
        {
            return await _context.Proyectos.ToListAsync();
        }
    }
}
