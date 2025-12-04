namespace Public.DTO.V1.Status;

public class StatusCreateDTO
{
    public string StatusName { get; set; } = default!;

    public string? Comment { get; set; }
}