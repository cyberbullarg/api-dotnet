using System.Text.Json.Serialization;

namespace BasicAPI.Model.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// This property represents the id of a Category
        /// </summary>
        public Guid CategoryId { get; set; }

        [JsonIgnore]
        /// <summary>
        /// This property represents a Category
        /// </summary>
        public Category Category { get; set; }
    }
}
