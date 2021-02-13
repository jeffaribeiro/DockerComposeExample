using System;
using System.Collections.Generic;
using System.Text;

namespace DockerComposeExample.Api.DTO
{
    public class ResponseSuccessoDTO : ResponseBaseDTO 
    {
        public ResponseSuccessoDTO()
        {
            this.Success = true;
        }

        public object Data { get; set; }
    }
}
