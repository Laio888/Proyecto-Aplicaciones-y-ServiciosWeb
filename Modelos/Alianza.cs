namespace ApiKnowledgeMap.Modelos
{
    public class Alianza
    {
        public int Aliado { get; set; }
        public int Departamento { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int Docente { get; set; }
    }
}