using System;
using DesafioTecnico.Domain.Commands.Contracts;
using Flunt.Notifications;
using Flunt.Validations;
using MediatR;

namespace DesafioTecnico.Domain.Commands
{
    public class PayableAccountCreateCommand : Notifiable, IRequest<ICommandResult>
    {
        public PayableAccountCreateCommand() { }
        public PayableAccountCreateCommand(string name, decimal value, DateTime? dueDate, DateTime? payDay)
        {
            Name = name;
            Value = value;
            DueDate = dueDate;
            Payday = payDay;
        }

        public string Name { get; set; }
        public decimal Value { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? Payday { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .Requires()
                    .IsNotNullOrEmpty(Name, "Name", "Nome da conta a pagar não pode estar em branco.")
                    .IsGreaterThan(Value, 0, "Value", "O valor da conta a pagar tem de ser maior que zero.")
                    .IsNotNull(DueDate, "Due date", "A data de vencimento deve ser informada.")
                    .IsNotNull(Payday, "Pay Day", "A data de pagamento deve ser informada."));
        }
    }
}