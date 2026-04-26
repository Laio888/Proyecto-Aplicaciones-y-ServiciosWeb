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

        public async Task<IEnumerable<UsuarioConRoles>> ObtenerTodosAsync()
        {
            using var conn = _conexion.ObtenerConexion();
            var resultado = await conn.QueryAsync<dynamic>(
                @"SELECT 
                    u.id        AS Id,
                    u.nombre    AS Nombre,
                    u.email     AS Email,
                    r.nombre    AS RolNombre
                  FROM usuario u
                  LEFT JOIN usuario_rol ur ON u.id = ur.usuario_id
                  LEFT JOIN rol r ON ur.rol_id = r.id");

            return resultado
                .GroupBy(x => (int)x.Id)
                .Select(g => new UsuarioConRoles
                {
                    Id = g.Key,
                    Nombre = g.First().Nombre,
                    Email = g.First().Email,
                    Roles = g
                        .Where(x => x.RolNombre != null)
                        .Select(x => (string)x.RolNombre)
                        .ToList()
                });
        }

        public async Task<UsuarioConRoles?> ObtenerPorIdAsync(int id)
        {
            using var conn = _conexion.ObtenerConexion();
            var resultado = await conn.QueryAsync<dynamic>(
                @"SELECT 
                    u.id        AS Id,
                    u.nombre    AS Nombre,
                    u.email     AS Email,
                    r.nombre    AS RolNombre
                  FROM usuario u
                  LEFT JOIN usuario_rol ur ON u.id = ur.usuario_id
                  LEFT JOIN rol r ON ur.rol_id = r.id
                  WHERE u.id = @Id",
                new { Id = id });

            var lista = resultado.ToList();
            if (!lista.Any()) return null;

            return new UsuarioConRoles
            {
                Id = (int)lista.First().Id,
                Nombre = (string)lista.First().Nombre,
                Email = (string)lista.First().Email,
                Roles = lista
                    .Where(x => x.RolNombre != null)
                    .Select(x => (string)x.RolNombre)
                    .ToList()
            };
        }

        public async Task<Usuario?> ObtenerPorEmailAsync(string email)
        {
            using var conn = _conexion.ObtenerConexion();
            return await conn.QueryFirstOrDefaultAsync<Usuario>(
                @"SELECT id AS Id, nombre AS Nombre, 
                         email AS Email, contrasena AS Contrasena
                  FROM usuario WHERE email = @Email",
                new { Email = email });
        }

        public async Task<int> InsertarAsync(Usuario u)
        {
            using var conn = _conexion.ObtenerConexion();
            return await conn.ExecuteScalarAsync<int>(
                @"INSERT INTO usuario (nombre, email, contrasena)
                  VALUES (@Nombre, @Email, @Contrasena);
                  SELECT SCOPE_IDENTITY();",
                u);
        }

        public async Task<bool> ActualizarAsync(Usuario u)
        {
            using var conn = _conexion.ObtenerConexion();
            var filas = await conn.ExecuteAsync(
                @"UPDATE usuario SET
                    nombre     = @Nombre,
                    email      = @Email,
                    contrasena = @Contrasena
                  WHERE id = @Id",
                u);
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