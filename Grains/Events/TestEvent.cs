namespace OrleansTesting.Grains.Events
{
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
