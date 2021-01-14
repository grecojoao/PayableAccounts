using DesafioTecnico.Domain.Entities;

namespace DesafioTecnico.Domain.API.Models.Factories
{
    public static class PaidAccountDTOFactory
    {
        public static PaidAccountDTO FactoryMethod(PaidAccount paidAccount)
        {
            return new PaidAccountDTO()
            {
                Nome = paidAccount.PayableAccount.Name,
                ValorOriginal = paidAccount.PayableAccount.Value,
                ValorCorrigido = paidAccount.AmountPaid,
                QuantidadeDeDiasDeAtraso = paidAccount.DelayedDays,
                DataDePagamento = paidAccount.PayDay
            };
        }
    }
}