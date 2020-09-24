using System.Web.Http;

namespace StellarPointer.WebApi.Controllers
{
    public class TestController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Test()
        {
            return Ok("test");
        }
    }
}
