namespace ApiKnowledgeMap.Modelos
{
    public class Docente
    {
        public int Cedula { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? Genero { get; set; }
        public string? Cargo { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? Correo { get; set; }
        public string? Telefono { get; set; }
        public string? UrlCvlac { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public string? Escalafon { get; set; }
        public string? Perfil { get; set; }
        public string? CatMinciencia { get; set; }
        public string? ConvMinciencia { get; set; }
        public string? Nacionalidaad { get; set; }
        public int? LineaInvestigacionPrincipal { get; set; }
    }
}