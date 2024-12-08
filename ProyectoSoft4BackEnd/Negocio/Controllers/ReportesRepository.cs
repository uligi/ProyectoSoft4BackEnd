using Microsoft.Data.SqlClient;
using Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

using Microsoft.EntityFrameworkCore;
using Negocio.Data;


namespace Negocio.Controllers
{
    public class ReportesRepository
    {
        private readonly string _connectionString;

        public ReportesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<ProyectoReporte>> GetProyectos(DateTime fechaInicio, string estado)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@FechaInicio", fechaInicio);
                parameters.Add("@Estado", estado);

                var result = await connection.QueryAsync<ProyectoReporte>("sp_GetProyectos", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<IEnumerable<TareaReporte>> GetTareas(int idUsuario, string prioridad)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdUsuario", idUsuario);
                parameters.Add("@Prioridad", prioridad);

                var result = await connection.QueryAsync<TareaReporte>("sp_GetTareas", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
    }

}
