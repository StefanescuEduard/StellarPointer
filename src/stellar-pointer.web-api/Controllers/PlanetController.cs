using StellarPointer.WebApi.Models;
using System.Threading.Tasks;
using System.Web.Http;

namespace StellarPointer.WebApi.Controllers
{
    public class PlanetController : ApiController
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
