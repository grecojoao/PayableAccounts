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
    public class PayableAccountEFRepository : IPayableAccountRepository
    {
        private readonly DataContext _context;

        public PayableAccountEFRepository(DataContext context) =>
            _context = context;

        public async Task InsertAsync(PayableAccount payableAccount, CancellationToken cancellationToken = default) =>
            await _context.PayableAccounts.AddAsync(payableAccount, cancellationToken);

        public async Task<PayableAccount> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
            await _context.PayableAccounts.AsNoTracking().FirstOrDefaultAsync(payableAccount => payableAccount.Id == id, cancellationToken);

        public async Task<IEnumerable<PayableAccount>> GetAsync(CancellationToken cancellationToken = default) =>
            await _context.PayableAccounts.AsNoTracking().ToArrayAsync(cancellationToken);
    }
}