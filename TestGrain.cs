using Orleans;
using OrleansTesting.Interfaces;

namespace OrleansTesting
{
    public class TestGrain : Grain, ITestGrain
    {
        public Task<string> SayHello(string greeting) => Task.FromResult($"Hello : {greeting}");
    }
}
