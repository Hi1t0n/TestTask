namespace TestTask.Domain.Models;

public class Product
{
    public Guid ProductId { get; set; }
    public required string ProductName { get; set; }
    public required string ProductDescription { get; set; }
    public List<Order> Orders { get; set; } = new List<Order>();
}