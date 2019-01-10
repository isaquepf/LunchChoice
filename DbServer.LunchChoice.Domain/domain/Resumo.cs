using System;
using System.Collections.Generic;
using System.Text;

namespace DbServer.LunchChoice.Core.domain
{
    public class Resumo
    {
        public string Restaurante { get; set; }

        public int QuantidadeDeVotos { get; set; }
        public Guid RestauranteId { get;  set; }
    }
}
