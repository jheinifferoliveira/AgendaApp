using AgendaApp.API.Models;
using Bogus;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AgendaApp.Tests
{
    public class TarefasTest
    {
        [Fact]
        public async Task<CriarTarefaResponseModel> CadastrarTarefas_Test()
        {
            #region Gerar os dados do teste

            var faker = new Faker("pt_BR");
            var request = new CriarTarefaRequestModel
            {
                Nome = faker.Lorem.Sentences(1),
                Descricao=faker.Lorem.Sentences(1),
                DataHora=faker.Date.Between(DateTime.Now.AddDays(-7),DateTime.Now.AddDays(7)),
                Prioridade=faker.Random.Int(1,3)
            };

            var jsonRequest = new StringContent(JsonConvert.SerializeObject(request),
                                 Encoding.UTF8, "application/json");
            #endregion

            #region Enviando a requisição POST para API

            var client = new WebApplicationFactory<Program>().CreateClient();
            var result = await client.PostAsync("/api/tarefas", jsonRequest);

            #endregion

            #region Verificar o Resultado

            result.StatusCode.Should().Be(HttpStatusCode.Created);

            var jsonResult = result.Content.ReadAsStringAsync().Result;
            var response = JsonConvert.DeserializeObject<CriarTarefaResponseModel>(jsonResult);

            response?.Id.Should().NotBeEmpty();
            response?.Nome.Should().Be(request.Nome);
            response?.Descricao.Should().Be(request.Descricao);
            response?.DataHora.Should().Be(request.DataHora);
            response?.Prioridade.Should().Be(request.Prioridade);
            response?.DataHoraCadastro.Should().NotBeNull();
            response?.Status.Should().Be(1);

            #endregion

            return response;

        }

        [Fact]
        public async Task AtualizarTarefas_Test()
        {
            #region Gerar os dados do teste

            var tarefa = await CadastrarTarefas_Test();

            var faker = new Faker("pt_BR");

            var request = new EditarTarefaRequestModel
            {
                Id = tarefa.Id,
                Nome = faker.Lorem.Sentences(1),
                Descricao = faker.Lorem.Sentences(1),
                DataHora = faker.Date.Between(DateTime.Now.AddDays(-7), DateTime.Now.AddDays(7)),
                Prioridade = faker.Random.Int(1, 3)
            };

            var jsonRequest = new StringContent(JsonConvert.SerializeObject(request),
                                 Encoding.UTF8, "application/json");
            #endregion

            #region Enviando a requisição PUT para API

            var client = new WebApplicationFactory<Program>().CreateClient();
            var result = await client.PutAsync("/api/tarefas", jsonRequest);

            #endregion

            #region Verificar o Resultado

            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var jsonResult = result.Content.ReadAsStringAsync().Result;
            var response = JsonConvert.DeserializeObject<EditarTarefaResponseModel>(jsonResult);

            response?.Id.Should().NotBeEmpty();
            response?.Nome.Should().Be(request.Nome);
            response?.Descricao.Should().Be(request.Descricao);
            response?.DataHora.Should().Be(request.DataHora);
            response?.Prioridade.Should().Be(request.Prioridade);
            response?.DataHoraCadastro.Should().NotBeNull();
            response?.DataHoraUltimaAtualizacao.Should().NotBeNull();
            response?.Status.Should().Be(1);

            #endregion

        }

        [Fact]
        public async Task ExcluirTarefas_Test()
        {
            #region Gerar os dados do teste

    
            var tarefa = await CadastrarTarefas_Test();

            #endregion

            #region Enviando a requisição DELETE para a API

     
            var client = new WebApplicationFactory<Program>().CreateClient();

     
            var result = await client.DeleteAsync("/api/tarefas/" + tarefa.Id);

            #endregion

            #region Verificar o resultado

          
            result.StatusCode.Should().Be(HttpStatusCode.OK);

      
            var jsonResult = result.Content.ReadAsStringAsync().Result;
            var response = JsonConvert.DeserializeObject<ExcluirTarefaResponseModel>(jsonResult);

           
            response?.Id.Should().Be(tarefa.Id);
            response?.Nome.Should().Be(tarefa.Nome);
            response?.Descricao.Should().Be(tarefa.Descricao);
            response?.DataHora.Should().Be(tarefa.DataHora);
            response?.Prioridade.Should().Be(tarefa.Prioridade);
            response?.DataHoraExclusao.Should().NotBeNull();

            #endregion


        }

        [Fact]
        public async Task ConsultarTarefasPorDatas_Test()
        {
            #region Gerar os dados do teste

            var tarefa = await CadastrarTarefas_Test();

            var dataInicio = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            var dataFim = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");

            #endregion

            #region Enviando a requisição GET para API

            var client = new WebApplicationFactory<Program>().CreateClient();

            var result = await client.GetAsync("/api/tarefas/" + dataInicio + "/" + dataFim);

            #endregion

            #region Verificar o resultado

            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var jsonResult = result.Content.ReadAsStringAsync().Result;
            var response = JsonConvert.DeserializeObject<List<ConsultarTarefaResponseModel>>(jsonResult);

            var tarefaObtida = response?.FirstOrDefault(t => t.Id == tarefa.Id);

            tarefaObtida?.Id.Should().Be(tarefa.Id);
            tarefaObtida?.Nome.Should().Be(tarefa.Nome);
            tarefaObtida?.Descricao.Should().Be(tarefa.Descricao);
            tarefaObtida?.DataHora.Should().Be(tarefa.DataHora);
            tarefaObtida?.Prioridade.Should().Be(tarefa.Prioridade);


            #endregion

        }

        [Fact]
        public async Task ObterTarefasPorId_Test()
        {
            #region Gerar os dados do teste

            var tarefa = await CadastrarTarefas_Test();

            #endregion

            #region Enviando a requisição GET para API

            var client = new WebApplicationFactory<Program>().CreateClient();

            var result = await client.GetAsync("/api/tarefas/" + tarefa.Id);

            #endregion

            #region Verificar o resultado


            result.StatusCode.Should().Be(HttpStatusCode.OK);


            var jsonResult = result.Content.ReadAsStringAsync().Result;
            var response = JsonConvert.DeserializeObject<ConsultarTarefaResponseModel>(jsonResult);


            response?.Id.Should().Be(tarefa.Id);
            response?.Nome.Should().Be(tarefa.Nome);
            response?.Descricao.Should().Be(tarefa.Descricao);
            response?.DataHora.Should().Be(tarefa.DataHora);
            response?.Prioridade.Should().Be(tarefa.Prioridade);
            
            #endregion

        }
    }
}
