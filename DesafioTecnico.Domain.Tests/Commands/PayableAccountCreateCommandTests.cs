using System;
using System.Collections.Generic;
using DesafioTecnico.Domain.Commands;
using Flunt.Notifications;
using Xunit;

namespace DesafioTecnico.Domain.Tests.Commands
{
    public class PayableAccountCreateCommandTests
    {
        private PayableAccountCreateCommand _payableAccountCreateValidCommand;
        private PayableAccountCreateCommand _payableAccountCreateInvalidCommand;

        public PayableAccountCreateCommandTests()
        {
            _payableAccountCreateValidCommand = new PayableAccountCreateCommand(
                name: "Conta de teste",
                value: 10.99m,
                dueDate: DateTime.Now.Date.AddDays(4),
                payDay: DateTime.Now.Date);
            _payableAccountCreateInvalidCommand = new PayableAccountCreateCommand();
        }

        [Fact]
        public void DadoUmComandoValidoORetornoDeveSerVerdadeiro()
        {
            _payableAccountCreateValidCommand.Validate();
            Assert.True(_payableAccountCreateValidCommand.Valid);
        }

        [Fact]
        public void DadoUmComandoInvalidoORetornoDeveSerFalso()
        {
            _payableAccountCreateInvalidCommand.Validate();
            Assert.True(_payableAccountCreateInvalidCommand.Invalid && ((IList<Notification>)_payableAccountCreateInvalidCommand.Notifications).Count == 4);
        }
    }
}