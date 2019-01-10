using System;
using System.Collections.Generic;
using System.Text;

namespace DbServer.LunchChoice.Core.domain
{
    public class RestauranteEscolhido
    {
        public Guid RestauranteId { get; set; }

        public int QuantidadeVotos { get; set; }

        public DateTime Data { get; set; }
    }
}
