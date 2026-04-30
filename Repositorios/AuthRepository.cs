using Dapper;
using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios.Abstracciones;

namespace ApiKnowledgeMap.Repositorios
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IProveedorConexion _conexion;

        public AuthRepository(IProveedorConexion conexion)
        {
            _conexion = conexion;
        }

        public async Task<Usuario?> ObtenerPorEmailAsync(string email)
        {
            using var conn = _conexion.ObtenerConexion();

            return await conn.QueryFirstOrDefaultAsync<Usuario>(
                @"SELECT u.id AS Id,
                         u.nombre AS Nombre,
                         u.email AS Email,
                         u.password_hash AS PasswordHash,
                         u.rol_id AS RolId,
                         r.nombre AS RolNombre
                  FROM usuario u
                  INNER JOIN rol r ON u.rol_id = r.id
                  WHERE u.email = @Email",
                new { Email = email });
        }

        public async Task<int> CrearUsuarioAsync(Usuario usuario)
        {
            using var conn = _conexion.ObtenerConexion();

            return await conn.ExecuteScalarAsync<int>(
                @"INSERT INTO usuario (id,nombre,email,password_hash,rol_id)
                  VALUES (@Id,@Nombre,@Email,@PasswordHash,@RolId);
                  SELECT @Id;", usuario);
        }
    }
}