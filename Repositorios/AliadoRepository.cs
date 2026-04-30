using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios.Abstracciones;
using Dapper;

namespace ApiKnowledgeMap.Repositorios
{
    public class AliadoRepository : IAliadoRepository
    {
        private readonly IProveedorConexion _conexion;

        public AliadoRepository(IProveedorConexion conexion)
        {
            _conexion = conexion;
        }

        public async Task<IEnumerable<Aliado>> ObtenerTodosAsync()
        {
            using var conn = _conexion.ObtenerConexion();

            return await conn.QueryAsync<Aliado>(
                @"SELECT
                    nit AS Nit,
                    razon_social AS RazonSocial,
                    nombre_contacto AS NombreContacto,
                    correo AS Correo,
                    telefono AS Telefono,
                    ciudad AS Ciudad
                  FROM aliado");
        }

        public async Task<Aliado?> ObtenerPorIdAsync(int nit)
        {
            using var conn = _conexion.ObtenerConexion();

            return await conn.QueryFirstOrDefaultAsync<Aliado>(
                @"SELECT
                    nit AS Nit,
                    razon_social AS RazonSocial,
                    nombre_contacto AS NombreContacto,
                    correo AS Correo,
                    telefono AS Telefono,
                    ciudad AS Ciudad
                  FROM aliado
                  WHERE nit = @Nit",
                new { Nit = nit });
        }

        public async Task<bool> InsertarAsync(Aliado aliado)
        {
            using var conn = _conexion.ObtenerConexion();

            var filas = await conn.ExecuteAsync(
                @"INSERT INTO aliado
                    (nit, razon_social, nombre_contacto, correo, telefono, ciudad)
                  VALUES
                    (@Nit, @RazonSocial, @NombreContacto, @Correo, @Telefono, @Ciudad)",
                aliado);

            return filas > 0;
        }

        public async Task<bool> ActualizarAsync(int nit, Aliado aliado)
        {
            using var conn = _conexion.ObtenerConexion();

            var filas = await conn.ExecuteAsync(
                @"UPDATE aliado
                  SET razon_social = @RazonSocial,
                      nombre_contacto = @NombreContacto,
                      correo = @Correo,
                      telefono = @Telefono,
                      ciudad = @Ciudad
                  WHERE nit = @Nit",
                new
                {
                    Nit = nit,
                    aliado.RazonSocial,
                    aliado.NombreContacto,
                    aliado.Correo,
                    aliado.Telefono,
                    aliado.Ciudad
                });

            return filas > 0;
        }

        public async Task<bool> EliminarAsync(int nit)
        {
            using var conn = _conexion.ObtenerConexion();

            var filas = await conn.ExecuteAsync(
                @"DELETE FROM aliado
                  WHERE nit = @Nit",
                new { Nit = nit });

            return filas > 0;
        }
    }
}