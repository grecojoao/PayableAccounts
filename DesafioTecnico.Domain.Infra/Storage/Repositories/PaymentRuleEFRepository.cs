using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DesafioTecnico.Domain.Entities;
using DesafioTecnico.Domain.Infra.Storage.Data;
using DesafioTecnico.Domain.Infra.Storage.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DesafioTecnico.Domain.Infra.Storage.Repositories
{
    public class PaymentRuleEFRepository : IPaymentRuleRepository
    {
        private readonly DataContext _context;

        public PaymentRuleEFRepository(DataContext context) =>
            _context = context;

        public async Task InsertAsync(PaymentRule paymentRule, CancellationToken cancellationToken = default) =>
            await _context.PaymentRules.AddAsync(paymentRule, cancellationToken);

        public async Task<IEnumerable<PaymentRule>> GetAsync(CancellationToken cancellationToken = default) =>
            await _context.PaymentRules.AsNoTracking().ToArrayAsync(cancellationToken);
    }
}