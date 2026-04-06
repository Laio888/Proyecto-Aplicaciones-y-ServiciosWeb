using Dapper;
using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios.Abstracciones;

namespace ApiKnowledgeMap.Repositorios
{
    public class ProgramaRepository : IProgramaRepository
    {
        private readonly IProveedorConexion _proveedorConexion;

        public ProgramaRepository(IProveedorConexion proveedorConexion)
        {
            _proveedorConexion = proveedorConexion;
        }

        public async Task<IEnumerable<Programa>> ObtenerTodosAsync()
        {
            using var conn = _proveedorConexion.ObtenerConexion();
            return await conn.QueryAsync<Programa>(
                "SELECT id AS Id, nombre AS Nombre, tipo AS Tipo, nivel AS Nivel, fecha_creacion AS FechaCreacion, fecha_cierre AS FechaCierre, numero_cohortes AS NumeroCohortes, cant_graduados AS CantGraduados, fecha_actualizacion AS FechaActualizacion, ciudad AS Ciudad, facultad AS Facultad FROM programa");
        }

        public async Task<Programa?> ObtenerPorIdAsync(int id)
        {
            using var conn = _proveedorConexion.ObtenerConexion();
            return await conn.QueryFirstOrDefaultAsync<Programa>(
                "SELECT id AS Id, nombre AS Nombre, tipo AS Tipo, nivel AS Nivel, fecha_creacion AS FechaCreacion, fecha_cierre AS FechaCierre, numero_cohortes AS NumeroCohortes, cant_graduados AS CantGraduados, fecha_actualizacion AS FechaActualizacion, ciudad AS Ciudad, facultad AS Facultad FROM programa WHERE id = @Id",
                new { Id = id });
        }

        public async Task<int> InsertarAsync(Programa Programa)
        {
            using var conn = _proveedorConexion.ObtenerConexion();
            return await conn.ExecuteScalarAsync<int>(
                @"INSERT INTO programa (id, nombre, tipo,nivel, fecha_creacion, fecha_cierre, numero_cohortes, cant_graduados, fecha_actualizacion, ciudad, facultad )
                  VALUES (@Id, @Nombre, @Tipo, @Nivel, @FechaCreacion, @FechaCierre,@NumeroCohortes, @CantGraduados, @FechaActualizacion,@Ciudad,@Facultad);
                  SELECT SCOPE_IDENTITY();", Programa);
        }

        public async Task<bool> ActualizarAsync(Programa Programa)
        {
            using var conn = _proveedorConexion.ObtenerConexion();
            var filas = await conn.ExecuteAsync(
                @"UPDATE programa 
                  SET nombre = @Nombre, tipo = @Tipo, nivel = @Nivel, fecha_creacion = @FechaCreacion, fecha_cierre = @FechaCierre, numero_cohortes = @NumeroCohortes, cant_graduados = @CantGraduados, fecha_actualizacion = @FechaActualizacion, ciudad = @Ciudad, facultad = @Facultad
                  WHERE id = @Id", Programa);
            return filas > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            using var conn = _proveedorConexion.ObtenerConexion();
            var filas = await conn.ExecuteAsync(
                "DELETE FROM programa WHERE id = @Id", new { Id = id });
            return filas > 0;
        }
    }
}