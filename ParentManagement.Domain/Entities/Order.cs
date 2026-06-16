namespace ParentManagement.Domain.Entities
{
    public class Order
    {
        public int SchoolId { get; set; }

        public string ParentEmail { get; set; } = string.Empty;

        public List<OrderLine> Lines { get; set; } = new List<OrderLine>();
    }
}
