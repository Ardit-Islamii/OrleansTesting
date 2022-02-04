using Orleans;
using Orleans.Runtime;
using OrleansTesting.Grains.States;
using OrleansTesting.Interfaces;

namespace OrleansTesting.Grains
{
    public class BetGrain : Grain, IBetGrain
    {
        private readonly IPersistentState<TestState> _test;

        public BetGrain([PersistentState("test", "testStorage")] IPersistentState<TestState> test)
        {
            _test = test;
        }
        public Task<string> GetBetNameAsync() => Task.FromResult(_test.State.Name);
        public async Task SetBetNameAsync(string name)
        {
            _test.State.Name = name;
            await _test.WriteStateAsync();
        }
        public Task<string> SayHello(string greeting)
        {
            return Task.FromResult($"Hello from: {greeting}");
        }
    }
}
