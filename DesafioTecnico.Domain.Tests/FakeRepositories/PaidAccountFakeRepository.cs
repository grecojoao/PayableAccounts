using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DesafioTecnico.Domain.Entities;
using DesafioTecnico.Domain.Infra.Storage.Repositories.Contracts;

namespace DesafioTecnico.Domain.Tests.FakeRepositories
{
    public class PaidAccountFakeRepository : IPaidAccountRepository
    {
        private readonly IList<PaidAccount> _context;

        public PaidAccountFakeRepository() =>
            _context = new List<PaidAccount>();

        public Task InsertAsync(PaidAccount paidAccount, CancellationToken cancellationToken = default)
        {
            _context.Add(paidAccount);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<PaidAccount>> GetAsync(CancellationToken cancellationToken = default) =>
            Task.FromResult((IEnumerable<PaidAccount>)_context.ToArray());

        public Task<PaidAccount> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
            Task.FromResult(_context.FirstOrDefault(paidAccount => paidAccount.Id == id));
    }
}