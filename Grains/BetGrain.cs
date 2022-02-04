using Orleans;
using Orleans.EventSourcing;
using Orleans.Providers;
using Orleans.Runtime;
using Orleans.Storage;
using OrleansTesting.Grains.Events;
using OrleansTesting.Grains.States;
using OrleansTesting.Interfaces;

namespace OrleansTesting.Grains
{
    [StorageProvider(ProviderName = "testStorage")]
    public class BetGrain : JournaledGrain<TestState,TestEvent>, IBetGrain
    {
        private readonly IPersistentState<TestState> _test;

        public BetGrain([PersistentState("test", "testStorage")] IPersistentState<TestState> test)
        {
            _test = test;
        }

        public async Task<string> GetBetNameAsync()
        {
            return await Task.FromResult(_test.State.Name);
        }

        public async Task SetBetNameAsync(string name)
        {
            _test.State.Name = name;
            RaiseEvent(new TestEvent(){GrainKey = this.GetPrimaryKeyString()});
            await ConfirmEvents();
            /*await _test.WriteStateAsync();*/
        }

        public Task<string> SayHello(string greeting)
        {
            return Task.FromResult($"Hello from: {greeting}");
        }
    }
}
