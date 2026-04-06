using System.ComponentModel.DataAnnotations.Schema;

namespace ApiKnowledgeMap.Modelos
{
    public class ActivAcademica {
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int NumCreditos { get; set; } 
    public string Tipo { get; set; } = string.Empty;
    public string AreaFormacion { get; set; } = string.Empty;
    public int H_Acom { get; set; } 
    public int H_Indep { get; set; } 
    public string Idioma { get; set; } = string.Empty;

    public bool Espejo{get; set;}
    public string EntidadEspejo {get; set;}
    public string PaisEspejo {get; set;}

    public int Disenio {get; set;}

    [ForeignKey("Disenio")] public Programa? _Programa{get;set;}
    }
}