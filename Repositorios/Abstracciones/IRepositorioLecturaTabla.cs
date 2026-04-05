// IRepositorioLecturaTabla.cs — Interface que define el contrato para lectura genérica de tablas
// Ubicación: Repositorios/Abstracciones/IRepositorioLecturaTabla.cs
//
// Principios SOLID aplicados:
// - SRP: Esta interface solo define operaciones de LECTURA, no mezcla responsabilidades
// - DIP: Permite que los servicios dependan de esta abstracción, no de implementaciones concretas
// - OCP: Abierta para extensión (nuevas implementaciones) pero cerrada para modificación
// - ISP: Interface específica y enfocada, solo contiene métodos de lectura

using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiKnowledgeMap.Repositorios.Abstracciones
{
    /// <summary>
    /// Contrato para repositorios que realizan operaciones de lectura sobre tablas de base de datos.
    /// Esta interface define el "QUÉ" se puede hacer, no el "CÓMO" se hace.
    /// </summary>
    public interface IRepositorioLecturaTabla
    {
        /// <summary>
        /// Obtiene filas de una tabla específica como lista de diccionarios.
        /// </summary>
        Task<IReadOnlyList<Dictionary<string, object?>>> ObtenerFilasAsync(
            string nombreTabla,
            string? esquema,
            int? limite
        );

        /// <summary>
        /// Obtiene filas filtradas por un valor específico en una columna.
        /// </summary>
        Task<IReadOnlyList<Dictionary<string, object?>>> ObtenerPorClaveAsync(
            string nombreTabla,
            string? esquema,
            string nombreClave,
            string valor
        );

        /// <summary>
        /// Crea un nuevo registro en la tabla especificada.
        /// </summary>
        Task<bool> CrearAsync(
            string nombreTabla,
            string? esquema,
            Dictionary<string, object?> datos,
            string? camposEncriptar = null
        );

        /// <summary>
        /// Actualiza un registro existente en la tabla especificada.
        /// </summary>
        Task<int> ActualizarAsync(
            string nombreTabla,
            string? esquema,
            string nombreClave,
            string valorClave,
            Dictionary<string, object?> datos,
            string? camposEncriptar = null
        );

        /// <summary>
        /// Elimina un registro existente de la tabla especificada.
        /// </summary>
        Task<int> EliminarAsync(
            string nombreTabla,
            string? esquema,
            string nombreClave,
            string valorClave
        );

        /// <summary>
        /// Verifica credenciales de usuario obteniendo el hash almacenado para comparación.
        /// </summary>
        Task<string?> ObtenerHashContrasenaAsync(
            string nombreTabla,
            string? esquema,
            string campoUsuario,
            string campoContrasena,
            string valorUsuario
        );

        /// <summary>
        /// Obtiene información de diagnóstico sobre la conexión actual a la base de datos.
        /// </summary>
        Task<Dictionary<string, object?>> ObtenerDiagnosticoConexionAsync();
    }

}

