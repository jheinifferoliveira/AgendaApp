namespace AgendaApp.API.Models
{
    /// <summary>
    /// Modelo de dados da reposta de consulta de tarefas.
    /// </summary>
    public class ConsultarTarefaResponseModel
    {
        public Guid? Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public DateTime? DataHora { get; set; }
        public int? Prioridade { get; set; }
        public DateTime? DataHoraCadastro { get; set; }
        public DateTime? DataHoraUltimaAtualizacao { get; set; }
        public int? Status { get; set; }

    }
}
