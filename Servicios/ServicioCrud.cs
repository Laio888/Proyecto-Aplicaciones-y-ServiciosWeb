// ServicioCrud.cs — Implementación de la lógica de negocio que coordina operaciones CRUD
// Ubicación: Servicios/ServicioCrud.cs
//
// Principios SOLID aplicados:
// - SRP: Solo se encarga de lógica de negocio, delega el acceso a datos al repositorio
// - DIP: Depende de IRepositorioLecturaTabla e IPoliticaTablasProhibidas (abstracciones)
// - OCP: Se puede cambiar el repositorio sin modificar este servicio
// - LSP: Implementa completamente IServicioCrud

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiKnowledgeMap.Servicios.Abstracciones;
using ApiKnowledgeMap.Repositorios.Abstracciones;

namespace ApiKnowledgeMap.Servicios
{
    /// <summary>
    /// Implementación concreta del servicio CRUD que aplica reglas de negocio.
    /// Actúa como coordinadora entre la capa de presentación (Controllers)
    /// y la capa de acceso a datos (Repositorios).
    /// </summary>
    public class ServicioCrud : IServicioCrud
    {
        private readonly IRepositorioLecturaTabla _repositorioLectura;
        private readonly IPoliticaTablasProhibidas _politicaTablasProhibidas;

        public ServicioCrud(
            IRepositorioLecturaTabla repositorioLectura,
            IPoliticaTablasProhibidas politicaTablasProhibidas)
        {
            _repositorioLectura = repositorioLectura ?? throw new ArgumentNullException(
                nameof(repositorioLectura),
                "IRepositorioLecturaTabla no puede ser null. " +
                "Verificar que esté registrado en Program.cs con AddScoped<IRepositorioLecturaTabla, ...>()"
            );
            _politicaTablasProhibidas = politicaTablasProhibidas ?? throw new ArgumentNullException(
                nameof(politicaTablasProhibidas),
                "IPoliticaTablasProhibidas no puede ser null. " +
                "Verificar que esté registrado en Program.cs con AddSingleton<IPoliticaTablasProhibidas, PoliticaTablasProhibidasDesdeJson>()"
            );
        }

        public async Task<IReadOnlyList<Dictionary<string, object?>>> ListarAsync(
            string nombreTabla,
            string? esquema,
            int? limite)
        {
            // FASE 1: VALIDACIONES DE REGLAS DE NEGOCIO
            if (string.IsNullOrWhiteSpace(nombreTabla))
                throw new ArgumentException("El nombre de la tabla no puede estar vacío.", nameof(nombreTabla));

            // VALIDACIÓN DE TABLAS PROHIBIDAS
            if (!_politicaTablasProhibidas.EsTablaPermitida(nombreTabla))
                throw new UnauthorizedAccessException(
                    $"Acceso denegado: La tabla '{nombreTabla}' está restringida y no puede ser consultada."
                );

            // FASE 2: NORMALIZACIÓN DE PARÁMETROS
            string? esquemaNormalizado = string.IsNullOrWhiteSpace(esquema) ? null : esquema.Trim();
            int? limiteNormalizado = (limite is null || limite <= 0) ? null : limite;

            // FASE 3: DELEGACIÓN AL REPOSITORIO (DIP EN ACCIÓN)
            var filas = await _repositorioLectura.ObtenerFilasAsync(nombreTabla, esquemaNormalizado, limiteNormalizado);

            return filas;
        }

        public async Task<IReadOnlyList<Dictionary<string, object?>>> ObtenerPorClaveAsync(
            string nombreTabla,
            string? esquema,
            string nombreClave,
            string valor)
        {
            if (string.IsNullOrWhiteSpace(nombreTabla))
                throw new ArgumentException("El nombre de la tabla no puede estar vacío.", nameof(nombreTabla));
            if (string.IsNullOrWhiteSpace(nombreClave))
                throw new ArgumentException("El nombre de la clave no puede estar vacío.", nameof(nombreClave));
            if (string.IsNullOrWhiteSpace(valor))
                throw new ArgumentException("El valor no puede estar vacío.", nameof(valor));

            if (!_politicaTablasProhibidas.EsTablaPermitida(nombreTabla))
                throw new UnauthorizedAccessException(
                    $"Acceso denegado: La tabla '{nombreTabla}' está restringida y no puede ser consultada."
                );

            string? esquemaNormalizado = string.IsNullOrWhiteSpace(esquema) ? null : esquema.Trim();
            string nombreClaveNormalizado = nombreClave.Trim();
            string valorNormalizado = valor.Trim();

            var filas = await _repositorioLectura.ObtenerPorClaveAsync(
                nombreTabla, esquemaNormalizado, nombreClaveNormalizado, valorNormalizado);

            return filas;
        }

        public async Task<bool> CrearAsync(
            string nombreTabla,
            string? esquema,
            Dictionary<string, object?> datos,
            string? camposEncriptar = null)
        {
            if (string.IsNullOrWhiteSpace(nombreTabla))
                throw new ArgumentException("El nombre de la tabla no puede estar vacío.", nameof(nombreTabla));
            if (datos == null || !datos.Any())
                throw new ArgumentException("Los datos no pueden estar vacíos.", nameof(datos));

            if (!_politicaTablasProhibidas.EsTablaPermitida(nombreTabla))
                throw new UnauthorizedAccessException(
                    $"Acceso denegado: La tabla '{nombreTabla}' está restringida y no puede ser modificada."
                );

            string? esquemaNormalizado = string.IsNullOrWhiteSpace(esquema) ? null : esquema.Trim();
            string? camposEncriptarNormalizados = string.IsNullOrWhiteSpace(camposEncriptar) ? null : camposEncriptar.Trim();

            return await _repositorioLectura.CrearAsync(
                nombreTabla, esquemaNormalizado, datos, camposEncriptarNormalizados);
        }

        public async Task<int> ActualizarAsync(
            string nombreTabla,
            string? esquema,
            string nombreClave,
            string valorClave,
            Dictionary<string, object?> datos,
            string? camposEncriptar = null)
        {
            if (string.IsNullOrWhiteSpace(nombreTabla))
                throw new ArgumentException("El nombre de la tabla no puede estar vacío.", nameof(nombreTabla));
            if (string.IsNullOrWhiteSpace(nombreClave))
                throw new ArgumentException("El nombre de la clave no puede estar vacío.", nameof(nombreClave));
            if (string.IsNullOrWhiteSpace(valorClave))
                throw new ArgumentException("El valor de la clave no puede estar vacío.", nameof(valorClave));
            if (datos == null || !datos.Any())
                throw new ArgumentException("Los datos a actualizar no pueden estar vacíos.", nameof(datos));

            if (!_politicaTablasProhibidas.EsTablaPermitida(nombreTabla))
                throw new UnauthorizedAccessException(
                    $"Acceso denegado: La tabla '{nombreTabla}' está restringida y no puede ser modificada."
                );

            string? esquemaNormalizado = string.IsNullOrWhiteSpace(esquema) ? null : esquema.Trim();
            string nombreClaveNormalizado = nombreClave.Trim();
            string valorClaveNormalizado = valorClave.Trim();
            string? camposEncriptarNormalizados = string.IsNullOrWhiteSpace(camposEncriptar) ? null : camposEncriptar.Trim();

            return await _repositorioLectura.ActualizarAsync(
                nombreTabla, esquemaNormalizado, nombreClaveNormalizado,
                valorClaveNormalizado, datos, camposEncriptarNormalizados);
        }

        public async Task<int> EliminarAsync(
            string nombreTabla,
            string? esquema,
            string nombreClave,
            string valorClave)
        {
            if (string.IsNullOrWhiteSpace(nombreTabla))
                throw new ArgumentException("El nombre de la tabla no puede estar vacío.", nameof(nombreTabla));
            if (string.IsNullOrWhiteSpace(nombreClave))
                throw new ArgumentException("El nombre de la clave no puede estar vacío.", nameof(nombreClave));
            if (string.IsNullOrWhiteSpace(valorClave))
                throw new ArgumentException("El valor de la clave no puede estar vacío.", nameof(valorClave));

            if (!_politicaTablasProhibidas.EsTablaPermitida(nombreTabla))
                throw new UnauthorizedAccessException(
                    $"Acceso denegado: La tabla '{nombreTabla}' está restringida y no puede ser modificada."
                );

            string? esquemaNormalizado = string.IsNullOrWhiteSpace(esquema) ? null : esquema.Trim();
            string nombreClaveNormalizado = nombreClave.Trim();
            string valorClaveNormalizado = valorClave.Trim();

            return await _repositorioLectura.EliminarAsync(
                nombreTabla, esquemaNormalizado, nombreClaveNormalizado, valorClaveNormalizado);
        }

        public async Task<(int codigo, string mensaje)> VerificarContrasenaAsync(
            string nombreTabla,
            string? esquema,
            string campoUsuario,
            string campoContrasena,
            string valorUsuario,
            string valorContrasena)
        {
            if (string.IsNullOrWhiteSpace(nombreTabla))
                throw new ArgumentException("El nombre de la tabla no puede estar vacío.", nameof(nombreTabla));
            if (string.IsNullOrWhiteSpace(campoUsuario))
                throw new ArgumentException("El campo de usuario no puede estar vacío.", nameof(campoUsuario));
            if (string.IsNullOrWhiteSpace(campoContrasena))
                throw new ArgumentException("El campo de contraseña no puede estar vacío.", nameof(campoContrasena));
            if (string.IsNullOrWhiteSpace(valorUsuario))
                throw new ArgumentException("El valor de usuario no puede estar vacío.", nameof(valorUsuario));
            if (string.IsNullOrWhiteSpace(valorContrasena))
                throw new ArgumentException("La contraseña no puede estar vacía.", nameof(valorContrasena));

            if (!_politicaTablasProhibidas.EsTablaPermitida(nombreTabla))
                throw new UnauthorizedAccessException(
                    $"Acceso denegado: La tabla '{nombreTabla}' está restringida y no puede ser consultada."
                );

            string? esquemaNormalizado = string.IsNullOrWhiteSpace(esquema) ? null : esquema.Trim();
            string campoUsuarioNormalizado = campoUsuario.Trim();
            string campoContrasenaNormalizado = campoContrasena.Trim();
            string valorUsuarioNormalizado = valorUsuario.Trim();

            try
            {
                string? hashAlmacenado = await _repositorioLectura.ObtenerHashContrasenaAsync(
                    nombreTabla, esquemaNormalizado, campoUsuarioNormalizado,
                    campoContrasenaNormalizado, valorUsuarioNormalizado);

                if (hashAlmacenado == null)
                    return (404, "Usuario no encontrado");

                bool contrasenaCorrecta = ApiKnowledgeMap.Servicios.Utilidades.EncriptacionBCrypt.Verificar(
                    valorContrasena, hashAlmacenado);

                return contrasenaCorrecta
                    ? (200, "Credenciales válidas")
                    : (401, "Contraseña incorrecta");
            }
            catch (Exception excepcion)
            {
                throw new InvalidOperationException(
                    $"Error durante la verificación de credenciales: {excepcion.Message}",
                    excepcion
                );
            }
        }
        public async Task<bool> ExisteAsync(
    string nombreTabla,
    string? esquema,
    string nombreClave,
    string valorClave)
        {
            var resultado = await _repositorioLectura.ObtenerPorClaveAsync(
                nombreTabla,
                esquema,
                nombreClave,
                valorClave
            );

            return resultado.Count > 0;
        }
    }

}

