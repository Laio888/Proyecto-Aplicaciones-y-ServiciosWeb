using System.ComponentModel.DataAnnotations.Schema;

namespace ApiKnowledgeMap.Modelos
{
    public class AnPrograma {
    public int AspectoNormativo { get; set; }
    public int Programa { get; set; }

    [ForeignKey("AspectoNormativo")] public AspectoNormativo? _AspectoNormativo{get; set;}
    [ForeignKey("Programa")] public Programa? _Programa{get; set;}
    }
}