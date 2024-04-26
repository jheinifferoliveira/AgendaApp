using AgendaApp.API.Models;
using AgendaApp.Data.Entities;
using AgendaApp.Data.Entities.Enums;
using AgendaApp.Data.Repositories;
using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Formats.Tar;

namespace AgendaApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefasController : ControllerBase

    { 
        private readonly IMapper _mapper;

        public TarefasController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CriarTarefaResponseModel),201)]
        public IActionResult Post(CriarTarefaRequestModel model)
        {
            try
            {
                var tarefa = _mapper.Map<Tarefa>(model);

                tarefa.Id = Guid.NewGuid();
                tarefa.DataHoraCadastro = DateTime.Now;
                tarefa.DataHoraUltimaAtualizacao = DateTime.Now;
                tarefa.Status = 1;

                var tarefaRepository = new TarefaRepository();
                tarefaRepository.Add(tarefa);

                var response = _mapper.Map<CriarTarefaResponseModel>(tarefa);

                return StatusCode(201,response);
            }
            catch(Exception e) 
            {
                return StatusCode(500, new { e.Message });
            }
            
        }

        [HttpPut]
        [ProducesResponseType(typeof(EditarTarefaResponseModel),200)]
        public IActionResult Put(EditarTarefaRequestModel model)
        {
            try
            {
                var tarefaRepository = new TarefaRepository();
                var tarefa = tarefaRepository.GetById(model.Id.Value);

                if (tarefa != null)
                {
                    tarefa.Nome = model.Nome;
                    tarefa.Descricao = model.Descricao;
                    tarefa.DataHora = model.DataHora;
                    tarefa.Prioridade = (PrioridadeTarefa)model.Prioridade;
                    tarefa.DataHoraUltimaAtualizacao = DateTime.Now;

                    //atualizar no banco de dados
                    tarefaRepository.Update(tarefa);

                    var response = _mapper.Map<EditarTarefaResponseModel>(tarefa);

                    return StatusCode(200, response);

                }
                else
                {
                    return StatusCode(400, new { message = " O ID DA TAREFA É INVÁLIDO. " });
                }

            }
            catch(Exception e)
            {
                return StatusCode(500, new { e.Message });
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ExcluirTarefaResponseModel),200)]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var tarefaRepository = new TarefaRepository();
                var tarefa = tarefaRepository.GetById(id);

                if(tarefa != null)
                {
                    tarefaRepository.Delete(tarefa);

                    var response = _mapper.Map<ExcluirTarefaResponseModel>(tarefa);
                    response.DataHoraExclusao = DateTime.Now;

                    return StatusCode(200, response);
                }
                else
                {
                    return StatusCode(400, new { message = "O ID da tarefa é inválido." });
                }

            }
               
            catch(Exception e)
            {
                return StatusCode(500, new { e.Message });
            }
        }

        [HttpGet("{dataInicio}/{dataFim}")]
        [ProducesResponseType(typeof(List<ConsultarTarefaResponseModel>),200)]
        public IActionResult Get(DateTime dataInicio, DateTime dataFim)
        {
            try
            {
                var tarefaRepository = new TarefaRepository();
                var tarefas = tarefaRepository.Get(dataInicio, dataFim);

                var response = _mapper.Map<List<ConsultarTarefaResponseModel>>(tarefas);

                return StatusCode(200, response);
            }
            catch(Exception e)
            {
                return StatusCode(500, new { e.Message });
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ConsultarTarefaResponseModel),200)]
        public IActionResult GetById(Guid id )
        {
            try
            {
                var tarefaRepository = new TarefaRepository();
                var tarefa = tarefaRepository.GetById(id);

                if(tarefa != null)
                {
                    var response = _mapper.Map<ConsultarTarefaResponseModel>(tarefa);

                    return StatusCode(200, response);
                }
                else
                {
                    return StatusCode(204);
                }

            }
            catch(Exception e)
            {
                return StatusCode(500, new { e.Message });
            }
        }



    }
}
