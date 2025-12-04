namespace DAL.DTO.Component;

public class ComponentSimpleDTO
{
    public Guid Id { get; set; }
    public string CategoryName { get; set; } = default!;
    public string ComponentName { get; set; } = default!;
}