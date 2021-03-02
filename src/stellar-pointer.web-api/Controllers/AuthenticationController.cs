using MediatR;
using Microsoft.AspNetCore.Mvc;
using StellarPointer.Business.Commands;
using StellarPointer.Persistence;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace StellarPointer.WebApi.Controllers
{
    [ApiController]
    [Route("identity")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator mediator;

        public AuthenticationController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Authentication([FromBody] User user)
        {
            try
            {
                string token = await mediator.Send(new AuthenticateCommand(user));
                return Ok(token);
            }
            catch (InvalidCredentialException)
            {
                return Unauthorized();
            }
        }
    }
}
