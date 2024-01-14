using BasicAPI.Model.Entities;

namespace BasicAPI.Model.Response
{
    public class CategoryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
