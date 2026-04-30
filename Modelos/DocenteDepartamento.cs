namespace ApiKnowledgeMap.Modelos
{
    public class DocenteDepartamento
    {
        public int Docente { get; set; }
        public int Departamento { get; set; }
        public string? Dedicacion { get; set; }
        public string? Modalidad { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public DateTime? FechaSalida { get; set; }
    }
}