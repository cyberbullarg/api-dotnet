namespace BasicAPI.Model.Request
{
    public class CreateProductRequest
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public Guid CategoryId { get; set; }
    }
}
