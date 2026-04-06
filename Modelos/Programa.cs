using System.ComponentModel.DataAnnotations.Schema;

namespace ApiKnowledgeMap.Modelos
{
    public class Programa {
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public string Nivel { get; set; } = string.Empty;
    public string FechaCreacion { get; set; }
    public string FechaCierre { get; set; }
    public string NumeroCohortes { get; set; } = string.Empty;
    public string CantGraduados { get; set; } = string.Empty;
    public string FechaActualizacion { get; set; }
    public string Ciudad { get; set; } = string.Empty;

    public int FacultadId{get; set;}

    [ForeignKey("Facultad")] public Facultad? Facultad {get; set;}
    
    }
}