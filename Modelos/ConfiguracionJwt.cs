// ConfiguracionJwt.cs — Clase que mapea la sección JWT de appsettings.json
// Ubicación: Modelos/ConfiguracionJwt.cs

namespace ApiKnowledgeMap.Modelos
{
    /// <summary>
    /// Clase que representa la configuración JWT desde appsettings.json.
    /// Se usa con IOptions<ConfiguracionJwt> o Bind() en Program.cs.
    /// </summary>
    public class ConfiguracionJwt
    {
        /// <summary>
        /// Clave secreta para firmar tokens (mínimo 32 caracteres).
        /// </summary>
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// Quién emite el token (nombre de tu app).
        /// </summary>
        public string Issuer { get; set; } = string.Empty;

        /// <summary>
        /// Para quién es el token (usuarios de tu app).
        /// </summary>
        public string Audience { get; set; } = string.Empty;

        /// <summary>
        /// Tiempo de vida del token en minutos antes de expirar.
        /// </summary>
        public int DuracionMinutos { get; set; } = 60;
    }
}
