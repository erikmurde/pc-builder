using DAL.EF.App.Seeding.Names;
using Domain.App;

namespace DAL.EF.App.Seeding;

public class AppDataSeedingComponentAttribute
{
    private readonly ApplicationDbContext _context;
    
    public AppDataSeedingComponentAttribute(ApplicationDbContext context)
    {
        _context = context;
    }
    
    internal void SeedAppDataComponentAttributes()
    {
        SeedCaCase();
        SeedCaMotherboard();
        SeedCaProcessor();
        SeedCaCpuCooler();
        SeedCaMemory();
        SeedCaGraphicsCard();
        SeedCaStorage();
        SeedCaPowerSupply();
    }

    private void SeedCaCase()
    {
        const string black = "Black";
        const string white = "White";
        
        AddCaCase(AppDataComponentIds.Case1, black);
        AddCaCase(AppDataComponentIds.Case2, white);
        AddCaCase(AppDataComponentIds.Case3, black);
        AddCaCase(AppDataComponentIds.Case4, white);
        AddCaCase(AppDataComponentIds.Case5, black);
        AddCaCase(AppDataComponentIds.Case6, white);
        AddCaCase(AppDataComponentIds.Case7, white);
        AddCaCase(AppDataComponentIds.Case8, black, 
            "Tinted Tempered Glass");
        AddCaCase(AppDataComponentIds.Case9, white);
        AddCaCase(AppDataComponentIds.Case10, white); 
        AddCaCase(AppDataComponentIds.Case11, black, 
            suppMotherboards: "EATX, ATX, mATX", type: "ATX Full Tower"); 
    }

    private void SeedCaMotherboard()
    {
        AddCaMotherboard(AppDataComponentIds.MotherboardIntel1, "DDR4", "LGA1700", "Z690");
        AddCaMotherboard(AppDataComponentIds.MotherboardIntel2, "DDR5", "LGA1700", "Z790");
        AddCaMotherboard(AppDataComponentIds.MotherboardIntel3, "DDR5", "LGA1700", "Z790");
        AddCaMotherboard(AppDataComponentIds.MotherboardIntel4, "DDR5", "LGA1700", "Z790");
        AddCaMotherboard(AppDataComponentIds.MotherboardIntel5, "DDR5", "LGA1700", "Z790");
        AddCaMotherboard(AppDataComponentIds.MotherboardIntel6, "DDR5", "LGA1700", "Z790", 
            formFactor: "EATX");
        AddCaMotherboard(AppDataComponentIds.MotherboardIntel7, "DDR5", "LGA1700", "Z790", 
            formFactor: "EATX");
        AddCaMotherboard(AppDataComponentIds.MotherboardAmd1, "DDR4", "AM4", "B550");
        AddCaMotherboard(AppDataComponentIds.MotherboardAmd2, "DDR4", "AM4", "X570");
        AddCaMotherboard(AppDataComponentIds.MotherboardAmd3, "DDR5", "AM5", "B650");
        AddCaMotherboard(AppDataComponentIds.MotherboardAmd4, "DDR5", "AM5", "B650");
        AddCaMotherboard(AppDataComponentIds.MotherboardAmd5, "DDR5", "AM5", "X670E");
    }

    private void SeedCaProcessor()
    {
        AddCaProcessor(AppDataComponentIds.ProcessorIntel1, 
            "4", "Intel Core i3", "Raptor Lake", "LGA1700", 
            "3.4GHz", "4.5GHz", "58W");
        AddCaProcessor(AppDataComponentIds.ProcessorIntel2, 
            "10", "Intel Core i5", "Raptor Lake", "LGA1700", 
            "2.5GHz", "4.6GHz", "65W");
        AddCaProcessor(AppDataComponentIds.ProcessorIntel3, 
            "14", "Intel Core i5", "Raptor Lake", "LGA1700", 
            "3.5GHz", "5.1GHz", "125W");
        AddCaProcessor(AppDataComponentIds.ProcessorIntel4, 
            "16", "Intel Core i7", "Raptor Lake", "LGA1700", 
            "3.4GHz", "5.4GHz", "125W");
        AddCaProcessor(AppDataComponentIds.ProcessorIntel5, 
            "24", "Intel Core i9", "Raptor Lake", "LGA1700", 
            "3GHz", "5.8GHz", "125W");
        AddCaProcessor(AppDataComponentIds.ProcessorAmd1, 
            "6", "AMD Ryzen 5", "Zen 3", "AM4", 
            "3.5GHz", "4.4GHz", "65W");
        AddCaProcessor(AppDataComponentIds.ProcessorAmd2, 
            "8", "AMD Ryzen 7", "Zen 3", "AM4", 
            "3.4GHz", "4.6GHz", "65W");
        AddCaProcessor(AppDataComponentIds.ProcessorAmd3, 
            "6", "AMD Ryzen 5", "Zen 4", "AM5", 
            "4.7GHz", "5.3GHz", "105W");
        AddCaProcessor(AppDataComponentIds.ProcessorAmd4, 
            "8", "AMD Ryzen 7", "Zen 4", "AM5", 
            "4.5GHz", "5.4GHz", "105W");
        AddCaProcessor(AppDataComponentIds.ProcessorAmd5, 
            "12", "AMD Ryzen 9", "Zen 4", "AM5", 
            "4.7GHz", "5.6GHz", "170W");
    }
    
    private void SeedCaCpuCooler()
    {
        AddCaCpuCooler(AppDataComponentIds.CpuCoolerAir1, "No", "650 - 2000 RPM");
        AddCaCpuCooler(AppDataComponentIds.CpuCoolerAir2, "No", "500 - 1850 RPM");
        AddCaCpuCooler(AppDataComponentIds.CpuCoolerLiquid1, "Yes - 240mm", "400 - 1850 RPM");
        AddCaCpuCooler(AppDataComponentIds.CpuCoolerLiquid2, "Yes - 240mm", "500 - 1500 RPM");
        AddCaCpuCooler(AppDataComponentIds.CpuCoolerLiquid3, "Yes - 240mm", "500 - 2250 RPM");
        AddCaCpuCooler(AppDataComponentIds.CpuCoolerLiquid4, "Yes - 280mm", "500 - 2000 RPM");
    }
    
    private void SeedCaMemory()
    {
        AddCaMemory(AppDataComponentIds.Memory1, "2 x 8GB", "3200MHz", "16GB", "DDR4");
        AddCaMemory(AppDataComponentIds.Memory2, "4 x 8GB", "3200MHz", "32GB", "DDR4");
        AddCaMemory(AppDataComponentIds.Memory3, "2 X 8GB", "5200MHz", "16GB", "DDR5");
        AddCaMemory(AppDataComponentIds.Memory4, "4 X 8GB", "5200MHz", "32GB", "DDR5");
        AddCaMemory(AppDataComponentIds.Memory5, "2 x 8GB", "3600MHz", "16GB", "DDR4");
        AddCaMemory(AppDataComponentIds.Memory6, "4 x 8GB", "3600MHz", "32GB", "DDR4");
        AddCaMemory(AppDataComponentIds.Memory7, "2 X 16GB", "6000MHz", "32GB", "DDR5");
        AddCaMemory(AppDataComponentIds.Memory8, "2 X 32GB", "6000MHz", "64GB", "DDR5");
        AddCaMemory(AppDataComponentIds.Memory9, "4 X 32GB", "6000MHz", "128GB", "DDR5");
    }
    
    private void SeedCaGraphicsCard()
    {
        AddCaGraphicsCard(AppDataComponentIds.GraphicsCardNvidia1, 
            "24GB", "GDDR6X", "2235MHz", "2640MHz", "450W", "GeForce RTX 4090");
        AddCaGraphicsCard(AppDataComponentIds.GraphicsCardNvidia2, 
            "12GB", "GDDR6X", "2475MHz", "2505MHz", "200W", "GeForce RTX 4070");
        AddCaGraphicsCard(AppDataComponentIds.GraphicsCardNvidia3, 
            "6GB", "GDDR6", "1530MHz", "1815MHz", "125W", "GeForce GTX 1660 SUPER");
        AddCaGraphicsCard(AppDataComponentIds.GraphicsCardNvidia4, 
            "12GB", "GDDR6", "1320MHz", "1837MHz", "170W", "GeForce RTX 3060 12GB");
        AddCaGraphicsCard(AppDataComponentIds.GraphicsCardNvidia5, 
            "12GB", "GDDR6X", "1920MHz", "2490MHz", "200W", "GeForce RTX 4070");
        AddCaGraphicsCard(AppDataComponentIds.GraphicsCardNvidia6, 
            "8GB", "GDDR6", "1500MHz", "1815MHz", "220W", "GeForce RTX 3070");
        AddCaGraphicsCard(AppDataComponentIds.GraphicsCardAmd1, 
            "8GB", "GDDR6", "1626MHz", "None", "132W", "Radeon RX 6600");
        AddCaGraphicsCard(AppDataComponentIds.GraphicsCardAmd2, 
            "24GB", "GDDR6", "2300MHz", "2615MHz", "355W", "Radeon RX 7900 XTX");
        AddCaGraphicsCard(AppDataComponentIds.GraphicsCardAmd3, 
            "16GB", "GDDR6", "1825MHz", "2250MHz", "300W", "Radeon RX 6800 XT");
        AddCaGraphicsCard(AppDataComponentIds.GraphicsCardAmd4, 
            "8GB", "GDDR6", "1968MHz", "2602MHz", "160W", "Radeon RX 6600 XT");
        AddCaGraphicsCard(AppDataComponentIds.GraphicsCardIntel1, 
            "16GB", "GDDR6", "2100MHz", "None", "225W", "Arc A770");
        AddCaGraphicsCard(AppDataComponentIds.GraphicsCardIntel2, 
            "8GB", "GDDR6", "2050MHz", "None", "225W", "Arc A750");
    }
    
    private void SeedCaStorage()
    {
        AddCaStorage(AppDataComponentIds.PrimaryStorage1, 
            "1TB", "NVME SSD", "M.2 PCIe 3.0 X4", "M.2-2280");
        AddCaStorage(AppDataComponentIds.PrimaryStorage2, 
            "1TB", "NVME SSD", "M.2 PCIe 3.0 X4", "M.2-2280");
        AddCaStorage(AppDataComponentIds.PrimaryStorage3, 
            "2TB", "NVME SSD", "M.2 PCIe 3.0 X4", "M.2-2280");
        AddCaStorage(AppDataComponentIds.PrimaryStorage4, 
            "1TB", "NVME SSD", "M.2 PCIe 4.0 X4", "M.2-2280");
        AddCaStorage(AppDataComponentIds.PrimaryStorage5, 
            "2TB", "NVME SSD", "M.2 PCIe 4.0 X4", "M.2-2280");
        AddCaStorage(AppDataComponentIds.PrimaryStorage6, 
            "1TB", "NVME SSD", "M.2 PCIe 4.0 X4", "M.2-2280");
        
        AddCaStorage(AppDataComponentIds.SecondaryStorage1, 
            "2TB", "7200RPM HDD", "SATA 6.0GB/s", "3.5\"");
        AddCaStorage(AppDataComponentIds.SecondaryStorage2, 
            "4TB", "5400RPM HDD", "SATA 6.0GB/s", "3.5\"");
        AddCaStorage(AppDataComponentIds.SecondaryStorage3, 
            "4TB", "5400RPM HDD", "SATA 6.0GB/s", "3.5\"");
        AddCaStorage(AppDataComponentIds.SecondaryStorage4, 
            "10TB", "7200RPM HDD", "SATA 6.0GB/s", "3.5\"");
        AddCaStorage(AppDataComponentIds.SecondaryStorage5, 
            "4TB", "7200RPM HDD", "SATA 6.0GB/s", "3.5\"");
        AddCaStorage(AppDataComponentIds.SecondaryStorage6, 
            "6TB", "7200RPM HDD", "SATA 6.0GB/s", "3.5\"");
    }
    
    private void SeedCaPowerSupply()
    {
        AddCaPowerSupply(AppDataComponentIds.PowerSupply1, "600W", "80+");
        AddCaPowerSupply(AppDataComponentIds.PowerSupply2, "750W", "80+ Gold");
        AddCaPowerSupply(AppDataComponentIds.PowerSupply3, "850W", "80+ Gold");
        AddCaPowerSupply(AppDataComponentIds.PowerSupply4, "850W", "80+ Gold");
        AddCaPowerSupply(AppDataComponentIds.PowerSupply5, "1000W", "80+ Gold");
        AddCaPowerSupply(AppDataComponentIds.PowerSupply6, "1000W", "80+ Gold");
    }
    
    private void AddCaCase(Guid componentId, string color, 
        string sidePanel = "Tempered Glass", string type = "ATX Mid Tower", string suppMotherboards = "ATX, mATX")
    {
        AddCa(componentId, AttributeNames.Type, type);
        AddCa(componentId, AttributeNames.Color, color);
        AddCa(componentId, AttributeNames.SidePanel, sidePanel);
        AddCa(componentId, AttributeNames.SuppMotherboards, suppMotherboards);
    }
    
    private void AddCaMotherboard(Guid componentId, 
        string supportedMemory, string socket, string chipset, 
        string capacity = "128GB", string formFactor = "ATX",  string slots = "4")
    {
        AddCa(componentId, AttributeNames.SuppMemory, supportedMemory);
        AddCa(componentId, AttributeNames.Slots, slots);
        AddCa(componentId, AttributeNames.Socket, socket);
        AddCa(componentId, AttributeNames.MemCapacity, capacity);
        AddCa(componentId, AttributeNames.Chipset, chipset);
        AddCa(componentId, AttributeNames.FormFactor, formFactor);
    }

    private void AddCaProcessor(Guid componentId, string coreCount, string series, string architecture, 
        string socket, string coreClc, string boostClc, string tdp, string capacity = "128GB")
    {
        AddCa(componentId, AttributeNames.CoreCount, coreCount);
        AddCa(componentId, AttributeNames.Series, series);
        AddCa(componentId, AttributeNames.Architecture, architecture);
        AddCa(componentId, AttributeNames.Socket, socket);
        AddCa(componentId, AttributeNames.MemCapacity, capacity);
        AddCa(componentId, AttributeNames.CoreClock, coreClc);
        AddCa(componentId, AttributeNames.BoostClock, boostClc);
        AddCa(componentId, AttributeNames.Tdp, tdp);
    }
    
    private void AddCaCpuCooler(Guid componentId, string waterCooled, string rpm)
    {
        AddCa(componentId, AttributeNames.WaterCooled, waterCooled);
        AddCa(componentId, AttributeNames.Rpm, rpm);
    }
    
    private void AddCaMemory(Guid componentId, string modules, string speed, string memory, string memType)
    {
        AddCa(componentId, AttributeNames.Modules, modules);
        AddCa(componentId, AttributeNames.Speed, speed);
        AddCa(componentId, AttributeNames.Memory, memory);
        AddCa(componentId, AttributeNames.MemType, memType);
    }
    
    private void AddCaGraphicsCard(Guid componentId, string memory, string memType, string coreClc, 
        string boostClc, string tdp, string chipset)
    {
        AddCa(componentId, AttributeNames.Memory, memory);
        AddCa(componentId, AttributeNames.MemType, memType);
        AddCa(componentId, AttributeNames.CoreClock, coreClc);
        AddCa(componentId, AttributeNames.BoostClock, boostClc);
        AddCa(componentId, AttributeNames.Tdp, tdp);
        AddCa(componentId, AttributeNames.Chipset, chipset);
    }
    
    private void AddCaStorage(Guid componentId, string capacity, string type, string intType, string formFactor)
    {
        AddCa(componentId, AttributeNames.MemCapacity, capacity);   
        AddCa(componentId, AttributeNames.Type, type);   
        AddCa(componentId, AttributeNames.Interface, intType);   
        AddCa(componentId, AttributeNames.FormFactor, formFactor);   
    }

    private void AddCaPowerSupply(Guid componentId, string wattage, string efficiency)
    {
        AddCa(componentId, AttributeNames.Wattage, wattage);
        AddCa(componentId, AttributeNames.Efficiency, efficiency);
    }

    private void AddCa(Guid componentId, string attribute, string value)
    {
        _context.ComponentAttributes.Add(new ComponentAttribute
        {
            ComponentId = componentId, 
            AttributeId = AppDataIds.AttributeIds[attribute],
            AttributeValue = value
        });
    }
}