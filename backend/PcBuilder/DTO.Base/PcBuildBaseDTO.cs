namespace DTO.Base;

public class PcBuildBaseDTO
{
    public string PcName { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int Stock { get; set; }
    public string? ImageSrc { get; set; } = default!;
    public Guid CaseId { get; set; }
    public Guid MotherboardId { get; set; }
    public Guid ProcessorId { get; set; }
    public Guid CpuCoolerId { get; set; }
    public Guid MemoryId { get; set; }
    public Guid GraphicsCardId { get; set; }
    public Guid PrimaryStorageId { get; set; }
    public Guid? SecondaryStorageId { get; set; }
    public Guid PowerSupplyId { get; set; }
    public Guid OperatingSystemId { get; set; }
}