using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DesafioTecnico.Domain.Entities;
using DesafioTecnico.Domain.Services;
using DesafioTecnico.Domain.Services.Contracts;
using DesafioTecnico.Domain.Tests.FakeRepositories;
using Xunit;

namespace DesafioTecnico.Domain.Tests.Services
{
    public class PayableAccountServiceTests
    {
        private IPayableAccountService _payableAccountService;

        public PayableAccountServiceTests()
        {
            var paymentRules = new List<PaymentRule>()
            {
                new PaymentRule(delayedDays: 0, finePercentage: 2, finePercentageInterestPerDay: 0.1m),
                new PaymentRule(delayedDays: 4, finePercentage: 3, finePercentageInterestPerDay: 0.2m),
                new PaymentRule(delayedDays: 6, finePercentage: 5, finePercentageInterestPerDay: 0.3m)
            };
            _payableAccountService = new PayableAccountService(new PaymentRuleFakeRepository(paymentRules));
        }

        [Fact]
        public async Task DadaUmaContaAPagarEOPagamentoDaMesmaEmDiaOValorPagoDeveSerIgualAoOriginal()
        {
            var payableAccount = new PayableAccount("Conta de teste", value: 100m, dueDate: DateTime.Now.Date.AddDays(3));
            var paidAccount = await _payableAccountService.Pay(payableAccount, payDay: DateTime.Now.Date);
            Assert.True(paidAccount.AmountPaid == 100m && paidAccount.DelayedDays == 0);
        }

        [Fact]
        public async Task DadaUmaContaAPagarEOPagamentoDaMesmaComTresDiasDeAtrasoOValorPagoDeveSerDeCentoEDoisComTrinta()
        {
            var payableAccount = new PayableAccount("Conta de teste", value: 100m, dueDate: DateTime.Now.Date);
            var paidAccount = await _payableAccountService.Pay(payableAccount, payDay: DateTime.Now.Date.AddDays(3));
            Assert.True(paidAccount.AmountPaid == 102.30m && paidAccount.DelayedDays == 3);
        }

        [Fact]
        public async Task DadaUmaContaAPagarEOPagamentoDaMesmaComCincoDiasDeAtrasoOValorPagoDeveSerDeCentoEQuatro()
        {
            var payableAccount = new PayableAccount("Conta de teste", value: 100m, dueDate: DateTime.Now.Date);
            var paidAccount = await _payableAccountService.Pay(payableAccount, payDay: DateTime.Now.Date.AddDays(5));
            Assert.True(paidAccount.AmountPaid == 104m && paidAccount.DelayedDays == 5);
        }

        [Fact]
        public async Task DadaUmaContaAPagarEOPagamentoDaMesmaComSeisDiasDeAtrasoOValorPagoDeveSerDeCentoEOitoComOitenta()
        {
            var payableAccount = new PayableAccount("Conta de teste", value: 100m, dueDate: DateTime.Now.Date);
            var paidAccount = await _payableAccountService.Pay(payableAccount, payDay: DateTime.Now.Date.AddDays(6));
            Assert.True(paidAccount.AmountPaid == 106.80m && paidAccount.DelayedDays == 6);
        }
    }
}