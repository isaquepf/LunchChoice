using System;

namespace DbServer.LunchChoice.Domain
{
    public class Restaurante
    {
        private Restaurante() {}

        public static Restaurante Create(string nome, Guid? id =null)
        {
            return new Restaurante
            {
                Id = id ?? Guid.NewGuid(),
                Nome = nome
            };
        }

        public Guid Id { get; private set; }

        public string Nome { get; private set; }
    }
}
