using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DesafioTecnico.Domain.Entities;
using DesafioTecnico.Domain.Infra.Storage.Repositories.Contracts;
using DesafioTecnico.Domain.Services.Contracts;

namespace DesafioTecnico.Domain.Services
{
    public class PayableAccountService : IPayableAccountService
    {
        private readonly IPaymentRuleRepository _paymentRuleRepository;

        public PayableAccountService(IPaymentRuleRepository paymentRuleRepository) =>
            _paymentRuleRepository = paymentRuleRepository;

        public async Task<PaidAccount> Pay(PayableAccount payableAccount, DateTime payDay, CancellationToken cancellationToken = default)
        {
            if (!PaidAfterTheDueDate(payDay.Date, ((DateTime)payableAccount.DueDate).Date))
                return new PaidAccount(payableAccount.Id, payDay, 0, payableAccount.Value);
            var delayedDays = (payDay.Date - ((DateTime)payableAccount.DueDate).Date).Days;
            var paymentRules = (await _paymentRuleRepository.GetAsync(cancellationToken)).OrderBy(rule => rule.DelayedDays);
            var amountPaid = CalcularValorPago(payableAccount.Value, delayedDays, paymentRules);
            return new PaidAccount(payableAccount.Id, payDay, delayedDays, amountPaid);
        }

        private bool PaidAfterTheDueDate(DateTime payDay, DateTime dueDate) =>
            DateTime.Compare(payDay, dueDate) > 0;

        private decimal CalcularValorPago(decimal originalPayableAmount, int delayedDays, IEnumerable<PaymentRule> paymentRules)
        {
            decimal amountPaid = 0;
            foreach (var paymentRule in paymentRules)
            {
                if (delayedDays >= paymentRule.DelayedDays)
                {
                    var fineAmount = originalPayableAmount + (originalPayableAmount * (paymentRule.FinePercentage / 100));
                    var fineForDaysLate = (originalPayableAmount * ((delayedDays * paymentRule.FinePercentageInterestPerDay) / 100));
                    amountPaid = Math.Round(fineAmount + fineForDaysLate, 2);
                }
                else
                    break;
            }
            return amountPaid;
        }
    }
}