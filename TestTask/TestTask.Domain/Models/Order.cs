namespace TestTask.Domain.Models;

public class Order
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public Product? Product { get; set; }
    public DateTime OrderDateTime { get; set; }
}