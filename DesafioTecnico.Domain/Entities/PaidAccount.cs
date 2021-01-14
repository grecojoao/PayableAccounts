using System;
using DesafioTecnico.Domain.Entities.Primitives;

namespace DesafioTecnico.Domain.Entities
{
    public class PaidAccount : Entity
    {
        public PaidAccount(Guid payableAccountId, DateTime payDay, int delayedDays, decimal amountPaid)
        {
            PayableAccountId = payableAccountId;
            PayDay = payDay;
            DelayedDays = delayedDays;
            AmountPaid = amountPaid;
        }

        public DateTime PayDay { get; private set; }
        public int DelayedDays { get; private set; }
        public decimal AmountPaid { get; private set; }
        public Guid PayableAccountId { get; private set; }
        public PayableAccount PayableAccount { get; set; }
    }
}