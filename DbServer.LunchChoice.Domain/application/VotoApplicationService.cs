using DbServer.LunchChoice.Core.application.Resources;
using DbServer.LunchChoice.Core.domain;
using DbServer.LunchChoice.Core.domain.services;
using DbServer.LunchChoice.Core.infra;
using DbServer.LunchChoice.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DbServer.LunchChoice.Core.application
{
    public class VotoApplicationService
    {
        private static VotoDomainService _service = new VotoDomainService();

        public async Task<Guid> Votar(VotoResource votoResource)
        {
            Guid? profissionalId = null;
            Guid? restauranteId = null;

            if (!string.IsNullOrEmpty(votoResource.ProfissionalId))
                profissionalId = Guid.Parse(votoResource.ProfissionalId);

            if (!string.IsNullOrEmpty(votoResource.RestauranteId))
                restauranteId = Guid.Parse(votoResource.RestauranteId);

            var profissional = Profissional.Create(votoResource.ProfissionalNome, profissionalId);           
            var voto = Voto.Create(DateTime.Now, restauranteId.Value, profissional.Id);
            _service.EscolherRestaurante(voto);
            return profissional.Id;
        }

        public async Task<Resumo> ContabilizarVotos()
        {
            var restaurantes = await RestauranteRepository.ObterRestaurantes();
            return _service.EncerrarVotacao(restaurantes, DateTime.Now);
        }
    }
}
