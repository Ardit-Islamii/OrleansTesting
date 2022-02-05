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
    //Relates to the siloBuilder.AddLogStorageBasedLogConsistencyProvider("testLogStorage"); on Startup.cs
    [LogConsistencyProvider(ProviderName = "testLogStorage")]
    //Relates to the siloBuilder.AddAdoNetGrainStorage("testStorage", o => on Startup.cs
    [StorageProvider(ProviderName = "testStorage")]
    //JournaledGrain<TState, TEvent> takes two classes, a state class and an event class and both should be serializable in order for the JournaledGrain To work. Refer to docs for more since JournaledGrain can only be of <TState> but didn't get to understand that part.
    public class BetGrain : JournaledGrain<TestState,TestEvent>, IBetGrain
    {
        private readonly IPersistentState<TestState> _test;

        public BetGrain([PersistentState("test", "testStorage")] IPersistentState<TestState> test)
        {
            _test = test;
        }

        public async Task<string> GetBetNameAsync()
        {
            //Gets all the confirmed events - put breakpoint to view all the events that have happened to this grain
            var allEvents = await RetrieveConfirmedEvents(0, Version);
            return await Task.FromResult(_test.State.Name);
        }

        public async Task SetBetNameAsync(string name)
        {
            _test.State.Name = name;
            //Writes a new event and adds a new event to the list inside the db(orleansstorage - payloadjson) everytime.
            RaiseEvent(new TestEvent(){GrainKey = this.GetPrimaryKeyString()});
            await ConfirmEvents();
            /*await _test.WriteStateAsync(); Commented out because RaiseEvent automatically writes state.*/
        }

        public Task<string> SayHello(string greeting)
        {
            return Task.FromResult($"Hello from: {greeting}");
        }
    }
}
