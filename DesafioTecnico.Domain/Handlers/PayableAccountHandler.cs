using System;
using System.Threading;
using System.Threading.Tasks;
using DesafioTecnico.Domain.Commands;
using DesafioTecnico.Domain.Commands.Contracts;
using DesafioTecnico.Domain.Entities;
using DesafioTecnico.Domain.Infra.Storage.Repositories.Contracts;
using DesafioTecnico.Domain.Infra.Storage.Transactions.Contracts;
using DesafioTecnico.Domain.Services.Contracts;
using MediatR;

namespace DesafioTecnico.Domain.Handlers
{
    public class PayableAccountHandler :
    IRequestHandler<PayableAccountCreateCommand, ICommandResult>
    {
        private readonly IPayableAccountRepository _payableAccountRepository;
        private readonly IPaidAccountRepository _paidAccountRepository;
        private readonly IPayableAccountService _payableAccountService;
        private readonly IUnitOfWork _unitOfWork;

        public PayableAccountHandler(
            IPayableAccountRepository payableAccountRepository,
            IPaidAccountRepository paidAccountRepository,
            IPayableAccountService payableAccountService,
            IUnitOfWork unitOfWork)
        {
            _payableAccountRepository = payableAccountRepository;
            _paidAccountRepository = paidAccountRepository;
            _payableAccountService = payableAccountService;
            _unitOfWork = unitOfWork;
        }

        public async Task<ICommandResult> Handle(PayableAccountCreateCommand request, CancellationToken cancellationToken = default)
        {
            request.Validate();
            if (request.Invalid)
                return new CommandResult(false, "Dados incompletos", null, request.Notifications);

            var payableAccount = new PayableAccount(request.Name, request.Value, (DateTime)request.DueDate);
            try
            {
                await _payableAccountRepository.InsertAsync(payableAccount, cancellationToken);
                var paidAccount = await _payableAccountService.Pay(payableAccount, (DateTime)request.Payday);
                await _paidAccountRepository.InsertAsync(paidAccount, cancellationToken);
                await _unitOfWork.CommitAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync(cancellationToken);
                return new CommandResult(false, "Erro ao gravar o registro", null, ex.Message);
            }
            return new CommandResult(true, "Conta a pagar registrada", payableAccount);
        }
    }
}