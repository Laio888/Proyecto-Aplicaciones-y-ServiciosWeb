using System.ComponentModel.DataAnnotations.Schema;

namespace ApiKnowledgeMap.Modelos
{
    public class Acreditacion {
    public int Resolucion { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public string Calificacion { get; set; } = string.Empty;
    public string FechaInicio { get; set; } = string.Empty;
    public string FechaFin { get; set; } = string.Empty;
    public int Programa { get; set; } 

    [ForeignKey("Programa")] public Programa? _Programa {get; set;}
    }
}