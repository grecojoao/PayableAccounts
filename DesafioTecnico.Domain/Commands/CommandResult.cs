using DesafioTecnico.Domain.Commands.Contracts;

namespace DesafioTecnico.Domain.Commands
{
    public class CommandResult : ICommandResult
    {
        public CommandResult() { }
        public CommandResult(bool sucess, string message, object entity = null, object notification = null)
        {
            Sucess = sucess;
            Message = message;
            Entity = entity;
            Notification = notification;
        }

        public bool Sucess { get; set; }
        public object Entity { get; set; }
        public object Notification { get; set; }
        public string Message { get; set; }
    }
}