using System;

namespace Itau.TestePratico.Dominio.Modelo.Abstract
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        public Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}
