// IPoliticaTablasProhibidas.cs — Interfaz para gestión de tablas permitidas/prohibidas
// Ubicación: Servicios/Abstracciones/IPoliticaTablasProhibidas.cs
//
// Principios SOLID aplicados:
// - SRP: Responsabilidad única = decidir si una tabla está permitida o prohibida
// - DIP: Los servicios dependen de esta abstracción, no de la configuración directa
// - OCP: Se puede extender con nuevas implementaciones
// - ISP: Interfaz pequeña y específica con un solo método

namespace ApiKnowledgeMap.Servicios.Abstracciones
{
    /// <summary>
    /// Interfaz que define el contrato para validar si una tabla está permitida o prohibida.
    /// Permite proteger tablas sensibles del sistema que no deben ser accesibles vía API genérica.
    /// </summary>
    public interface IPoliticaTablasProhibidas
    {
        /// <summary>
        /// Determina si una tabla específica está permitida para operaciones CRUD.
        /// </summary>
        /// <param name="nombreTabla">Nombre de la tabla a validar (case-insensitive).</param>
        /// <returns>true si la tabla está permitida, false si está prohibida.</returns>
        bool EsTablaPermitida(string nombreTabla);
    }
}
