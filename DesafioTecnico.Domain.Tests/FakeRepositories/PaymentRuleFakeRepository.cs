using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DesafioTecnico.Domain.Entities;
using DesafioTecnico.Domain.Infra.Storage.Repositories.Contracts;

namespace DesafioTecnico.Domain.Tests.FakeRepositories
{
    public class PaymentRuleFakeRepository : IPaymentRuleRepository
    {
        private readonly IList<PaymentRule> _context;

        public PaymentRuleFakeRepository(IList<PaymentRule> context) =>
            _context = context;

        public Task InsertAsync(PaymentRule paymentRule, CancellationToken cancellationToken = default)
        {
            _context.Add(paymentRule);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<PaymentRule>> GetAsync(CancellationToken cancellationToken = default) =>
            Task.FromResult((IEnumerable<PaymentRule>)_context.ToArray());
    }
}