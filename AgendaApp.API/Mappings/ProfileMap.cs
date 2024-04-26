using AgendaApp.API.Models;
using AgendaApp.Data.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AgendaApp.API.Mappings
{
    /// <summary>
    /// Classe para configurar os mapeamentos através do AutoMapper
    /// </summary>
    public class ProfileMap:Profile
    {
        public ProfileMap()
        {
            CreateMap<CriarTarefaRequestModel, Tarefa>();

            CreateMap<Tarefa, CriarTarefaResponseModel>();

            CreateMap<Tarefa, EditarTarefaResponseModel>();

            CreateMap<Tarefa, ExcluirTarefaResponseModel>();

            CreateMap<Tarefa, ConsultarTarefaResponseModel>();
        }
    }
}
