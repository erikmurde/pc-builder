namespace Public.DTO.V1.Status;

public class StatusDTO
{
    public Guid Id { get; set; }

    public string StatusName { get; set; } = default!;

    public string? Comment { get; set; }
}