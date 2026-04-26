using Dapper;
using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios.Abstracciones;

namespace ApiKnowledgeMap.Repositorios
{
    public class UsuarioRolRepository : IUsuarioRolRepository
    {
        private readonly IProveedorConexion _conexion;

        public UsuarioRolRepository(IProveedorConexion conexion)
        {
            _conexion = conexion;
        }

        public async Task<IEnumerable<UsuarioRol>> ObtenerPorUsuarioAsync(int usuarioId)
        {
            using var conn = _conexion.ObtenerConexion();
            return await conn.QueryAsync<UsuarioRol>(
                @"SELECT usuario_id AS UsuarioId, rol_id AS RolId
                  FROM usuario_rol WHERE usuario_id = @UsuarioId",
                new { UsuarioId = usuarioId });
        }

        public async Task<bool> InsertarAsync(UsuarioRol ur)
        {
            using var conn = _conexion.ObtenerConexion();
            var filas = await conn.ExecuteAsync(
                @"INSERT INTO usuario_rol (usuario_id, rol_id)
                  VALUES (@UsuarioId, @RolId)", ur);
            return filas > 0;
        }

        public async Task<bool> EliminarAsync(int usuarioId, int rolId)
        {
            using var conn = _conexion.ObtenerConexion();
            var filas = await conn.ExecuteAsync(
                @"DELETE FROM usuario_rol 
                  WHERE usuario_id = @UsuarioId AND rol_id = @RolId",
                new { UsuarioId = usuarioId, RolId = rolId });
            return filas > 0;
        }

        public async Task<bool> EliminarTodosDeUsuarioAsync(int usuarioId)
        {
            using var conn = _conexion.ObtenerConexion();
            var filas = await conn.ExecuteAsync(
                "DELETE FROM usuario_rol WHERE usuario_id = @UsuarioId",
                new { UsuarioId = usuarioId });
            return filas > 0;
        }
    }
}