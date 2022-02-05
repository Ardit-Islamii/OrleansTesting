namespace OrleansTesting.Grains.Events
{
    //Gets added inside the list on the payloadjson(orleanssstorage) everytime an event is Raised( RaiseEvent(TestEvent e) ) hence why it needs to be serializable.
    [Serializable]
    public class TestEvent
    {
        public string GrainKey { get; set; }
        public DateTime UpdatedTime{get; } = DateTime.Now;
        public TestEvent()
        {
                
        }
    }
}
