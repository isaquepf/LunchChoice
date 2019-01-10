using System;
using System.Collections.Generic;
using System.Text;

namespace DbServer.LunchChoice.Domain
{
    public class Profissional
    {
        private Profissional() {}

        public static Profissional Create(string nome, Guid? id = null)
        {            
            return new Profissional
            {
                Id = id.HasValue ? id.Value : Guid.NewGuid(),    
                Nome = nome
            };
        }

        public Guid Id { get; private set; }

        public string Nome { get; private set; }
    }
}
