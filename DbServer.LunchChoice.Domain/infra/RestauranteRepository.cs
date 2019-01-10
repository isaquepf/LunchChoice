using DbServer.LunchChoice.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbServer.LunchChoice.Core.infra
{
    public class RestauranteRepository
    {
        private static List<Restaurante> _restaurantes => new List<Restaurante>
        {
            Restaurante.Create("Outback", Guid.Parse("d5962814-4329-4fd1-ac06-9f965f1cc129")),
            Restaurante.Create("Barbacoa", Guid.Parse("4c6672f1-45a9-412b-a205-1870fa4f0c25")),
            Restaurante.Create("Cocô Bambu", Guid.Parse("85d1992d-3c3f-43bf-8901-2a84cb9347c8")),
            Restaurante.Create("Macdonald's", Guid.Parse("14ec1058-fe1c-4440-b2d4-b5411c9fd673")),
            Restaurante.Create("Dôzo", Guid.Parse("95a07767-d57d-42dc-8e03-fd355b2fb62e"))
        };


        public static Task<List<Restaurante>> ObterRestaurantes() 
            => Task.FromResult(_restaurantes);

        public static Task<Restaurante> ObterRestaurante(Guid id) 
            => Task.FromResult(_restaurantes.Find(p => p.Id == id));

        public static Task<Restaurante> ObterRestaurante(string nome) 
            => Task.FromResult(_restaurantes.Find(p => p.Nome.Contains(nome, StringComparison.InvariantCultureIgnoreCase)));

    }
}
