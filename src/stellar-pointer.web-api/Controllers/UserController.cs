using MediatR;
using Microsoft.AspNetCore.Mvc;
using StellarPointer.Business.Commands;
using StellarPointer.Persistence;
using System.Threading.Tasks;

namespace StellarPointer.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator mediator;

        public UserController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(User user)
        {
            await mediator.Send(new RegisterUserCommand(user));
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> AddFavoriteCelestialBody(string username, string celestialBodyName)
        {
            await mediator.Send(new AddFavoriteCelestialBodyCommand(username, celestialBodyName));
            return Ok();
        }
    }
}
