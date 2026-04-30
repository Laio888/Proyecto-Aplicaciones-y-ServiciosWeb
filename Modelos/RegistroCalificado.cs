namespace ApiKnowledgeMap.Modelos
{
    public class RegistroCalificado
    {
        public int Codigo { get; set; }
        public string CantCreditos { get; set; } = string.Empty;
        public string HoraAcom { get; set; } = string.Empty;
        public string HoraInd { get; set; } = string.Empty;
        public string Metodologia { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string DuracionAnios { get; set; } = string.Empty;
        public string DuracionSemestres { get; set; } = string.Empty;
        public string TipoTitulacion { get; set; } = string.Empty;
        public int Programa { get; set; }
    }
}