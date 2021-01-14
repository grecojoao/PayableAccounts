namespace DesafioTecnico.Domain.Commands.Contracts
{
    public interface ICommandResult
    {
        bool Sucess { get; set; }
        object Entity { get; set; }
        object Notification { get; set; }
        string Message { get; set; }
    }
}