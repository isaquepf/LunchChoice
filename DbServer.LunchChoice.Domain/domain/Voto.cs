using System;
using System.Collections.Generic;
using System.Text;

namespace DbServer.LunchChoice.Core.domain
{
    public class Voto
    {
        private Voto() {}

        public static Voto Create(DateTime data, Guid restauranteId, Guid profissionalId)
        {
            return new Voto
            {
                Data = data,
                RestauranteId = restauranteId,
                ProfissionalId = profissionalId                               
            };
        }

        public Guid RestauranteId { get; private set; }

        public Guid ProfissionalId { get; private set; }

        public DateTime Data { get; set; }
    }
}
