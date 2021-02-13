using System;

namespace DockerComposeExample.Domain.Base
{
    public class EntidadeBase
    {
        public Guid Id { get; protected set; }

        public EntidadeBase()
        {
            Id = Guid.NewGuid();
        }
    }
}
