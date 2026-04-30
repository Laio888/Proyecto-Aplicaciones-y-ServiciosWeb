using Dapper;
using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios.Abstracciones;

namespace ApiKnowledgeMap.Repositorios
{
    public class PracticaEstrategiaRepository : IPracticaEstrategiaRepository
    {
        private readonly IProveedorConexion _proveedorConexion;

        public PracticaEstrategiaRepository(IProveedorConexion proveedorConexion)
        {
            _proveedorConexion = proveedorConexion;
        }

        public async Task<IEnumerable<PracticaEstrategia>> ObtenerTodosAsync()
        {
            using var conn = _proveedorConexion.ObtenerConexion();
            return await conn.QueryAsync<PracticaEstrategia>(
                "SELECT id AS Id, tipo AS Tipo, nombre AS Nombre, descripcion AS Descripcion FROM practica_estrategia");
        }

        public async Task<PracticaEstrategia?> ObtenerPorIdAsync(int id)
        {
            using var conn = _proveedorConexion.ObtenerConexion();
            return await conn.QueryFirstOrDefaultAsync<PracticaEstrategia>(
                "SELECT id AS Id, tipo AS Tipo, nombre AS Nombre, descripcion AS Descripcion FROM practica_estrategia WHERE id = @Id",
                new { Id = id });
        }

        public async Task<int> InsertarAsync(PracticaEstrategia practica)
        {
            using var conn = _proveedorConexion.ObtenerConexion();
            return await conn.ExecuteScalarAsync<int>(
                @"INSERT INTO practica_estrategia (id, tipo, nombre, descripcion)
                  VALUES (@Id, @Tipo, @Nombre, @Descripcion);
                  SELECT SCOPE_IDENTITY();", practica);
        }

        public async Task<bool> ActualizarAsync(PracticaEstrategia practica)
        {
            using var conn = _proveedorConexion.ObtenerConexion();
            var filas = await conn.ExecuteAsync(
                @"UPDATE practica_estrategia 
                  SET tipo = @Tipo, nombre = @Nombre, descripcion = @Descripcion
                  WHERE id = @Id", practica);
            return filas > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            using var conn = _proveedorConexion.ObtenerConexion();
            var filas = await conn.ExecuteAsync(
                "DELETE FROM practica_estrategia WHERE id = @Id", new { Id = id });
            return filas > 0;
        }
    }
}