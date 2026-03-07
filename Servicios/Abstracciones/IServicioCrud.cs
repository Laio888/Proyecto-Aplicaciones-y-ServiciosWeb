// IServicioCrud.cs — Interface que define el contrato para operaciones de lógica de negocio
// Ubicación: Servicios/Abstracciones/IServicioCrud.cs
//
// Principios SOLID aplicados:
// - SRP: Solo define operaciones de lógica de negocio
// - DIP: Permite que el controlador dependa de esta abstracción
// - ISP: Interface específica y enfocada
// - OCP: Abierta para extensión, cerrada para modificación

using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiKnowledgeMap.Servicios.Abstracciones
{
    /// <summary>
    /// Interface que define el contrato para un servicio CRUD genérico.
    /// La capa de servicios aplica la lógica de negocio: validaciones, políticas,
    /// transformaciones de datos y coordinación con repositorios.
    /// </summary>
    public interface IServicioCrud
    {
        /// <summary>
        /// Lista registros de una tabla aplicando reglas de negocio.
        /// </summary>
        Task<IReadOnlyList<Dictionary<string, object?>>> ListarAsync(
            string nombreTabla,
            string? esquema,
            int? limite
        );

        /// <summary>
        /// Obtiene registros filtrados por un valor de clave específico.
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
        /// Elimina un registro de la tabla especificada.
        /// </summary>
        Task<int> EliminarAsync(
            string nombreTabla,
            string? esquema,
            string nombreClave,
            string valorClave
        );

        /// <summary>
        /// Verifica credenciales de usuario usando BCrypt.
        /// </summary>
        Task<(int codigo, string mensaje)> VerificarContrasenaAsync(
            string nombreTabla,
            string? esquema,
            string campoUsuario,
            string campoContrasena,
            string valorUsuario,
            string valorContrasena
        );
    }
}
