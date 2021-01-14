using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DesafioTecnico.Domain.Entities;

namespace DesafioTecnico.Domain.Infra.Storage.Repositories.Contracts
{
    public interface IPayableAccountRepository
    {
        Task InsertAsync(PayableAccount payableAccount, CancellationToken cancellationToken);
        Task<PayableAccount> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<PayableAccount>> GetAsync(CancellationToken cancellationToken);
    }
}