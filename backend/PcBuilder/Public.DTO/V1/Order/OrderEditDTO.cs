namespace Public.DTO.V1.Order;

public class OrderEditDTO
{
    public Guid Id { get; set; }
    public Guid StatusId { get; set; }
    public string? Comment { get; set; }
}