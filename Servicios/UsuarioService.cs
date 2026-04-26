using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios.Abstracciones;
using BCrypt.Net;

namespace ApiKnowledgeMap.Servicios
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly IUsuarioRolRepository _usuarioRolRepo;

        public UsuarioService(
            IUsuarioRepository usuarioRepo,
            IUsuarioRolRepository usuarioRolRepo)
        {
            _usuarioRepo = usuarioRepo;
            _usuarioRolRepo = usuarioRolRepo;
        }

        public Task<IEnumerable<UsuarioConRoles>> ObtenerTodosAsync()
            => _usuarioRepo.ObtenerTodosAsync();

        public Task<UsuarioConRoles?> ObtenerPorIdAsync(int id)
            => _usuarioRepo.ObtenerPorIdAsync(id);

        public async Task<int> CrearAsync(Usuario usuario, List<int> roles)
        {
            // Encriptar contraseña
            usuario.Contrasena = BCrypt.Net.BCrypt.HashPassword(usuario.Contrasena);

            var id = await _usuarioRepo.InsertarAsync(usuario);

            // Asignar roles
            foreach (var rolId in roles)
            {
                await _usuarioRolRepo.InsertarAsync(new UsuarioRol
                {
                    UsuarioId = id,
                    RolId = rolId
                });
            }

            return id;
        }

        public async Task<bool> ActualizarAsync(Usuario usuario, List<int> roles)
        {
            // Si cambió la contraseña, encriptarla
            if (!usuario.Contrasena.StartsWith("$2"))
                usuario.Contrasena = BCrypt.Net.BCrypt.HashPassword(usuario.Contrasena);

            var resultado = await _usuarioRepo.ActualizarAsync(usuario);

            // Actualizar roles: eliminar todos y reinsertar
            await _usuarioRolRepo.EliminarTodosDeUsuarioAsync(usuario.Id);
            foreach (var rolId in roles)
            {
                await _usuarioRolRepo.InsertarAsync(new UsuarioRol
                {
                    UsuarioId = usuario.Id,
                    RolId = rolId
                });
            }

            return resultado;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            await _usuarioRolRepo.EliminarTodosDeUsuarioAsync(id);
            return await _usuarioRepo.EliminarAsync(id);
        }

        public async Task<(bool valido, UsuarioConRoles? usuario)> ValidarCredencialesAsync(
            string email, string password)
        {
            var usuario = await _usuarioRepo.ObtenerPorEmailAsync(email);

            if (usuario == null)
                return (false, null);

            bool valido = BCrypt.Net.BCrypt.Verify(password, usuario.Contrasena);

            if (!valido)
                return (false, null);

            var usuarioRoles = await _usuarioRepo.ObtenerPorIdAsync(usuario.Id);

            return (true, usuarioRoles);
        }
    }
}