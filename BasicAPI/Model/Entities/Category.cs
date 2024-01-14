using System.Text.Json.Serialization;

namespace BasicAPI.Model.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        /// <summary>
        /// This property represents all Products that a Category contains
        /// </summary>
        public ICollection<Product> Products { get; set; }
    }
}
