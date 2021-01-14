using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DesafioTecnico.Domain.Entities;

namespace DesafioTecnico.Domain.Infra.Storage.Repositories.Contracts
{
    public interface IPaymentRuleRepository
    {
        Task InsertAsync(PaymentRule paymentRule, CancellationToken cancellationToken);
        Task<IEnumerable<PaymentRule>> GetAsync(CancellationToken cancellationToken);
    }
}