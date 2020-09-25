using Microsoft.AspNetCore.Mvc;
using StellarPointer.WebApi.Models;
using System.Threading.Tasks;

namespace StellarPointer.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlanetController : ControllerBase
    {
        private readonly SerialWriter serialWriter;

        public PlanetController()
        {
            serialWriter = new SerialWriter();
        }

        [HttpPost]
        public Task PointStellarObject([FromBody] StellarObjectDesignation stellarObjectDesignation)
        {
            return serialWriter.PointSkyObject(stellarObjectDesignation);
        }
    }
}
