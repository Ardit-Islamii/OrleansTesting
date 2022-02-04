using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orleans;
using OrleansTesting.Grains;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetNameAsync(string id)
        {
            var test = _factory.GetGrain<IBetGrain>(id);
            var nameResult = await test.GetBetNameAsync();
            return Ok(nameResult);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<string>> SetNameAsync(string id,[FromBody] string betName)
        {
            var test = _factory.GetGrain<IBetGrain>(id);
            await test.SetBetNameAsync(betName);
            return Ok("something");
        }
    }
}
