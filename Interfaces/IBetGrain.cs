using Orleans;

namespace OrleansTesting.Interfaces
{
    public interface IBetGrain : IGrainWithStringKey
    {
        Task<string> SayHello(string greeting);
        Task<string> GetBetNameAsync();
        Task SetBetNameAsync(string name);
    }
}
