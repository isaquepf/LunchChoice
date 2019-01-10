using DbServer.LunchChoice.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbServer.LunchChoice.Core.domain.services
{
    public class VotoDomainService
    {
        public static List<Voto> Votos { get; set; }
        
        public static List<RestauranteEscolhido> RestauranteEscolhidos { get; set; }

        public void EscolherRestaurante(Voto voto)
        {
            if (Votos == null)
                Votos = new List<Voto>();

            var profissionalJaEscolheuRestaurante = Votos.Exists(p => p.Data.Date == voto.Data.Date 
                                        && voto.ProfissionalId == p.ProfissionalId);

            if (profissionalJaEscolheuRestaurante)
                throw new ArgumentException("Você já escolheu um restaurante para esse dia.");


            if (RestauranteEscolhidos != null && RestauranteEscolhidos.Any())
            {
                var restauranteJaEscolhidoDuranteSemana = RestauranteEscolhidos.Exists(p => p.Data.GetWeekOfYear() == voto.Data.GetWeekOfYear() &&
                                                    p.RestauranteId == voto.RestauranteId);

                if (restauranteJaEscolhidoDuranteSemana)
                    throw new ArgumentException("Esse restaurante já foi Escolhido essa semana.");
            }

            Votos.Add(voto);
        }

        public Resumo EncerrarVotacao(List<Restaurante> restaurantes, DateTime dataFechamento)
        {
            if (RestauranteEscolhidos == null)
                RestauranteEscolhidos = new List<RestauranteEscolhido>();

            var resultado = Votos.Where(p => p.Data.Date == dataFechamento.Date).GroupBy(p => p.RestauranteId);
          
            var resumos = new List<Resumo>();

            foreach (var item in resultado)
            {            
                resumos.Add(new Resumo
                {
                    Restaurante = restaurantes.Find(p => p.Id == item.Key).Nome,
                    RestauranteId = item.Key,
                    QuantidadeDeVotos = item.Count()
                });
            }

            var resumo = resumos.OrderByDescending(p => p.QuantidadeDeVotos).FirstOrDefault();

            RestauranteEscolhidos.Add(new RestauranteEscolhido
            {
                Data = DateTime.Now,
                QuantidadeVotos = resumo.QuantidadeDeVotos,
                RestauranteId = resumo.RestauranteId,
            });

            return resumo;
        }
    }
}
