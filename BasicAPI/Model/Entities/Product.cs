namespace BasicAPI.Model.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
