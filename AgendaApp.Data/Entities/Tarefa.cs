using AgendaApp.Data.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaApp.Data.Entities
{
    /// <summary>
    /// Modelo de entidade para Tarefa
    /// </summary>
    public class Tarefa
    {
        #region Propriedades

        public Guid? Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public DateTime? DataHora { get; set; }
        public PrioridadeTarefa? Prioridade { get; set; }
        public DateTime? DataHoraCadastro { get; set; }
        public DateTime? DataHoraUltimaAtualizacao { get; set; }
        public int? Status { get; set; }

        #endregion
    }
}
