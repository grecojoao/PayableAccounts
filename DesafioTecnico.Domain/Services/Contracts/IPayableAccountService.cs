using System;
using System.Threading;
using System.Threading.Tasks;
using DesafioTecnico.Domain.Entities;

namespace DesafioTecnico.Domain.Services.Contracts
{
    public interface IPayableAccountService
    {
        Task<PaidAccount> Pay(PayableAccount payableAccount, DateTime payDay, CancellationToken cancellationToken = default);
    }
}