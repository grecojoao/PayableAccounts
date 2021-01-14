using System;

namespace DesafioTecnico.Domain.API.Models
{
    public class PaidAccountDTO
    {
        public string Nome { get; set; }
        public decimal ValorOriginal { get; set; }
        public decimal ValorCorrigido { get; set; }
        public int QuantidadeDeDiasDeAtraso { get; set; }
        public DateTime DataDePagamento { get; set; }
    }
}