using Dapper;
using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios.Abstracciones;

namespace ApiKnowledgeMap.Repositorios
{
    public class AspectoNormativoRepository : IAspectoNormativoRepository
    {
        private readonly IProveedorConexion _proveedorConexion;

        public AspectoNormativoRepository(IProveedorConexion proveedorConexion)
        {
            _proveedorConexion = proveedorConexion;
        }

        public async Task<IEnumerable<AspectoNormativo>> ObtenerTodosAsync()
        {
            using var conn = _proveedorConexion.ObtenerConexion();
            return await conn.QueryAsync<AspectoNormativo>(
                "SELECT id AS Id, tipo AS Tipo, descripcion AS Descripcion, fuente AS Fuente FROM aspecto_normativo");
        }

        public async Task<AspectoNormativo?> ObtenerPorIdAsync(int id)
        {
            using var conn = _proveedorConexion.ObtenerConexion();
            return await conn.QueryFirstOrDefaultAsync<AspectoNormativo>(
                "SELECT id AS Id, tipo AS Tipo, descripcion AS Descripcion, fuente AS Fuente FROM aspecto_normativo WHERE id = @Id",
                new { Id = id });
        }

        public async Task<int> InsertarAsync(AspectoNormativo AspectoNormativo)
        {
            using var conn = _proveedorConexion.ObtenerConexion();
            return await conn.ExecuteScalarAsync<int>(
                @"INSERT INTO aspecto_normativo (id, tipo, descripcion, fuente )
                  VALUES (@Id, @Tipo, @Descripcion, @Fuente );
                  SELECT SCOPE_IDENTITY();", AspectoNormativo);
        }

        public async Task<bool> ActualizarAsync(AspectoNormativo AspectoNormativo)
        {
            using var conn = _proveedorConexion.ObtenerConexion();
            var filas = await conn.ExecuteAsync(
                @"UPDATE aspecto_normativo 
                  SET tipo = @Tipo, descripcion = @Descripcion, fuente = @Fuente 
                  WHERE id = @Id", AspectoNormativo);
            return filas > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            using var conn = _proveedorConexion.ObtenerConexion();
            var filas = await conn.ExecuteAsync(
                "DELETE FROM aspecto_normativo WHERE id = @Id", new { Id = id });
            return filas > 0;
        }
    }
}