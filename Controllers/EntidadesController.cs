// EntidadesController.cs — Controlador genérico para operaciones HTTP sobre cualquier tabla
// Ubicación: Controllers/EntidadesController.cs
//
// Principios SOLID aplicados:
// - SRP: El controlador solo coordina peticiones HTTP, no contiene lógica de negocio
// - DIP: Depende de IServicioCrud (abstracción), no de ServicioCrud (implementación concreta)
// - OCP: Preparado para agregar más endpoints sin modificar código existente
//
// Tablas del primer entregable:
//   - car_innovacion  : id, nombre, descripcion, tipo
//   - enfoque         : id, nombre, descripcion
//   - practica_estrategia : id, tipo, nombre, descripcion
//   - aspecto_normativo   : id, tipo, descripcion, fuente
//   - universidad     : id, nombre, tipo, ciudad

using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using ApiKnowledgeMap.Servicios.Abstracciones;
using Microsoft.Data.SqlClient;
using System.Text.Json;

namespace ApiKnowledgeMap.Controllers
{
    /// <summary>
    /// Controlador genérico para operaciones CRUD sobre cualquier tabla de la base de datos
    /// knowledge_map_db.
    /// 
    /// Ruta base: /api/{tabla}
    /// Ejemplos:
    ///   GET    /api/car_innovacion
    ///   GET    /api/enfoque/id/1
    ///   POST   /api/practica_estrategia
    ///   PUT    /api/aspecto_normativo/id/5
    ///   DELETE /api/universidad/id/2
    /// </summary>
    [Route("api/{tabla}")]
    [ApiController]
    public class EntidadesController : ControllerBase
    {
        private readonly IServicioCrud _servicioCrud;
        private readonly ILogger<EntidadesController> _logger;
        private readonly IConfiguration _configuration;

        public EntidadesController(
            IServicioCrud servicioCrud,
            ILogger<EntidadesController> logger,
            IConfiguration configuration)
        {
            _servicioCrud = servicioCrud ?? throw new ArgumentNullException(
                nameof(servicioCrud),
                "IServicioCrud no fue inyectado correctamente. Verificar registro en Program.cs"
            );
            _logger = logger ?? throw new ArgumentNullException(
                nameof(logger),
                "ILogger no fue inyectado correctamente."
            );
            _configuration = configuration ?? throw new ArgumentNullException(
                nameof(configuration),
                "IConfiguration no fue inyectado correctamente."
            );
        }

        // =====================================================================
        // GET /api/{tabla}?esquema={esquema}&limite={limite}
        // Listar todos los registros de una tabla
        // =====================================================================
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> ListarAsync(
            string tabla,
            [FromQuery] string? esquema,
            [FromQuery] int? limite)
        {
            try
            {
                _logger.LogInformation(
                    "INICIO consulta - Tabla: {Tabla}, Esquema: {Esquema}, Límite: {Limite}",
                    tabla, esquema ?? "por defecto", limite?.ToString() ?? "por defecto"
                );

                var filas = await _servicioCrud.ListarAsync(tabla, esquema, limite);

                _logger.LogInformation(
                    "RESULTADO exitoso - Registros obtenidos: {Cantidad} de tabla {Tabla}",
                    filas.Count, tabla
                );

                if (filas.Count == 0)
                {
                    _logger.LogInformation("SIN DATOS - Tabla {Tabla} sin registros", tabla);
                    return NoContent();
                }

                return Ok(new
                {
                    tabla = tabla,
                    esquema = esquema ?? "por defecto",
                    limite = limite,
                    total = filas.Count,
                    datos = filas
                });
            }
            catch (ArgumentException excepcionArgumento)
            {
                _logger.LogWarning(
                    "ERROR DE VALIDACIÓN - Tabla: {Tabla}, Error: {Mensaje}",
                    tabla, excepcionArgumento.Message
                );
                return BadRequest(new
                {
                    estado = 400,
                    mensaje = "Parámetros de entrada inválidos.",
                    detalle = excepcionArgumento.Message,
                    tabla = tabla
                });
            }
            catch (UnauthorizedAccessException excepcionAcceso)
            {
                _logger.LogWarning(
                    "ACCESO DENEGADO - Tabla restringida: {Tabla}, Error: {Mensaje}",
                    tabla, excepcionAcceso.Message
                );
                return StatusCode(403, new
                {
                    estado = 403,
                    mensaje = "Acceso denegado.",
                    detalle = excepcionAcceso.Message,
                    tabla = tabla
                });
            }
            catch (InvalidOperationException excepcionOperacion)
            {
                _logger.LogError(excepcionOperacion,
                    "ERROR DE OPERACIÓN - Tabla: {Tabla}, Error: {Mensaje}",
                    tabla, excepcionOperacion.Message
                );
                return NotFound(new
                {
                    estado = 404,
                    mensaje = "El recurso solicitado no fue encontrado.",
                    detalle = excepcionOperacion.Message,
                    tabla = tabla,
                    sugerencia = "Verifique que la tabla y el esquema existan en la base de datos"
                });
            }
            catch (Exception excepcionGeneral)
            {
                _logger.LogError(excepcionGeneral,
                    "ERROR CRÍTICO - Tabla: {Tabla}", tabla
                );
                var detalleError = new System.Text.StringBuilder();
                detalleError.AppendLine($"Tipo: {excepcionGeneral.GetType().Name}");
                detalleError.AppendLine($"Mensaje: {excepcionGeneral.Message}");
                if (excepcionGeneral.InnerException != null)
                    detalleError.AppendLine($"Error interno: {excepcionGeneral.InnerException.Message}");

                return StatusCode(500, new
                {
                    estado = 500,
                    mensaje = "Error interno del servidor al consultar tabla.",
                    tabla = tabla,
                    tipoError = excepcionGeneral.GetType().Name,
                    detalle = excepcionGeneral.Message,
                    detalleCompleto = detalleError.ToString(),
                    errorInterno = excepcionGeneral.InnerException?.Message,
                    timestamp = DateTime.UtcNow,
                    sugerencia = "Revise los logs del servidor para más detalles."
                });
            }
        }

        // =====================================================================
        // GET /api/{tabla}/{nombreClave}/{valor}?esquema={esquema}
        // Obtener un registro por clave (ej: /api/car_innovacion/id/1)
        // =====================================================================
        [AllowAnonymous]
        [HttpGet("{nombreClave}/{valor}")]
        public async Task<IActionResult> ObtenerPorClaveAsync(
            string tabla,
            string nombreClave,
            string valor,
            [FromQuery] string? esquema = null)
        {
            try
            {
                _logger.LogInformation(
                    "INICIO filtrado - Tabla: {Tabla}, Esquema: {Esquema}, Clave: {Clave}, Valor: {Valor}",
                    tabla, esquema ?? "por defecto", nombreClave, valor
                );

                var filas = await _servicioCrud.ObtenerPorClaveAsync(tabla, esquema, nombreClave, valor);

                _logger.LogInformation(
                    "RESULTADO filtrado - {Cantidad} registros para {Clave}={Valor} en {Tabla}",
                    filas.Count, nombreClave, valor, tabla
                );

                if (filas.Count == 0)
                {
                    return NotFound(new
                    {
                        estado = 404,
                        mensaje = "No se encontraron registros",
                        detalle = $"No se encontró ningún registro con {nombreClave} = {valor} en la tabla {tabla}",
                        tabla = tabla,
                        esquema = esquema ?? "por defecto",
                        filtro = $"{nombreClave} = {valor}"
                    });
                }

                return Ok(new
                {
                    tabla = tabla,
                    esquema = esquema ?? "por defecto",
                    filtro = $"{nombreClave} = {valor}",
                    total = filas.Count,
                    datos = filas
                });
            }
            catch (UnauthorizedAccessException excepcionAcceso)
            {
                return StatusCode(403, new
                {
                    estado = 403,
                    mensaje = "Acceso denegado.",
                    detalle = excepcionAcceso.Message,
                    tabla = tabla
                });
            }
            catch (ArgumentException excepcionArgumento)
            {
                return BadRequest(new
                {
                    estado = 400,
                    mensaje = "Parámetros inválidos.",
                    detalle = excepcionArgumento.Message,
                    tabla = tabla
                });
            }
            catch (InvalidOperationException excepcionOperacion)
            {
                return NotFound(new
                {
                    estado = 404,
                    mensaje = "Recurso no encontrado.",
                    detalle = excepcionOperacion.Message,
                    tabla = tabla
                });
            }
            catch (Exception excepcionGeneral)
            {
                _logger.LogError(excepcionGeneral,
                    "ERROR CRÍTICO - Tabla: {Tabla}, Clave: {Clave}, Valor: {Valor}",
                    tabla, nombreClave, valor
                );
                return StatusCode(500, new
                {
                    estado = 500,
                    mensaje = "Error interno del servidor al filtrar registros.",
                    tabla = tabla,
                    filtro = $"{nombreClave} = {valor}",
                    tipoError = excepcionGeneral.GetType().Name,
                    detalle = excepcionGeneral.Message,
                    errorInterno = excepcionGeneral.InnerException?.Message,
                    timestamp = DateTime.UtcNow
                });
            }
        }

        // =====================================================================
        // POST /api/{tabla}?esquema={esquema}&camposEncriptar={campos}
        // Crear un nuevo registro
        // Body JSON: { "id": 1, "nombre": "Innovación Abierta", ... }
        // =====================================================================
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CrearAsync(
            string tabla,
            [FromBody] System.Collections.Generic.Dictionary<string, object?> datosEntidad,
            [FromQuery] string? esquema = null,
            [FromQuery] string? camposEncriptar = null)
        {
            try
            {
                _logger.LogInformation(
                    "INICIO creación - Tabla: {Tabla}, Esquema: {Esquema}, Campos a encriptar: {CamposEncriptar}",
                    tabla, esquema ?? "por defecto", camposEncriptar ?? "ninguno"
                );

                if (datosEntidad == null || !datosEntidad.Any())
                {
                    return BadRequest(new
                    {
                        estado = 400,
                        mensaje = "Los datos de la entidad no pueden estar vacíos.",
                        tabla = tabla
                    });
                }

                // Conversión de JsonElement a tipos nativos
                var datosConvertidos = new System.Collections.Generic.Dictionary<string, object?>();
                foreach (var kvp in datosEntidad)
                {
                    if (kvp.Value is JsonElement elemento)
                        datosConvertidos[kvp.Key] = ConvertirJsonElement(elemento);
                    else
                        datosConvertidos[kvp.Key] = kvp.Value;
                }

                bool creado = await _servicioCrud.CrearAsync(tabla, esquema, datosConvertidos, camposEncriptar);

                if (creado)
                {
                    _logger.LogInformation("ÉXITO creación - Registro creado en tabla {Tabla}", tabla);
                    return Ok(new
                    {
                        estado = 200,
                        mensaje = "Registro creado exitosamente.",
                        tabla = tabla,
                        esquema = esquema ?? "por defecto"
                    });
                }
                else
                {
                    return StatusCode(500, new
                    {
                        estado = 500,
                        mensaje = "No se pudo crear el registro.",
                        tabla = tabla
                    });
                }
            }
            catch (UnauthorizedAccessException excepcionAcceso)
            {
                return StatusCode(403, new
                {
                    estado = 403,
                    mensaje = "Acceso denegado.",
                    detalle = excepcionAcceso.Message,
                    tabla = tabla
                });
            }
            catch (ArgumentException excepcionArgumento)
            {
                return BadRequest(new
                {
                    estado = 400,
                    mensaje = "Datos inválidos.",
                    detalle = excepcionArgumento.Message,
                    tabla = tabla
                });
            }
            catch (InvalidOperationException excepcionOperacion)
            {
                return StatusCode(500, new
                {
                    estado = 500,
                    mensaje = "Error en la operación.",
                    detalle = excepcionOperacion.Message,
                    tabla = tabla
                });
            }
            catch (Exception excepcionGeneral)
            {
                _logger.LogError(excepcionGeneral, "ERROR CRÍTICO - Tabla: {Tabla}", tabla);
                return StatusCode(500, new
                {
                    estado = 500,
                    mensaje = "Error interno del servidor al crear registro.",
                    tabla = tabla,
                    tipoError = excepcionGeneral.GetType().Name,
                    detalle = excepcionGeneral.Message,
                    errorInterno = excepcionGeneral.InnerException?.Message,
                    timestamp = DateTime.UtcNow
                });
            }
        }

        // =====================================================================
        // PUT /api/{tabla}/{nombreClave}/{valorClave}?esquema={esquema}&camposEncriptar={campos}
        // Actualizar un registro existente
        // Body JSON: { "nombre": "Nuevo nombre", "tipo": "Nuevo tipo" }
        // =====================================================================
        [AllowAnonymous]
        [HttpPut("{nombreClave}/{valorClave}")]
        public async Task<IActionResult> ActualizarAsync(
            string tabla,
            string nombreClave,
            string valorClave,
            [FromBody] System.Collections.Generic.Dictionary<string, object?> datosEntidad,
            [FromQuery] string? esquema = null,
            [FromQuery] string? camposEncriptar = null)
        {
            try
            {
                _logger.LogInformation(
                    "INICIO actualización - Tabla: {Tabla}, Clave: {Clave}={Valor}",
                    tabla, nombreClave, valorClave
                );

                if (datosEntidad == null || !datosEntidad.Any())
                {
                    return BadRequest(new
                    {
                        estado = 400,
                        mensaje = "Los datos de actualización no pueden estar vacíos.",
                        tabla = tabla,
                        filtro = $"{nombreClave} = {valorClave}"
                    });
                }

                var datosConvertidos = new System.Collections.Generic.Dictionary<string, object?>();
                foreach (var kvp in datosEntidad)
                {
                    if (kvp.Value is JsonElement elemento)
                        datosConvertidos[kvp.Key] = ConvertirJsonElement(elemento);
                    else
                        datosConvertidos[kvp.Key] = kvp.Value;
                }

                int filasAfectadas = await _servicioCrud.ActualizarAsync(
                    tabla, esquema, nombreClave, valorClave, datosConvertidos, camposEncriptar);

                if (filasAfectadas > 0)
                {
                    _logger.LogInformation(
                        "ÉXITO actualización - {Filas} fila(s) actualizada(s) en tabla {Tabla}",
                        filasAfectadas, tabla
                    );
                    return Ok(new
                    {
                        estado = 200,
                        mensaje = "Registro actualizado exitosamente.",
                        tabla = tabla,
                        filtro = $"{nombreClave} = {valorClave}",
                        filasAfectadas = filasAfectadas
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        estado = 404,
                        mensaje = "No se encontró el registro a actualizar.",
                        tabla = tabla,
                        filtro = $"{nombreClave} = {valorClave}"
                    });
                }
            }
            catch (UnauthorizedAccessException excepcionAcceso)
            {
                return StatusCode(403, new
                {
                    estado = 403,
                    mensaje = "Acceso denegado.",
                    detalle = excepcionAcceso.Message,
                    tabla = tabla
                });
            }
            catch (ArgumentException excepcionArgumento)
            {
                return BadRequest(new
                {
                    estado = 400,
                    mensaje = "Datos inválidos.",
                    detalle = excepcionArgumento.Message,
                    tabla = tabla
                });
            }
            catch (InvalidOperationException excepcionOperacion)
            {
                return StatusCode(500, new
                {
                    estado = 500,
                    mensaje = "Error en la operación.",
                    detalle = excepcionOperacion.Message,
                    tabla = tabla
                });
            }
            catch (Exception excepcionGeneral)
            {
                _logger.LogError(excepcionGeneral,
                    "ERROR CRÍTICO - Tabla: {Tabla}, Clave: {Clave}={Valor}",
                    tabla, nombreClave, valorClave
                );
                return StatusCode(500, new
                {
                    estado = 500,
                    mensaje = "Error interno del servidor al actualizar registro.",
                    tabla = tabla,
                    tipoError = excepcionGeneral.GetType().Name,
                    detalle = excepcionGeneral.Message,
                    errorInterno = excepcionGeneral.InnerException?.Message,
                    timestamp = DateTime.UtcNow
                });
            }
        }

        // =====================================================================
        // DELETE /api/{tabla}/{nombreClave}/{valorClave}?esquema={esquema}
        // Eliminar un registro
        // =====================================================================
        [AllowAnonymous]
        [HttpDelete("{nombreClave}/{valorClave}")]
        public async Task<IActionResult> EliminarAsync(
            string tabla,
            string nombreClave,
            string valorClave,
            [FromQuery] string? esquema = null)
        {
            try
            {
                _logger.LogInformation(
                    "INICIO eliminación - Tabla: {Tabla}, Clave: {Clave}={Valor}",
                    tabla, nombreClave, valorClave
                );

                int filasAfectadas = await _servicioCrud.EliminarAsync(
                    tabla, esquema, nombreClave, valorClave);

                if (filasAfectadas > 0)
                {
                    _logger.LogInformation(
                        "ÉXITO eliminación - {Filas} fila(s) eliminada(s) en tabla {Tabla}",
                        filasAfectadas, tabla
                    );
                    return Ok(new
                    {
                        estado = 200,
                        mensaje = "Registro eliminado exitosamente.",
                        tabla = tabla,
                        filtro = $"{nombreClave} = {valorClave}",
                        filasAfectadas = filasAfectadas
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        estado = 404,
                        mensaje = "No se encontró el registro a eliminar.",
                        tabla = tabla,
                        filtro = $"{nombreClave} = {valorClave}"
                    });
                }
            }
            catch (UnauthorizedAccessException excepcionAcceso)
            {
                return StatusCode(403, new
                {
                    estado = 403,
                    mensaje = "Acceso denegado.",
                    detalle = excepcionAcceso.Message,
                    tabla = tabla
                });
            }
            catch (ArgumentException excepcionArgumento)
            {
                return BadRequest(new
                {
                    estado = 400,
                    mensaje = "Parámetros inválidos.",
                    detalle = excepcionArgumento.Message,
                    tabla = tabla
                });
            }
            catch (InvalidOperationException excepcionOperacion)
            {
                return StatusCode(500, new
                {
                    estado = 500,
                    mensaje = "Error en la operación.",
                    detalle = excepcionOperacion.Message,
                    tabla = tabla
                });
            }
            catch (Exception excepcionGeneral)
            {
                _logger.LogError(excepcionGeneral,
                    "ERROR CRÍTICO - Tabla: {Tabla}, Clave: {Clave}={Valor}",
                    tabla, nombreClave, valorClave
                );
                return StatusCode(500, new
                {
                    estado = 500,
                    mensaje = "Error interno del servidor al eliminar registro.",
                    tabla = tabla,
                    tipoError = excepcionGeneral.GetType().Name,
                    detalle = excepcionGeneral.Message,
                    errorInterno = excepcionGeneral.InnerException?.Message,
                    timestamp = DateTime.UtcNow
                });
            }
        }

        // =====================================================================
        // Método auxiliar para convertir JsonElement a tipos nativos
        // =====================================================================
        private static object? ConvertirJsonElement(JsonElement elemento)
        {
            return elemento.ValueKind switch
            {
                JsonValueKind.String => elemento.GetString(),
                JsonValueKind.Number when elemento.TryGetInt32(out int entero) => entero,
                JsonValueKind.Number when elemento.TryGetInt64(out long largo) => largo,
                JsonValueKind.Number when elemento.TryGetDouble(out double doble) => doble,
                JsonValueKind.True => true,
                JsonValueKind.False => false,
                JsonValueKind.Null => null,
                _ => elemento.ToString()
            };
        }
    }
}
