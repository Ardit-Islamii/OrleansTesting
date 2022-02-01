using Orleans;

namespace OrleansTesting.Interfaces
{
    public interface ITestGrain : IGrainWithStringKey
    {
        Task<string> SayHello(string greeting);
    }
}
