using System.Text.Json.Serialization;

namespace MyWardrobe.Models
{
    public class WardrobeItemUsage
    {
        public Guid Id { get; set; }
        public int WardrobeItemUsageCounter {  get; set; }
        public DateTime WardrobeItemUsageDateTime { get; set; }
        public Guid WardrobeItemId { get; set; }
        [JsonIgnore]
        public WardrobeItem? WardrobeItem { get; set; }
    }
}
