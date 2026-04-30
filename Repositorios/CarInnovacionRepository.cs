using Dapper;
using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios.Abstracciones;

namespace ApiKnowledgeMap.Repositorios
{
    public class CarInnovacionRepository : ICarInnovacionRepository
    {
        private readonly IProveedorConexion _proveedorConexion;

        public CarInnovacionRepository(IProveedorConexion proveedorConexion)
        {
            _proveedorConexion = proveedorConexion;
        }

        public async Task<IEnumerable<CarInnovacion>> ObtenerTodosAsync()
        {
            using var conn = _proveedorConexion.ObtenerConexion();
            return await conn.QueryAsync<CarInnovacion>(
                "SELECT id AS Id, nombre AS Nombre, descripcion AS Descripcion, tipo AS Tipo FROM car_innovacion");
        }

        public async Task<CarInnovacion?> ObtenerPorIdAsync(int id)
        {
            using var conn = _proveedorConexion.ObtenerConexion();
            return await conn.QueryFirstOrDefaultAsync<CarInnovacion>(
                "SELECT id AS Id, nombre AS Nombre, descripcion AS Descripcion, tipo AS Tipo FROM car_innovacion WHERE id = @Id",
                new { Id = id });
        }

        public async Task<int> InsertarAsync(CarInnovacion carInnovacion)
        {
            using var conn = _proveedorConexion.ObtenerConexion();
            return await conn.ExecuteScalarAsync<int>(
                @"INSERT INTO car_innovacion (id, nombre, descripcion, tipo)
                  VALUES (@Id, @Nombre, @Descripcion, @Tipo);
                  SELECT SCOPE_IDENTITY();", carInnovacion);
        }

        public async Task<bool> ActualizarAsync(CarInnovacion carInnovacion)
        {
            using var conn = _proveedorConexion.ObtenerConexion();
            var filas = await conn.ExecuteAsync(
                @"UPDATE car_innovacion 
                  SET nombre = @Nombre, descripcion = @Descripcion, tipo = @Tipo
                  WHERE id = @Id", carInnovacion);
            return filas > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            using var conn = _proveedorConexion.ObtenerConexion();
            var filas = await conn.ExecuteAsync(
                "DELETE FROM car_innovacion WHERE id = @Id", new { Id = id });
            return filas > 0;
        }
    }
}