namespace ApiKnowledgeMap.Modelos
{
    public class Premio
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateOnly Fecha { get; set; }
        public string EntidadOtorgante { get; set; } = string.Empty;
        public string Pais { get; set; } = string.Empty;
        public int Programa { get; set; }
    }
}