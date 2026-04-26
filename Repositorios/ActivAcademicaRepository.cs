using Dapper;
using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios.Abstracciones;

namespace ApiKnowledgeMap.Repositorios
{
    public class ActivAcademicaRepository : IActivAcademicaRepository
    {
        private readonly IProveedorConexion _proveedorConexion;

        public ActivAcademicaRepository(IProveedorConexion proveedorConexion)
        {
            _proveedorConexion = proveedorConexion;
        }

        public async Task<IEnumerable<ActivAcademica>> ObtenerTodosAsync()
        {
            using var conn = _proveedorConexion.ObtenerConexion();

            return await conn.QueryAsync<ActivAcademica>(
                @"SELECT 
                    id AS Id,
                    nombre AS Nombre,
                    num_creditos AS NumCreditos,
                    tipo AS Tipo,
                    area_formacion AS AreaFormacion,
                    h_acom AS H_Acom,
                    h_indep AS H_Indep,
                    espejo AS Espejo,
                    entidad_espejo AS EntidadEspejo,
                    pais_espejo AS PaisEspejo,
                    disenio AS Disenio
                  FROM activ_academica");
        }

        public async Task<ActivAcademica?> ObtenerPorIdAsync(int id)
        {
            using var conn = _proveedorConexion.ObtenerConexion();

            return await conn.QueryFirstOrDefaultAsync<ActivAcademica>(
                @"SELECT 
                    id AS Id,
                    nombre AS Nombre,
                    num_creditos AS NumCreditos,
                    tipo AS Tipo,
                    area_formacion AS AreaFormacion,
                    h_acom AS H_Acom,
                    h_indep AS H_Indep,
                    idioma AS Idioma,
                    espejo AS Espejo,
                    entidad_espejo AS EntidadEspejo,
                    pais_espejo AS PaisEspejo,
                    disenio AS Disenio
                  FROM activ_academica
                  WHERE id = @Id",
                new { Id = id });
        }

        public async Task<int> InsertarAsync(ActivAcademica activAcademica)
        {
            using var conn = _proveedorConexion.ObtenerConexion();

            return await conn.ExecuteScalarAsync<int>(
                @"INSERT INTO activ_academica (id,nombre,num_creditos, tipo, area_formacion,h_acom,h_indep,idioma,espejo,entidad_espejo,pais_espejo,disenio)
                  VALUES (@Id,@Nombre,@NumCreditos,@Tipo,@AreaFormacion,@H_Acom,@H_Indep,@Idioma,@Espejo,@EntidadEspejo,@PaisEspejo,@Disenio);
                  SELECT @Id;",
                activAcademica);
        }

        public async Task<bool> ActualizarAsync(ActivAcademica activAcademica)
        {
            using var conn = _proveedorConexion.ObtenerConexion();

            var filas = await conn.ExecuteAsync(
                @"UPDATE activ_academica
                  SET nombre = @Nombre,
                      num_creditos = @NumCreditos,
                      tipo = @Tipo,
                      area_formacion = @AreaFormacion,
                      h_acom = @H_Acom,
                      h_indep = @H_Indep,
                      idioma = @Idioma,
                      espejo = @Espejo,
                      entidad_espejo = @EntidadEspejo,
                      pais_espejo = @PaisEspejo,
                      disenio = @Disenio
                  WHERE id = @Id",
                activAcademica);

            return filas > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            using var conn = _proveedorConexion.ObtenerConexion();

            var filas = await conn.ExecuteAsync(
                "DELETE FROM activ_academica WHERE id = @Id",
                new { Id = id });

            return filas > 0;
        }
    }
}