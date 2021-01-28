using System.Threading.Tasks;
using DesafioTecnico.Domain.Commands;
using DesafioTecnico.Domain.Infra.Storage.Repositories.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using DesafioTecnico.Domain.API.Models.Factories;

namespace DesafioTecnico.Domain.API.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class PayableAccountController : ControllerBase
    {
        [HttpPost]
        public async Task<CommandResult> Post(
            [FromBody] PayableAccountCreateCommand command,
            [FromServices] IMediator mediator)
        {
            return (CommandResult)await mediator.Send(command);
        }

        [HttpGet]
        public async Task<CommandResult> Get([FromServices] IPaidAccountRepository paidAccountRepository)
        {
            var paidAccounts = await paidAccountRepository.GetAsync(default);
            var paidAccountsDTO = (from paidAccount in paidAccounts
                                   select PaidAccountDTOFactory.FactoryMethod(paidAccount)).ToList();
            return paidAccountsDTO.Count == 0 ?
                new CommandResult(false, "Não há contas a pagar cadastradas") :
                new CommandResult(true, "Contas a pagar cadastradas", paidAccountsDTO);
        }
    }
}