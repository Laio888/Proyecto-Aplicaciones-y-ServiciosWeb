// ConfiguracionJwt.cs — Clase que mapea la tabla de enfoque
// Ubicación: Modelos/enfoque.cs

namespace ApiKnowledgeMap.Modelos
{
    /// <summary>
    /// Enfoques pedagógicos y curriculares.
    /// </summary>
    public class Enfoque
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string nombre { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string descripcion { get; set; } = string.Empty;
        
    }
}
