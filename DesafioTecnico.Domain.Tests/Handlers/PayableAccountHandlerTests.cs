using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DesafioTecnico.Domain.Commands;
using DesafioTecnico.Domain.Entities;
using DesafioTecnico.Domain.Handlers;
using DesafioTecnico.Domain.Services;
using DesafioTecnico.Domain.Tests.FakeRepositories;
using DesafioTecnico.Domain.Tests.FakeTransactions;
using Flunt.Notifications;
using Xunit;

namespace DesafioTecnico.Domain.Tests.Handlers
{
    public class PayableAccountHandlerTests
    {
        private PayableAccountHandler _payableAccountHandler;
        private PayableAccountCreateCommand _payableAccountCreateValidCommand;
        private PayableAccountCreateCommand _payableAccountCreateInvalidCommand;

        public PayableAccountHandlerTests()
        {
            var paymentRules = new List<PaymentRule>()
            {
                new PaymentRule(delayedDays: 0, finePercentage: 2, finePercentageInterestPerDay: 0.1m),
                new PaymentRule(delayedDays: 4, finePercentage: 3, finePercentageInterestPerDay: 0.2m),
                new PaymentRule(delayedDays: 6, finePercentage: 5, finePercentageInterestPerDay: 0.3m)
            };
            _payableAccountHandler = new PayableAccountHandler(
                new PayableAccountFakeRepository(),
                new PaidAccountFakeRepository(),
                new PayableAccountService(new PaymentRuleFakeRepository(paymentRules)),
                new FakeUnitOfWork());
        }

        [Fact]
        public async Task DadoUmRegistroDeUmaContaAPagarEOPagamentoDestaContaEmDiaORetornoDeveSerQueFoiRegistradaEPaga()
        {
            _payableAccountCreateValidCommand = new PayableAccountCreateCommand(
                name: "Conta de teste",
                value: 100m,
                dueDate: DateTime.Now.Date,
                payDay: DateTime.Now.Date);
            var retorno = (CommandResult)await _payableAccountHandler.Handle(_payableAccountCreateValidCommand);
            Assert.True(retorno.Sucess);
        }

        [Fact]
        public async Task DadoUmRegistroDeUmaContaAPagarEOPagamentoDestaContaEmAtrasoORetornoDeveSerQueFoiRegistradaEPaga()
        {
            _payableAccountCreateValidCommand = new PayableAccountCreateCommand(
                name: "Conta de teste",
                value: 100m,
                dueDate: DateTime.Now.Date,
                payDay: DateTime.Now.Date.AddDays(4));
            var retorno = (CommandResult)await _payableAccountHandler.Handle(_payableAccountCreateValidCommand);
            Assert.True(retorno.Sucess);
        }

        [Fact]
        public async Task DadoUmRegistroDeUmaContaAPagarInvalidaORetornoDeveSerQueFoiNaoRegistrada()
        {
            _payableAccountCreateInvalidCommand = new PayableAccountCreateCommand();
            var retorno = (CommandResult)await _payableAccountHandler.Handle(_payableAccountCreateInvalidCommand);
            Assert.True(!retorno.Sucess && ((IList<Notification>)retorno.Notification).Count == 4);
        }
    }
}