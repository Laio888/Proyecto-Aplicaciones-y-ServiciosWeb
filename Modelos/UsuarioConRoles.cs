namespace ApiKnowledgeMap.Modelos
{
    public class UsuarioConRoles
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public string Email { get; set; } = "";
        public List<string> Roles { get; set; } = new();
    }
}