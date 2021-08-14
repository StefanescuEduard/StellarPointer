using MediatR;
using Microsoft.AspNetCore.Mvc;
using StellarPointer.Business.Queries;
using StellarPointer.WebApi.Models;
using System.Threading.Tasks;

namespace StellarPointer.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CelestialBodyController : ControllerBase
    {
        private readonly SerialWriter serialWriter;
        private readonly IMediator mediator;

        public CelestialBodyController(IMediator mediator)
        {
            this.mediator = mediator;
            serialWriter = new SerialWriter();
        }

        [HttpGet]
        public async Task<IActionResult> GetVisibleCelestialBodies()
        {
            await mediator.Send(new GetVisibleCelestialBodiesQuery());
            return Ok();
        }

        [HttpPost]
        public Task PointStellarObject([FromBody] StellarObjectDesignation stellarObjectDesignation)
        {
            return serialWriter.PointSkyObject(stellarObjectDesignation);
        }
    }
}
