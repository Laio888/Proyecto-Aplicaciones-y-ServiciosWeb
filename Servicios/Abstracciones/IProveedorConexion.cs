// IProveedorConexion.cs — Interface que define el contrato para obtener conexiones a base de datos
// Ubicación: Servicios/Abstracciones/IProveedorConexion.cs
//
// Principios SOLID aplicados:
// - SRP: Solo define operaciones relacionadas con conexiones
// - DIP: Permite que otras clases dependan de esta abstracción, no de implementaciones concretas
// - ISP: Interface específica y pequeña
// - OCP: Abierta para extensión, cerrada para modificación

using System;

namespace ApiKnowledgeMap.Servicios.Abstracciones
{
    /// <summary>
    /// Contrato que define cómo obtener información de conexión a base de datos.
    /// </summary>
    public interface IProveedorConexion
    {
        /// <summary>
        /// Obtiene el nombre del proveedor de base de datos actualmente configurado.
        /// Valores esperados: "SqlServer", "Postgres", "MariaDB", "MySQL"
        /// </summary>
        string ProveedorActual { get; }

        /// <summary>
        /// Obtiene la cadena de conexión correspondiente al proveedor configurado.
        /// </summary>
        string ObtenerCadenaConexion();
    }
}
