using System;
using System.Collections.Generic;
using System.Text;

namespace DockerComposeExample.Api.DTO
{
    public abstract class ResponseBaseDTO
    {
        public bool Success { get; protected set; }
    }
}
