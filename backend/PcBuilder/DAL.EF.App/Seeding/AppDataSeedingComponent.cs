using Domain.App;

namespace DAL.EF.App.Seeding;

public class AppDataSeedingComponent
{
    private readonly ApplicationDbContext _context;
    
    public AppDataSeedingComponent(ApplicationDbContext context)
    {
        _context = context;
    }
    
    internal void SeedAppDataComponents()
    {
        SeedAppDataCases();
        SeedAppDataMotherboards();
        SeedAppDataProcessors();
        SeedAppDataCpuCoolers();
        SeedAppDataMemory();
        SeedAppDataGraphicsCards();
        SeedAppDataPrimaryStorage();
        SeedAppDataSecondaryStorage();
        SeedAppDataPowerSupplies();
        SeedAppDataOperatingSystems();
    }

    private void SeedAppDataCases()
    {
        const string cat = "Case";
        
        AddComponent(AppDataComponentIds.Case1, cat, "ADATA", 
            "ADATA XPG INVADER", "Case 1", 79.99m, 
            "https://content.ibuypower.com/Images/Components/21858/gaming-pc-01-invader-main-175.png");
        
        AddComponent(AppDataComponentIds.Case2, cat, "ADATA", 
            "ADATA XPG INVADER", "Case 2", 79.99m,
            "https://content.ibuypower.com/Images/Components/21909/gaming-pc-01-invader-main-175-.png");
        
        AddComponent(AppDataComponentIds.Case3, cat, "be quiet!", 
            "be quiet! Pure Base 500DX", "Case 3", 102.99m, 
            "https://content.ibuypower.com/Images/Components/21839/gaming-pc-01-500DX-main-175.png");
        
        AddComponent(AppDataComponentIds.Case4, cat, "be quiet!", 
            "be quiet! Pure Base 500DX", "Case 4", 102.99m, 
            "https://content.ibuypower.com/Images/Components/22087/gaming-pc-01-500DX-main-175-v3.png");
        
        AddComponent(AppDataComponentIds.Case5, cat, "NZXT", 
            "NZXT H7 Flow", "Case 5", 122.99m, 
            "https://content.ibuypower.com/Images/Components/25755/gaming-pc-01-H7-Flow-Black-main-solo-175.png");
        
        AddComponent(AppDataComponentIds.Case6, cat, "NZXT", 
            "NZXT H7 Flow", "Case 6", 122.99m, 
            "https://content.ibuypower.com/Images/Components/25756/gaming-pc-01-H7-Flow-white-main-solo-175.png");
        
        AddComponent(AppDataComponentIds.Case7, cat, "Corsair", 
            "Corsair 4000D", "Case 7", 113.99m, 
            "https://www.cyberpowerpc.com/images/cs/corsair4000d/cs-429-143_220.png?v1");
        
        AddComponent(AppDataComponentIds.Case8, cat, "Corsair", 
            "Corsair 4000D Airflow", "Case 8", 113.99m, 
            "https://www.cyberpowerpc.com/images/cs/corsair4000dair/cs-429-144_220.png?v1");
        
        AddComponent(AppDataComponentIds.Case9, cat, "NZXT", 
            "NZXT H510", "Case 9", 103.99m, 
            "https://www.cyberpowerpc.com/images/cs/H510/CS-211-222_220.png");
        
        AddComponent(AppDataComponentIds.Case10, cat, "APEVIA", 
            "APEVIA DESTINY-FLOW", "Case 10", 103.99m, 
            "https://www.cyberpowerpc.com/images/cs/DESTINYFLOW/CS-428-310_220.png");
        
        AddComponent(AppDataComponentIds.Case11, cat, "Corsair", 
            "Corsair iCUE 7000X RGB", "Case 11", 229.99m, 
            "https://www.cyberpowerpc.com/images/cs/corsair7000x/cs-429-154_400.png");
    }

    private void SeedAppDataMotherboards()
    {
        const string cat = "Motherboard";
        
        AddComponent(AppDataComponentIds.MotherboardIntel1, cat, "MSI", 
            "MSI PRO Z690-A", "Intel motherboard 1", 189.99m,
            "https://content.ibuypower.com/Images/Components/23589/MSI-PRO-Z690-A-WIFI-DDR4-400.png");
      
        AddComponent(AppDataComponentIds.MotherboardIntel2, cat, "MSI", 
            "MSI MPG Z790 Edge", "Intel motherboard 2", 290.99m,
            "https://content.ibuypower.com/Images/Components/26638/MSI-MPG-Z790-EDGE-WIFI-DDR4-400.png");
        
        AddComponent(AppDataComponentIds.MotherboardIntel3, cat, "ASRock", 
            "ASRock Z790 Steel Legend", "Intel motherboard 3", 302.99m,
            "https://content.ibuypower.com/Images/Components/25957/Asrock-Z790-Steel-Legend-Wifi-400.png");
        
        AddComponent(AppDataComponentIds.MotherboardIntel4, cat, "ASUS", 
            "ASUS Prime Z790-A", "Intel motherboard 4", 372.99m,
            "https://content.ibuypower.com/Images/Components/25962/Asus-Prime-Z790-A-WiFi-400.png");
        
        AddComponent(AppDataComponentIds.MotherboardIntel5, cat, "GIGABYTE", 
            "GIGABYTE Z790 AERO G", "Intel motherboard 5", 289.99m,
            "https://www.cyberpowerpc.com/images/mb/MB-492-261_220.png");
        
        AddComponent(AppDataComponentIds.MotherboardIntel6, cat, "ASRock", 
            "ASRock Z790 Taichi Carrara", "Intel motherboard 6", 499.99m,
            "https://www.cyberpowerpc.com/images/mb/MB-492-224_220.png?v1");
        
        AddComponent(AppDataComponentIds.MotherboardIntel7, cat, "GIGABYTE", 
            "GIGABYTE Z790 AORUS MASTER", "Intel motherboard 7", 486.99m,
            "https://www.cyberpowerpc.com/images/mb/MB-492-260_220.png");
        
        AddComponent(AppDataComponentIds.MotherboardAmd1, cat, "MSI", 
            "MSI PRO B550-VC", "AMD motherboard 1", 150.99m,
            "https://content.ibuypower.com/Images/Components/26473/MSI-B550-A-PRO-CEC-400..png");
        
        AddComponent(AppDataComponentIds.MotherboardAmd2, cat, "ASUS", 
            "ASUS PRIME X570-PRO", "AMD motherboard 2", 320.99m,
            "https://content.ibuypower.com/Images/Components/23368/ASUS-PRIME-X570-PRO-ES-400.png");
        
        AddComponent(AppDataComponentIds.MotherboardAmd3, cat, "ASUS", 
            "ASUS ROG STRIX B650-A GAMING", "AMD motherboard 3", 259.99m,
            "https://www.cyberpowerpc.com/images/mb/MB-492-123_220.png");
        
        AddComponent(AppDataComponentIds.MotherboardAmd4, cat, "GIGABYTE", 
            "GIGABYTE B650 AORUS ELITE AX", "AMD motherboard 4", 199.99m,
            "https://www.cyberpowerpc.com/images/mb/MB-492-132_220.png?v2");
        
        AddComponent(AppDataComponentIds.MotherboardAmd5, cat, "MSI", 
            "MSI MPG X670E CARBON", "AMD motherboard 5", 372.99m,
            "https://www.cyberpowerpc.com/images/mb/MB-491-303_220.png");
    }
    
    private void SeedAppDataProcessors()
    {
        const string cat = "Processor";
        
        AddComponent(AppDataComponentIds.ProcessorIntel1, cat, "Intel", 
            "Intel Core i3-13100F", "Intel processor 1", 110.99m,
            "https://content.ibuypower.com/Images/Components/26457/11th-Gen-i3-400.png");
        
        AddComponent(AppDataComponentIds.ProcessorIntel2, cat, "Intel", 
            "Intel Core i5-13400F", "Intel processor 2", 199.99m,
            "https://content.ibuypower.com/Images/Components/26298/11th-Gen-i5-400-.png");
        
        AddComponent(AppDataComponentIds.ProcessorIntel3, cat, "Intel", 
            "Intel Core i5-13600K", "Intel processor 3", 320.99m,
            "https://content.ibuypower.com/Images/Components/26298/11th-Gen-i5-400-.png");
        
        AddComponent(AppDataComponentIds.ProcessorIntel4, cat, "Intel", 
            "Intel Core i7-13700K", "Intel processor 4", 430.99m,
            "https://content.ibuypower.com/Images/Components/25881/11th-Gen-i7-400.png");
        
        AddComponent(AppDataComponentIds.ProcessorIntel5, cat, "Intel", 
            "Intel Core i9-13900K", "Intel processor 5", 578.99m,
            "https://content.ibuypower.com/Images/Components/25896/11th-Gen-i9-400.png");
        
        AddComponent(AppDataComponentIds.ProcessorAmd1, cat, "AMD", 
            "AMD Ryzen 5 5600", "AMD processor 1", 136.99m,
            "https://content.ibuypower.com/Images/Components/25463/Ryzen-5-400.png");
        
        AddComponent(AppDataComponentIds.ProcessorAmd2, cat, "AMD", 
            "AMD Ryzen 7 5700X", "AMD processor 2", 233.99m,
            "https://content.ibuypower.com/Images/Components/25462/Ryzen-7-400.png");
        
        AddComponent(AppDataComponentIds.ProcessorAmd3, cat, "AMD", 
            "AMD Ryzen 5 7600X", "AMD processor 3", 238.99m,
            "https://content.ibuypower.com/Images/Components/25463/Ryzen-5-400.png");
        
        AddComponent(AppDataComponentIds.ProcessorAmd4, cat, "AMD", 
            "AMD Ryzen 7 7700X", "AMD processor 4", 329.99m,
            "https://content.ibuypower.com/Images/Components/25462/Ryzen-7-400.png");
        
        AddComponent(AppDataComponentIds.ProcessorAmd5, cat, "AMD", 
            "AMD Ryzen 9 7900X", "AMD processor 5", 416.99m,
            "https://content.ibuypower.com/Images/Components/25873/Ryzen-9-400-.png");
    }
    
    private void SeedAppDataCpuCoolers()
    {
        const string cat = "CPU Cooler";
        
        AddComponent(AppDataComponentIds.CpuCoolerAir1, cat, "Cooler Master", 
            "Cooler Master Hyper 212 RGB Black Edition", "CPU air cooler 1", 99.99m,
            "https://www.cyberpowerpc.com/images/fa/fa-205-119_220.png");
        
        AddComponent(AppDataComponentIds.CpuCoolerAir2, cat, "DeepCool", 
            "DeepCool AK400 Zero Dark", "CPU air cooler 2", 42.99m,
            "https://ironsidecomputers.com/wp-content/uploads/2023/02/Deepcool-Fan.png");
        
        AddComponent(AppDataComponentIds.CpuCoolerLiquid1, cat, "Corsair", 
            "Corsair iCUE H100i RGB ELITE", "CPU liquid cooler 1", 149.99m,
            "https://content.ibuypower.com/Images/Components/25858/Corsair-ICUE-H100i-RGB-ELITE-240mm-400.png");
        
        AddComponent(AppDataComponentIds.CpuCoolerLiquid2, cat, "NZXT", 
            "NZXT Kraken X53 RGB", "CPU liquid cooler 2", 174.99m,
            "https://content.ibuypower.com/Images/Components/22871/NZXT-KRAKEN-X53-RGB-400.png");
        
        AddComponent(AppDataComponentIds.CpuCoolerLiquid3, cat, "DeepCool", 
            "DeepCool LS520 ARGB", "CPU liquid cooler 3", 124.99m,
            "https://content.ibuypower.com/Images/Components/26097/DEEPCOOL-240MM-LS520-400.png");
        
        AddComponent(AppDataComponentIds.CpuCoolerLiquid4, cat, "NZXT", 
            "NZXT Kraken X63 RGB", "CPU liquid cooler 4", 154.99m,
            "https://content.ibuypower.com/Images/Components/23515/NZXT-KRAKEN-X63-RGB-280MM-Black-400.png");
    }
    
    private void SeedAppDataMemory()
    {
        const string cat = "Memory";

        AddComponent(AppDataComponentIds.Memory1, cat, "Kingston",
            "Kingston Fury Beast 16GB DDR4 3200", "Memory 1", 69.99m,
            "https://ironsidecomputers.com/wp-content/uploads/2017/06/Kingston-DDR4-2-stick-330x330-1.png");
        
        AddComponent(AppDataComponentIds.Memory2, cat, "Kingston",
            "Kingston Fury Beast 32GB DDR4 3200", "Memory 2", 123.99m,
            "https://ironsidecomputers.com/wp-content/uploads/2017/06/Kingston-DDR4-4-stick-330x330-1.png");
        
        AddComponent(AppDataComponentIds.Memory3, cat, "Kingston",
            "Kingston Fury Beast 16GB DDR5 5200", "Memory 3", 99.99m,
            "https://ironsidecomputers.com/wp-content/uploads/2021/11/Kingston-DDR5-2-stick-330x330-1.png");
        
        AddComponent(AppDataComponentIds.Memory4, cat, "Kingston",
            "Kingston Fury Beast 32GB DDR5 5200", "Memory 4", 158.99m,
            "https://ironsidecomputers.com/wp-content/uploads/2021/11/Kingston-DDR5-4-stick-330x330-1.png");

        AddComponent(AppDataComponentIds.Memory5, cat, "G.Skill",
            "G.Skill Trident Z RGB 16GB DDR4 3600", "Memory 5", 79.99m,
            "https://ironsidecomputers.com/wp-content/uploads/2017/06/TridentZRGB2sticks.png");
        
        AddComponent(AppDataComponentIds.Memory6, cat, "G.Skill",
            "G.Skill Trident Z RGB 32GB DDR4 3600", "Memory 6", 159.99m,
            "https://ironsidecomputers.com/wp-content/uploads/2017/06/TridentZRGB4sticks16.png");
        
        AddComponent(AppDataComponentIds.Memory7, cat, "Kingston",
            "Kingston Fury Beast RGB 32GB DDR5 6000", "Memory 7", 167.99m,
            "https://ironsidecomputers.com/wp-content/uploads/2022/11/Kingston-RGB-DDR5-2-stick-330x330-1.png");

        AddComponent(AppDataComponentIds.Memory8, cat, "Kingston",
            "Kingston Fury Beast RGB 64GB DDR5 6000", "Memory 8", 278.99m,
            "https://ironsidecomputers.com/wp-content/uploads/2022/11/Kingston-RGB-DDR5-2-stick-330x330-1.png");
        
        AddComponent(AppDataComponentIds.Memory9, cat, "Kingston",
            "Kingston Fury Beast RGB 128GB DDR5 6000", "Memory 9", 512.99m,
            "https://ironsidecomputers.com/wp-content/uploads/2022/11/Kingston-RGB-DDR5-4-stick-330x330-1.png");
    }
    
    private void SeedAppDataGraphicsCards()
    {
        const string cat = "Graphics Card";
        
        AddComponent(AppDataComponentIds.GraphicsCardNvidia1, cat, "ASUS", 
            "ASUS ROG STRIX GAMING OC GeForce RTX 4090", "Nvidia graphics card 1", 1999.99m,
            "nvidia1.png");
        
        AddComponent(AppDataComponentIds.GraphicsCardNvidia2, cat, "ASUS", 
            "ASUS DUAL GeForce RTX 4070", "Nvidia graphics card 2", 599.99m,
            "nvidia2.png");
        
        AddComponent(AppDataComponentIds.GraphicsCardNvidia3, cat, "MSI", 
            "MSI VENTUS XS OC GeForce GTX 1660 SUPER", "Nvidia graphics card 3", 259.99m,
            "nvidia3.png");
        
        AddComponent(AppDataComponentIds.GraphicsCardNvidia4, cat, "MSI", 
            "MSI GeForce RTX 3060 Gaming X12G", "Nvidia graphics card 4", 379.99m,
            "nvidia4.png");
        
        AddComponent(AppDataComponentIds.GraphicsCardNvidia5, cat, "GIGABYTE", 
            "GIGABYTE WINDFORCE OC GeForce RTX 4070", "Nvidia graphics card 5", 599.99m,
            "nvidia5.png");
        
        AddComponent(AppDataComponentIds.GraphicsCardNvidia6, cat, "GIGABYTE", 
            "GIGABYTE GAMING OC GeForce RTX 3070", "Nvidia graphics card 6", 499.99m,
            "nvidia6.png");
        
        AddComponent(AppDataComponentIds.GraphicsCardAmd1, cat, "ASRock", 
            "ASRock Challenger D Radeon RX 6600", "AMD graphics card 1", 219.99m,
            "amd1.png");
        
        AddComponent(AppDataComponentIds.GraphicsCardAmd2, cat, "XFX", 
            "XFX Speedster MERC 310 Radeon RX 7900 XTX", "AMD graphics card 2", 974.99m,
            "amd2.png");
        
        AddComponent(AppDataComponentIds.GraphicsCardAmd3, cat, "XFX", 
            "XFX Speedster MERC 319 CORE Radeon RX 6800 XT", "AMD graphics card 3", 505.99m,
            "amd3.png");
        
        AddComponent(AppDataComponentIds.GraphicsCardAmd4, cat, "MSI", 
            "MSI RX 6600 XT MECH 2X 8G OC", "AMD graphics card 4", 249.99m,
            "amd4.png");
        
        AddComponent(AppDataComponentIds.GraphicsCardIntel1, cat, "Intel", 
            "Intel Limited Edition Arc A770", "Intel graphics card 1", 349.99m,
            "intel1.png");
        
        AddComponent(AppDataComponentIds.GraphicsCardIntel2, cat, "Intel", 
            "Intel Limited Edition Arc A750", "Intel graphics card 2", 229.99m,
            "intel2.png");
    }
    
    private void SeedAppDataPrimaryStorage()
    {
        const string cat = "Solid State Drive";
        
        AddComponent(AppDataComponentIds.PrimaryStorage1, cat, "Samsung", 
            "Samsung 970 Evo Plus 1TB", "Primary storage 1", 59.99m,
            "https://content.ibuypower.com/Images/Components/20089/SAMSUNG-970-EVO-Plus-400-.png");
        
        AddComponent(AppDataComponentIds.PrimaryStorage2, cat, "Western Digital", 
            "WD Blue SN570 1TB", "Primary storage 2", 45.99m,
            "https://content.ibuypower.com/Images/Components/23813/WD-BLUE-SN570-3D-400.png");
        
        AddComponent(AppDataComponentIds.PrimaryStorage3, cat, "Samsung", 
            "Samsung 970 Evo Plus 2TB", "Primary storage 3", 143.99m,
            "https://content.ibuypower.com/Images/Components/20133/SAMSUNG-970-EVO-Plus-400-.png");
        
        AddComponent(AppDataComponentIds.PrimaryStorage4, cat, "ADATA", 
            "ADATA XPG GAMMIX S70 Blade 1TB", "Primary storage 4", 89.99m,
            "https://content.ibuypower.com/Images/Components/23455/ADATA-XPG-GAMMIX-S70-Blade-400.png");
        
        AddComponent(AppDataComponentIds.PrimaryStorage5, cat, "Samsung", 
            "Samsung 990 PRO 2TB", "Primary storage 5", 189.99m,
            "https://content.ibuypower.com/Images/Components/26277/Samsung-990-PRO-400.png");
        
        AddComponent(AppDataComponentIds.PrimaryStorage6, cat, "Corsair", 
            "Corsair MP600 GS 1TB", "Primary storage 6", 64.99m,
            "https://content.ibuypower.com/Images/Components/26376/Corsair-MP600-GS-400,.png");
    }
    
    private void SeedAppDataSecondaryStorage()
    {
        const string cat = "Hard Drive";
        
        AddComponent(AppDataComponentIds.SecondaryStorage1, cat, "Western Digital", 
            "WD Blue 2TB", "Secondary storage 1", 49.99m,
            "https://content.ibuypower.com/Images/Components/26010/WD-Blue-SATA-400-.png");
        
        AddComponent(AppDataComponentIds.SecondaryStorage2, cat, "Seagate", 
            "Seagate Barracuda 4TB", "Secondary storage 2", 70.99m,
            "https://content.ibuypower.com/Images/Components/25556/4TB-Barracuda-400.png");
        
        AddComponent(AppDataComponentIds.SecondaryStorage3, cat, "Western Digital", 
            "WD Blue 4TB", "Secondary storage 3", 73.99m,
            "https://content.ibuypower.com/Images/Components/26276/WD-Blue-SATA-400,.png");
        
        AddComponent(AppDataComponentIds.SecondaryStorage4, cat, "Seagate", 
            "Seagate IronWolf 10TB", "Secondary storage 4", 280.99m,
            "https://content.ibuypower.com/Images/Components/23303/10TB-Seagate-IronWolf-HDD-400.png");
        
        AddComponent(AppDataComponentIds.SecondaryStorage5, cat, "Seagate", 
            "Seagate IronWolf Pro 4TB", "Secondary storage 5", 115.99m,
            "https://www.cyberpowerpc.com/images/hd/hd-403-211_220.png");
        
        AddComponent(AppDataComponentIds.SecondaryStorage6, cat, "Seagate", 
            "Seagate IronWolf Pro 6TB", "Secondary storage 6", 160.99m,
            "https://www.cyberpowerpc.com/images/hd/hd-403-212_220.png");
    }
    
    private void SeedAppDataPowerSupplies()
    {
        const string cat = "Power Supply";
        
        AddComponent(AppDataComponentIds.PowerSupply1, cat, "Thermaltake", 
            "Thermaltake SMART Series 600W", "Power supply 1", 102.99m,
            "https://www.cyberpowerpc.com/images/ps/ps-118-144_220.png");
        
        AddComponent(AppDataComponentIds.PowerSupply2, cat, "Thermaltake", 
            "Thermaltake Toughpower Grand RGB 750W", "Power supply 2", 131.99m,
            "https://content.ibuypower.com/Images/Components/13049/Tt-Toughpower-RGB-750W-400...png");
        
        AddComponent(AppDataComponentIds.PowerSupply3, cat, "Corsair", 
            "Corsair RM850e 850W", "Power supply 3", 161.99m,
            "https://www.cyberpowerpc.com/images/ps/ps-121-259_220.png");
        
        AddComponent(AppDataComponentIds.PowerSupply4, cat, "Corsair", 
            "Corsair RM850x 850W", "Power supply 4", 188.99m,
            "https://www.cyberpowerpc.com/images/ps/ps-121-257_220.png");
        
        AddComponent(AppDataComponentIds.PowerSupply5, cat, "Corsair", 
            "Corsair RM1000x 1000W", "Power supply 5", 199.99m,
            "https://www.cyberpowerpc.com/images/ps/ps-121-140_200.png");
        
        AddComponent(AppDataComponentIds.PowerSupply6, cat, "EVGA", 
            "EVGA SuperNOVA 1000 GT 1000W", "Power supply 6", 193.99m,
            "https://www.cyberpowerpc.com/images/ps/PS-305-124_220.png");
    }
    
    private void SeedAppDataOperatingSystems()
    {
        const string cat = "Operating System";
        
        AddComponent(AppDataComponentIds.OperatingSystemId, cat, "Microsoft", 
            "Microsoft Windows 11", "Test operating system", 139.00m, 
            "https://content.ibuypower.com/Images/Components/23498/Windows-11-Home-400.png");
    }

    private void AddComponent(
        Guid id, string category, string manufacturer, string name, 
        string description, decimal price, string imageSrc, Guid? discountId = null)
    {
        _context.Components.Add(new Component
        {
            Id = id,
            CategoryId = AppDataIds.CategoryIds[category],
            ManufacturerId = AppDataIds.ManufacturerIds[manufacturer],
            DiscountId = discountId ?? AppDataIds.Discount0,
            ComponentName = name,
            Description = description,
            Price = price,
            Stock = 100,
            ImageSrc = imageSrc
        });
    }
}