using System;
using DesafioTecnico.Domain.Entities.Primitives;

namespace DesafioTecnico.Domain.Entities
{
    public class PayableAccount : Entity
    {
        public PayableAccount(string name, decimal value, DateTime dueDate)
        {
            Name = name;
            Value = value;
            DueDate = dueDate;
        }

        public string Name { get; private set; }
        public decimal Value { get; private set; }
        public DateTime DueDate { get; private set; }
    }
}