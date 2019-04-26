namespace Inventory.Entities
{
    public class Inventory
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public double Cost { get; set; }

        public int Quantity { get; set; }

        public string Category { get; set; }
    }
}