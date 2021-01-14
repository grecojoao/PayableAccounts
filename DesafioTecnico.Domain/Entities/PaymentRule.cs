using DesafioTecnico.Domain.Entities.Primitives;

namespace DesafioTecnico.Domain.Entities
{
    public class PaymentRule : Entity
    {
        public PaymentRule(int delayedDays, decimal finePercentage, decimal finePercentageInterestPerDay)
        {
            DelayedDays = delayedDays;
            FinePercentage = finePercentage;
            FinePercentageInterestPerDay = finePercentageInterestPerDay;
        }

        public int DelayedDays { get; private set; }
        public decimal FinePercentage { get; private set; }
        public decimal FinePercentageInterestPerDay { get; private set; }
    }
}