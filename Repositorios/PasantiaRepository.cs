using Dapper;
using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios.Abstracciones;

namespace ApiKnowledgeMap.Repositorios
{
    public class PasantiaRepository : IPasantiaRepository
    {
        private readonly IProveedorConexion _proveedorConexion;

        public PasantiaRepository(IProveedorConexion proveedorConexion)
        {
            _proveedorConexion = proveedorConexion;
        }

        public async Task<IEnumerable<Pasantia>> ObtenerTodosAsync()
        {
            using var conn = _proveedorConexion.ObtenerConexion();

            return await conn.QueryAsync<Pasantia>(
                @"SELECT 
                    id AS Id,
                    nombre AS Nombre,
                    pais AS Pais,
                    empresa AS Empresa,
                    descripcion AS Descripcion,
                    programa AS Programa
                  FROM pasantia");
        }

        public async Task<Pasantia?> ObtenerPorIdAsync(int id)
        {
            using var conn = _proveedorConexion.ObtenerConexion();

            return await conn.QueryFirstOrDefaultAsync<Pasantia>(
                @"SELECT 
                    id AS Id,
                    nombre AS Nombre,
                    pais AS Pais,
                    empresa AS Empresa,
                    descripcion AS Descripcion,
                    programa AS Programa
                  FROM pasantia
                  WHERE id = @Id",
                new { Id = id });
        }

        public async Task<int> InsertarAsync(Pasantia pasantia)
        {
            using var conn = _proveedorConexion.ObtenerConexion();

            return await conn.ExecuteScalarAsync<int>(
                @"INSERT INTO pasantia (id, nombre, pais, empresa, descripcion, programa)
                  VALUES (@Id, @Nombre, @Pais, @Empresa, @Descripcion, @Programa);
                  SELECT @Id;",
                pasantia);
        }

        public async Task<bool> ActualizarAsync(Pasantia pasantia)
        {
            using var conn = _proveedorConexion.ObtenerConexion();

            var filas = await conn.ExecuteAsync(
                @"UPDATE pasantia
                  SET nombre = @Nombre,
                      pais = @Pais,
                      empresa = @Empresa,
                      descripcion = @Descripcion,
                      programa = @Programa
                  WHERE id = @Id",
                pasantia);

            return filas > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            using var conn = _proveedorConexion.ObtenerConexion();

            var filas = await conn.ExecuteAsync(
                "DELETE FROM pasantia WHERE id = @Id",
                new { Id = id });

            return filas > 0;
        }
    }
}