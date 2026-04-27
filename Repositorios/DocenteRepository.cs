using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios.Abstracciones;
using Dapper;

namespace ApiKnowledgeMap.Repositorios
{
    public class DocenteRepository : IDocenteRepository
    {
        private readonly IProveedorConexion _conexion;

        public DocenteRepository(IProveedorConexion conexion)
        {
            _conexion = conexion;
        }

        public async Task<IEnumerable<Docente>> ObtenerTodosAsync()
        {
            using var conn = _conexion.ObtenerConexion();

            return await conn.QueryAsync<Docente>(
                @"SELECT
                    cedula AS Cedula,
                    nombres AS Nombres,
                    apellidos AS Apellidos,
                    genero AS Genero,
                    cargo AS Cargo,
                    fecha_nacimiento AS FechaNacimiento,
                    correo AS Correo,
                    telefono AS Telefono,
                    url_cvlac AS UrlCvlac,
                    fecha_actualizacion AS FechaActualizacion,
                    escalafon AS Escalafon,
                    perfil AS Perfil,
                    cat_minciencia AS CatMinciencia,
                    conv_minciencia AS ConvMinciencia,
                    nacionalidaad AS Nacionalidaad,
                    linea_investigacion_principal AS LineaInvestigacionPrincipal
                  FROM docente");
        }

        public async Task<Docente?> ObtenerPorIdAsync(int cedula)
        {
            using var conn = _conexion.ObtenerConexion();

            return await conn.QueryFirstOrDefaultAsync<Docente>(
                @"SELECT
                    cedula AS Cedula,
                    nombres AS Nombres,
                    apellidos AS Apellidos,
                    genero AS Genero,
                    cargo AS Cargo,
                    fecha_nacimiento AS FechaNacimiento,
                    correo AS Correo,
                    telefono AS Telefono,
                    url_cvlac AS UrlCvlac,
                    fecha_actualizacion AS FechaActualizacion,
                    escalafon AS Escalafon,
                    perfil AS Perfil,
                    cat_minciencia AS CatMinciencia,
                    conv_minciencia AS ConvMinciencia,
                    nacionalidaad AS Nacionalidaad,
                    linea_investigacion_principal AS LineaInvestigacionPrincipal
                  FROM docente
                  WHERE cedula = @Cedula",
                new { Cedula = cedula });
        }

        public async Task<bool> InsertarAsync(Docente docente)
        {
            using var conn = _conexion.ObtenerConexion();

            var filas = await conn.ExecuteAsync(
                @"INSERT INTO docente
                    (cedula, nombres, apellidos, genero, cargo, fecha_nacimiento, correo,
                     telefono, url_cvlac, fecha_actualizacion, escalafon, perfil,
                     cat_minciencia, conv_minciencia, nacionalidaad, linea_investigacion_principal)
                  VALUES
                    (@Cedula, @Nombres, @Apellidos, @Genero, @Cargo, @FechaNacimiento, @Correo,
                     @Telefono, @UrlCvlac, @FechaActualizacion, @Escalafon, @Perfil,
                     @CatMinciencia, @ConvMinciencia, @Nacionalidaad, @LineaInvestigacionPrincipal)",
                docente);

            return filas > 0;
        }

        public async Task<bool> ActualizarAsync(int cedula, Docente docente)
        {
            using var conn = _conexion.ObtenerConexion();

            var filas = await conn.ExecuteAsync(
                @"UPDATE docente
                  SET nombres = @Nombres,
                      apellidos = @Apellidos,
                      genero = @Genero,
                      cargo = @Cargo,
                      fecha_nacimiento = @FechaNacimiento,
                      correo = @Correo,
                      telefono = @Telefono,
                      url_cvlac = @UrlCvlac,
                      fecha_actualizacion = @FechaActualizacion,
                      escalafon = @Escalafon,
                      perfil = @Perfil,
                      cat_minciencia = @CatMinciencia,
                      conv_minciencia = @ConvMinciencia,
                      nacionalidaad = @Nacionalidaad,
                      linea_investigacion_principal = @LineaInvestigacionPrincipal
                  WHERE cedula = @Cedula",
                new
                {
                    Cedula = cedula,
                    docente.Nombres,
                    docente.Apellidos,
                    docente.Genero,
                    docente.Cargo,
                    docente.FechaNacimiento,
                    docente.Correo,
                    docente.Telefono,
                    docente.UrlCvlac,
                    docente.FechaActualizacion,
                    docente.Escalafon,
                    docente.Perfil,
                    docente.CatMinciencia,
                    docente.ConvMinciencia,
                    docente.Nacionalidaad,
                    docente.LineaInvestigacionPrincipal
                });

            return filas > 0;
        }

        public async Task<bool> EliminarAsync(int cedula)
        {
            using var conn = _conexion.ObtenerConexion();

            var filas = await conn.ExecuteAsync(
                @"DELETE FROM docente
                  WHERE cedula = @Cedula",
                new { Cedula = cedula });

            return filas > 0;
        }
    }
}