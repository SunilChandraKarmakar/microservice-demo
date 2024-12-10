namespace EventBus.Messages.Events
{
    public class BaseEvent
    {
        public BaseEvent()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.Now;
        }

        public Guid Id { get; private set; }
        public DateTime CreatedDate { get; private set; }
    }
}