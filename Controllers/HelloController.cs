using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orleans;
using OrleansTesting.Interfaces;

namespace OrleansTesting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelloController : ControllerBase
    {
        private readonly IGrainFactory _factory;

        public HelloController(IGrainFactory factory)
        {
            _factory = factory;
        }

        [HttpGet]
        public async Task<ActionResult<string>> Get()
        {
            var test =  _factory.GetGrain<ITestGrain>("Test");
            var result = await test.SayHello("Hello world from ardit");
            return Ok(result);
        }
    }
}
