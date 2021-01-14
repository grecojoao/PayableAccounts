using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DesafioTecnico.Domain.Entities;
using DesafioTecnico.Domain.Infra.Storage.Repositories.Contracts;

namespace DesafioTecnico.Domain.Tests.FakeRepositories
{
    public class PayableAccountFakeRepository : IPayableAccountRepository
    {
        private readonly IList<PayableAccount> _context;

        public PayableAccountFakeRepository() =>
            _context = new List<PayableAccount>();

        public Task InsertAsync(PayableAccount payableAccount, CancellationToken cancellationToken = default)
        {
            _context.Add(payableAccount);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<PayableAccount>> GetAsync(CancellationToken cancellationToken = default) =>
            Task.FromResult((IEnumerable<PayableAccount>)_context.ToArray());

        public Task<PayableAccount> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
            Task.FromResult(_context.FirstOrDefault(payableAccount => payableAccount.Id == id));
    }
}