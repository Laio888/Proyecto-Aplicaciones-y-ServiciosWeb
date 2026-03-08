// ProveedorConexion.cs — Implementación que lee configuración desde appsettings.json
// Ubicación: Servicios/Conexion/ProveedorConexion.cs
//
// Principios SOLID aplicados:
// - SRP: Solo sabe "leer configuración y entregar una cadena de conexión"
// - DIP: Implementa IProveedorConexion
// - OCP: Si se agrega MySQL/Oracle, se extiende sin tocar el resto

using Microsoft.Extensions.Configuration;
using System;
using ApiKnowledgeMap.Servicios.Abstracciones;

namespace ApiKnowledgeMap.Servicios.Conexion
{
    /// <summary>
    /// Implementación concreta que lee "DatabaseProvider" y "ConnectionStrings" desde IConfiguration.
    /// Responsabilidad única: conectar la configuración JSON con los repositorios que necesitan
    /// cadenas de conexión.
    /// </summary>
    public class ProveedorConexion : IProveedorConexion
    {
        private readonly IConfiguration _configuracion;

        public ProveedorConexion(IConfiguration configuracion)
        {
            _configuracion = configuracion ?? throw new ArgumentNullException(
                nameof(configuracion),
                "IConfiguration no puede ser null. Verificar configuración en Program.cs."
            );
        }

        /// <summary>
        /// Lee el valor de "DatabaseProvider" desde appsettings.json.
        /// Si no existe o está vacío, por defecto usa "SqlServer".
        /// </summary>
        public string ProveedorActual
        {
            get
            {
                var valor = _configuracion.GetValue<string>("DatabaseProvider");
                return string.IsNullOrWhiteSpace(valor) ? "SqlServer" : valor.Trim();
            }
        }

        /// <summary>
        /// Entrega la cadena de conexión correspondiente al proveedor actual.
        /// </summary>
        public string ObtenerCadenaConexion()
        {
            string? cadena = _configuracion.GetConnectionString(ProveedorActual);

            if (string.IsNullOrWhiteSpace(cadena))
            {
                throw new InvalidOperationException(
                    $"No se encontró la cadena de conexión para el proveedor '{ProveedorActual}'. " +
                    $"Verificar que existe 'ConnectionStrings:{ProveedorActual}' en appsettings.json " +
                    $"y que 'DatabaseProvider' esté configurado correctamente."
                );
            }

            return cadena;
        }
    }

}

