namespace IntegrationEvents
{
    public class ItemCreatedEvent 
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
