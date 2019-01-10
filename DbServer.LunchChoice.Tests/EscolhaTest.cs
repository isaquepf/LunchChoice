using DbServer.LunchChoice.Core.domain;
using DbServer.LunchChoice.Core.domain.services;
using DbServer.LunchChoice.Core.infra;
using DbServer.LunchChoice.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DbServer.LunchChoice.Tests
{
    public class EscolhaTest
    {
        private static VotoDomainService _service = new VotoDomainService();

        [Fact]
        public async Task Um_Profissional_So_Pode_Votar_Em_Um_Restaurante_Por_Dia()
        {
            var restaurantes = await RestauranteRepository.ObterRestaurantes();
            var isaque = Profissional.Create("Isaque");
            var outback = restaurantes.Find(p => p.Nome.Contains("OutBack", StringComparison.InvariantCultureIgnoreCase));
            var mac = restaurantes.Find(p => p.Nome.Contains("macdonald's", StringComparison.InvariantCultureIgnoreCase));

            var escolha = Voto.Create(DateTime.Now.AddDays(-1), outback.Id, isaque.Id);
            var escolhaRepetida = Voto.Create(DateTime.Now.AddDays(-1), mac.Id, isaque.Id);

            Exception exception = Assert.Throws<ArgumentException>(() =>
            {
                _service.EscolherRestaurante(escolha);
                _service.EscolherRestaurante(escolhaRepetida);
            });

            Assert.Equal("Você já escolheu um restaurante para esse dia.", exception.Message);
        }

        [Fact]
        public async Task Um_Restaurante_Nao_Deve_Ser_Escolhido_Mais_De_Uma_Vez_Na_Semana()
        {
            var restaurantes = await RestauranteRepository.ObterRestaurantes();
            var isaque = Profissional.Create("Isaque");
            var bella = Profissional.Create("Bella");
            var joao = Profissional.Create("Joao");
            var maria = Profissional.Create("Maria");

            var outback = restaurantes.Find(p => p.Nome.Contains("OutBack", StringComparison.InvariantCultureIgnoreCase));

            var escolha1 = Voto.Create(DateTime.Now.AddDays(-1), outback.Id, isaque.Id);
            var escolha2 = Voto.Create(DateTime.Now.AddDays(-1), outback.Id, bella.Id);
            var escolha3 = Voto.Create(DateTime.Now, outback.Id, joao.Id);
            var escolha4 = Voto.Create(DateTime.Now, outback.Id, maria.Id);

            Exception exception = Assert.Throws<ArgumentException>(() =>
            {
                _service.EscolherRestaurante(escolha1);
                _service.EscolherRestaurante(escolha2);
                _service.EncerrarVotacao(restaurantes, DateTime.Now.AddDays(-1));
                _service.EscolherRestaurante(escolha3);
                _service.EscolherRestaurante(escolha4);
                _service.EncerrarVotacao(restaurantes, DateTime.Now);
            });

            Assert.Equal("Esse restaurante já foi Escolhido essa semana.", exception.Message);
        }



        [Fact]
        public async Task Deve_Retornar_O_Restaurante_Escolhido()
        {
            var restaurantes = await RestauranteRepository.ObterRestaurantes();

            var usuario1 = Profissional.Create("Isaque");
            var usuario2 = Profissional.Create("Bella");
            var usuario3 = Profissional.Create("Joao");
            var usuario4 = Profissional.Create("Maria");
            var usuario5 = Profissional.Create("Jose");
            var usuario6 = Profissional.Create("Betania");


            var outback = restaurantes.Find(p => p.Nome.Contains("OutBack", StringComparison.InvariantCultureIgnoreCase));
            var mac = restaurantes.Find(p => p.Nome.Contains("macdonald's", StringComparison.InvariantCultureIgnoreCase));

            var escolha1 = Voto.Create(DateTime.Now, mac.Id, usuario1.Id);
            var escolha2 = Voto.Create(DateTime.Now, mac.Id, usuario2.Id);
            var escolha3 = Voto.Create(DateTime.Now, mac.Id, usuario3.Id);
            var escolha4 = Voto.Create(DateTime.Now, outback.Id, usuario4.Id);
            var escolha5 = Voto.Create(DateTime.Now, outback.Id, usuario5.Id);
            var escolha6 = Voto.Create(DateTime.Now, outback.Id, usuario6.Id);


            _service.EscolherRestaurante(escolha1);
            _service.EscolherRestaurante(escolha2);
            _service.EscolherRestaurante(escolha3);
            _service.EscolherRestaurante(escolha4);
            _service.EscolherRestaurante(escolha5);
            _service.EscolherRestaurante(escolha6);

            var restauranteEscolhido = _service.EncerrarVotacao(restaurantes, DateTime.Now);
            
                      
            Assert.NotNull(restauranteEscolhido);
        }
    }
}



