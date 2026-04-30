using Dapper;
using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios.Abstracciones;

namespace ApiKnowledgeMap.Repositorios
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IProveedorConexion _conexion;

        public UsuarioRepository(IProveedorConexion conexion)
        {
            _conexion = conexion;
        }

        public async Task<IEnumerable<Usuario>> ObtenerTodosAsync()
        {
            using var conn = _conexion.ObtenerConexion();

            return await conn.QueryAsync<Usuario>(
                @"SELECT u.id, u.nombre, u.email, u.rol_id AS RolId,
                         r.nombre AS RolNombre
                  FROM usuario u
                  INNER JOIN rol r ON u.rol_id = r.id");
        }

        public async Task<Usuario?> ObtenerPorIdAsync(int id)
        {
            using var conn = _conexion.ObtenerConexion();

            return await conn.QueryFirstOrDefaultAsync<Usuario>(
                @"SELECT u.id, u.nombre, u.email, u.rol_id AS RolId,
                         r.nombre AS RolNombre
                  FROM usuario u
                  INNER JOIN rol r ON u.rol_id = r.id
                  WHERE u.id = @Id",
                new { Id = id });
        }

        public async Task<bool> ActualizarAsync(Usuario usuario)
        {
            using var conn = _conexion.ObtenerConexion();

            var filas = await conn.ExecuteAsync(
                @"UPDATE usuario
                  SET nombre = @Nombre,
                      email = @Email,
                      rol_id = @RolId
                  WHERE id = @Id",
                usuario);

            return filas > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            using var conn = _conexion.ObtenerConexion();

            var filas = await conn.ExecuteAsync(
                "DELETE FROM usuario WHERE id = @Id",
                new { Id = id });

            return filas > 0;
        }
    }
}