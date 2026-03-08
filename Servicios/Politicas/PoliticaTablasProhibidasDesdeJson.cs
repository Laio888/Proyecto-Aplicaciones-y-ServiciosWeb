// PoliticaTablasProhibidasDesdeJson.cs — Lee tablas prohibidas desde appsettings.json
// Ubicación: Servicios/Politicas/PoliticaTablasProhibidasDesdeJson.cs
//
// Principios SOLID aplicados:
// - SRP: Solo se encarga de leer configuración JSON y validar tablas
// - DIP: Implementa IPoliticaTablasProhibidas, permitiendo intercambiarla

using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using ApiKnowledgeMap.Servicios.Abstracciones;

namespace ApiKnowledgeMap.Servicios.Politicas
{
    /// <summary>
    /// Implementación concreta que lee la lista de tablas prohibidas desde appsettings.json.
    /// 
    /// CONFIGURACIÓN ESPERADA EN appsettings.json:
    /// {
    ///   "TablasProhibidas": ["usuarios", "roles", "sys_config"]
    /// }
    /// 
    /// Si la lista está vacía: TODAS las tablas están permitidas.
    /// La comparación es case-insensitive.
    /// </summary>
    public class PoliticaTablasProhibidasDesdeJson : IPoliticaTablasProhibidas
    {
        // HashSet proporciona búsqueda O(1) vs List que sería O(n)
        private readonly HashSet<string> _tablasProhibidas;

        public PoliticaTablasProhibidasDesdeJson(IConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(
                    nameof(configuration),
                    "IConfiguration no puede ser null."
                );

            var tablasProhibidasArray = configuration.GetSection("TablasProhibidas")
                .Get<string[]>() ?? Array.Empty<string>();

            _tablasProhibidas = new HashSet<string>(
                tablasProhibidasArray.Where(t => !string.IsNullOrWhiteSpace(t)),
                StringComparer.OrdinalIgnoreCase
            );
        }

        /// <summary>
        /// Determina si una tabla está permitida verificando si NO está en la lista de prohibidas.
        /// </summary>
        public bool EsTablaPermitida(string nombreTabla)
        {
            if (string.IsNullOrWhiteSpace(nombreTabla))
                return false;

            return !_tablasProhibidas.Contains(nombreTabla);
        }

        public IReadOnlyCollection<string> ObtenerTablasProhibidas()
        {
            return _tablasProhibidas;
        }

        public bool TieneRestricciones()
        {
            return _tablasProhibidas.Count > 0;
        }
    }

}

