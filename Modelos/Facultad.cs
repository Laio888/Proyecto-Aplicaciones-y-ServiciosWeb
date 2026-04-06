using System.ComponentModel.DataAnnotations.Schema;

namespace ApiKnowledgeMap.Modelos
{
    public class Facultad {
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public DateTime FechaFun { get; set; }
    public int UniversidadId { get; set; }

    [ForeignKey("UniversidadId")] public Universidad? Universidad {get; set;}
    }
}