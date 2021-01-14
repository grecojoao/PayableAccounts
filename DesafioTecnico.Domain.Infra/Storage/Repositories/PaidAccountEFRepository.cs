using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DesafioTecnico.Domain.Entities;
using DesafioTecnico.Domain.Infra.Storage.Data;
using DesafioTecnico.Domain.Infra.Storage.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DesafioTecnico.Domain.Infra.Storage.Repositories
{
    public class PaidAccountEFRepository : IPaidAccountRepository
    {
        private readonly DataContext _context;

        public PaidAccountEFRepository(DataContext context) =>
            _context = context;

        public async Task InsertAsync(PaidAccount paidAccount, CancellationToken cancellationToken = default) =>
            await _context.PaidAccounts.AddAsync(paidAccount, cancellationToken);

        public async Task<PaidAccount> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
            await _context.PaidAccounts.Include(x => x.PayableAccount)
            .FirstOrDefaultAsync(paidAccount => paidAccount.Id == id, cancellationToken);

        public async Task<IEnumerable<PaidAccount>> GetAsync(CancellationToken cancellationToken = default) =>
            await _context.PaidAccounts.Include(x => x.PayableAccount)
            .ToArrayAsync(cancellationToken);
    }
}