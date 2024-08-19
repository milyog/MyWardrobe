namespace MyWardrobe2.Models
{
    public class WardrobeItem
    {
        public Guid Id { get; set; }
        public string Category { get; set; }
        public string Subcategory { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public string Material { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string Description { get; set; }
        //List<UsageLog> UsageLogs) { get; set; };

        public WardrobeItem(
            Guid id, 
            string category, 
            string subcategory, 
            string brand, 
            string model, 
            decimal price, 
            string material,  
            string color, 
            string size,
            string description) 
        {
            Id = id;
            Category = category; 
            Subcategory = subcategory; 
            Brand = brand; 
            Model = model; 
            Price = price; 
            Material = material; 
            Color = color; 
            Size = size; 
            Description = description;
        }
    }
}
