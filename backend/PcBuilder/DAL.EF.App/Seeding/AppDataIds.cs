using DAL.EF.App.Seeding.Names;

namespace DAL.EF.App.Seeding;

public static class AppDataIds
{   
    // constant IDs for users
    internal static readonly Guid AdminId = Guid.NewGuid();
    internal static readonly Guid AdminRoleId = Guid.NewGuid();
    internal static readonly Guid UserId = Guid.NewGuid();
    internal static readonly Guid StandardRoleId = Guid.NewGuid();

    // constant IDs for categories
    public static readonly Dictionary<string, Guid> CategoryIds = new()
    {
        {CategoryNames.Case, Guid.NewGuid()},
        {CategoryNames.Motherboard, Guid.NewGuid()}, 
        {CategoryNames.Processor, Guid.NewGuid()}, 
        {CategoryNames.CpuCooler, Guid.NewGuid()},
        {CategoryNames.GraphicsCard, Guid.NewGuid()},
        {CategoryNames.Memory, Guid.NewGuid()},
        {CategoryNames.SolidStateDrive, Guid.NewGuid()},
        {CategoryNames.HardDrive, Guid.NewGuid()},
        {CategoryNames.PowerSupply, Guid.NewGuid()},
        {CategoryNames.OperatingSystem, Guid.NewGuid()},
        {CategoryNames.PrebuiltPc, Guid.NewGuid()},
        {CategoryNames.CustomPc, Guid.NewGuid()},
        {CategoryNames.TemplatePc, Guid.NewGuid()}
    };

    // constant IDs for PC builds
    internal static readonly Guid PcTemplate1 = Guid.NewGuid();
    internal static readonly Guid PcTemplate2 = Guid.NewGuid();
    internal static readonly Guid PcTemplate3 = Guid.NewGuid();
    internal static readonly Guid PcTemplate4 = Guid.NewGuid();
    internal static readonly Guid PcTemplate5 = Guid.NewGuid();
    internal static readonly Guid PcTemplate6 = Guid.NewGuid();
    internal static readonly Guid PcPrebuilt1 = Guid.NewGuid();
    internal static readonly Guid PcPrebuilt2 = Guid.NewGuid();
    internal static readonly Guid PcPrebuilt3 = Guid.NewGuid();
    internal static readonly Guid PcPrebuilt4 = Guid.NewGuid();
    internal static readonly Guid PcPrebuilt5 = Guid.NewGuid();
    internal static readonly Guid PcPrebuilt6 = Guid.NewGuid();
    internal static readonly Guid PcPrebuilt7 = Guid.NewGuid();
    internal static readonly Guid PcPrebuilt8 = Guid.NewGuid();
    internal static readonly Guid PcPrebuilt9 = Guid.NewGuid();
    internal static readonly Guid PcPrebuilt10 = Guid.NewGuid();
    internal static readonly Guid PcPrebuilt11 = Guid.NewGuid();

    // constant IDs for manufacturers
    internal static readonly Dictionary<string, Guid> ManufacturerIds = new()
    {
        {"Intel", Guid.NewGuid()}, {"AMD", Guid.NewGuid()}, {"Nvidia", Guid.NewGuid()},
        {"Corsair", Guid.NewGuid()}, {"Seagate", Guid.NewGuid()}, {"Samsung", Guid.NewGuid()},
        {"Cooler Master", Guid.NewGuid()}, {"NZXT", Guid.NewGuid()}, {"Noctua", Guid.NewGuid()},
        {"be quiet!", Guid.NewGuid()}, {"G.Skill", Guid.NewGuid()}, {"Kingston", Guid.NewGuid()},
        {"MSI", Guid.NewGuid()}, {"ASRock", Guid.NewGuid()}, {"ASUS", Guid.NewGuid()},
        {"GIGABYTE", Guid.NewGuid()}, {"Zotac", Guid.NewGuid()}, {"Thermaltake", Guid.NewGuid()},
        {"EVGA", Guid.NewGuid()}, {"Silverstone", Guid.NewGuid()}, {"Microsoft", Guid.NewGuid()},
        {"ADATA", Guid.NewGuid()}, {"DeepCool", Guid.NewGuid()}, {"APEVIA", Guid.NewGuid()},
        {"XFX", Guid.NewGuid()}, {"Western Digital", Guid.NewGuid()}
    };
    
    // constant ID for statuses
    public static readonly Guid CancelledStatusId = Guid.NewGuid();
    public static readonly Guid CompletedStatusId = Guid.NewGuid();
    internal static readonly Guid PlacedStatusId = Guid.NewGuid();
    
    // constant ID for shipping method
    internal static readonly Guid StandardShippingId = Guid.NewGuid();
    internal static readonly Guid ExpressShippingId = Guid.NewGuid();
    
    // constant IDs for package sizes
    internal static readonly Guid SmallSizeId = Guid.NewGuid();
    internal static readonly Guid MediumSizeId = Guid.NewGuid();
    internal static readonly Guid LargeSizeId = Guid.NewGuid();
    
    // constant ID for discount
    internal static readonly Guid Discount0 = Guid.NewGuid();
    internal static readonly Guid Discount10 = Guid.NewGuid();
    internal static readonly Guid Discount20 = Guid.NewGuid();
    internal static readonly Guid Discount30 = Guid.NewGuid();
    internal static readonly Guid Discount40 = Guid.NewGuid();
    internal static readonly Guid Discount50 = Guid.NewGuid();
    
    // constant IDs for attributes
    internal static readonly Dictionary<string, Guid> AttributeIds = new()
    {
        // Case
        {AttributeNames.Color, Guid.NewGuid()},
        {AttributeNames.SidePanel, Guid.NewGuid()},
        {AttributeNames.SuppMotherboards, Guid.NewGuid()},
        
        // Motherboard
        {AttributeNames.SuppMemory, Guid.NewGuid()},
        {AttributeNames.Slots, Guid.NewGuid()},

        // CPU
        {AttributeNames.CoreCount, Guid.NewGuid()},
        {AttributeNames.Series, Guid.NewGuid()},
        {AttributeNames.Architecture, Guid.NewGuid()}, 
        
        // CPU Cooler
        {AttributeNames.WaterCooled, Guid.NewGuid()},
        {AttributeNames.Rpm, Guid.NewGuid()},
        
        // Memory
        {AttributeNames.Modules, Guid.NewGuid()}, 
        {AttributeNames.Speed, Guid.NewGuid()},
        
        // Power Supply
        {AttributeNames.Wattage, Guid.NewGuid()},
        {AttributeNames.Efficiency, Guid.NewGuid()},
        
        // Motherboard, CPU
        {AttributeNames.Socket, Guid.NewGuid()},
        {AttributeNames.MemCapacity, Guid.NewGuid()}, 
        
        // Memory, GPU
        {AttributeNames.Memory, Guid.NewGuid()},
        {AttributeNames.MemType, Guid.NewGuid()},
        
        // CPU, GPU
        {AttributeNames.CoreClock, Guid.NewGuid()},
        {AttributeNames.BoostClock, Guid.NewGuid()},
        {AttributeNames.Tdp, Guid.NewGuid()},

        // SSD, HDD
        {AttributeNames.Capacity, Guid.NewGuid()},
        {AttributeNames.Interface, Guid.NewGuid()},

        // Motherboard, CPU, GPU
        {AttributeNames.Chipset, Guid.NewGuid()},
        
        // SSD, HDD, Case
        {AttributeNames.Type, Guid.NewGuid()},
        
        // SSD, HDD, Motherboard
        {AttributeNames.FormFactor, Guid.NewGuid()}
    };
    
    // constant ID for shipping cost
    internal static readonly Guid ShippingCostId = Guid.NewGuid();
}