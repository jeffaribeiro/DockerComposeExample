using System;
using System.Collections.Generic;
using System.Text;

namespace DockerComposeExample.Api.DTO
{
    public class ResponseErroDTO : ResponseBaseDTO 
    {
        public ResponseErroDTO()
        {
            this.Success = false;
        }

        public List<string> Errors { get; set; }
    }
}
