using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DesafioTecnico.Domain.Entities;

namespace DesafioTecnico.Domain.Infra.Storage.Repositories.Contracts
{
    public interface IPaidAccountRepository
    {
        Task InsertAsync(PaidAccount paidAccount, CancellationToken cancellationToken);
        Task<PaidAccount> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<PaidAccount>> GetAsync(CancellationToken cancellationToken);
    }
}