using System.Reflection.Metadata.Ecma335;

namespace AgendaApp.API.Models
{
    public class CriarTarefaResponseModel
    {
        public Guid? Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public DateTime? DataHora { get; set; }
        public int? Prioridade { get; set; }
        public DateTime? DataHoraCadastro { get; set; }
        public int? Status { get; set; }
    }
}
