using System.ComponentModel.DataAnnotations;

namespace DockerComposeExample.Application.DTO
{
    public class PagamentoInputDTO
    {
        [Required(ErrorMessage = "{0} é obrigatório")]
        [Range(typeof(decimal), "0.01", "79228162514264337593543950335", ErrorMessage = "{0} precisa ser maior que 0,00")]
        public decimal ValorPagamento { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [Range(typeof(decimal), "0.01", "79228162514264337593543950335", ErrorMessage = "{0} precisa ser maior que 0,00")]
        public decimal ValorPagoCliente { get; set; }
    }
}
