using Dapper;
using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios.Abstracciones;

namespace ApiKnowledgeMap.Repositorios
{
    public class EnfoqueRcRepository : IEnfoqueRcRepository
    {
        private readonly IProveedorConexion _proveedorConexion;

        public EnfoqueRcRepository(IProveedorConexion proveedorConexion)
        {
            _proveedorConexion = proveedorConexion;
        }

        public async Task<IEnumerable<EnfoqueRc>> ObtenerTodosAsync()
        {
            using var conn = _proveedorConexion.ObtenerConexion();

            return await conn.QueryAsync<EnfoqueRc>(
                @"SELECT 
                    enfoque AS Enfoque,
                    registro_calificado AS RegistroCalificado
                  FROM enfoque_rc");
        }

        public async Task<EnfoqueRc?> ObtenerPorIdAsync(int enfoque, int registroCalificado)
        {
            using var conn = _proveedorConexion.ObtenerConexion();

            return await conn.QueryFirstOrDefaultAsync<EnfoqueRc>(
                @"SELECT 
                    enfoque AS Enfoque,
                    registro_calificado AS RegistroCalificado
                  FROM enfoque_rc
                  WHERE enfoque = @enfoque
                  AND registro_calificado = @registroCalificado",
                new { enfoque, registroCalificado });
        }

        public async Task<bool> InsertarAsync(EnfoqueRc item)
        {
            using var conn = _proveedorConexion.ObtenerConexion();

            var filas = await conn.ExecuteAsync(
                @"INSERT INTO enfoque_rc
                  (enfoque, registro_calificado)
                  VALUES
                  (@Enfoque, @RegistroCalificado)",
                item);

            return filas > 0;
        }

        public async Task<bool> ActualizarAsync(EnfoqueRc item)
        {
            using var conn = _proveedorConexion.ObtenerConexion();

            var filas = await conn.ExecuteAsync(
                @"UPDATE enfoque_rc
                  SET enfoque = @Enfoque,
                      registro_calificado = @RegistroCalificado
                  WHERE enfoque = @Enfoque
                  AND registro_calificado = @RegistroCalificado",
                item);

            return filas > 0;
        }

        public async Task<bool> EliminarAsync(int enfoque, int registroCalificado)
        {
            using var conn = _proveedorConexion.ObtenerConexion();

            var filas = await conn.ExecuteAsync(
                @"DELETE FROM enfoque_rc
                  WHERE enfoque = @enfoque
                  AND registro_calificado = @registroCalificado",
                new { enfoque, registroCalificado });

            return filas > 0;
        }
    }
}