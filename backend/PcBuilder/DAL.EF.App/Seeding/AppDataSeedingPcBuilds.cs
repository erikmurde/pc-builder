using DAL.EF.App.Seeding.Names;
using Domain.App;

namespace DAL.EF.App.Seeding;

public class AppDataSeedingPcBuilds
{
    private readonly ApplicationDbContext _context;
    
    public AppDataSeedingPcBuilds(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public void SeedAppDataPcBuilds()
    {
        AddPcBuild(AppDataIds.PcPrebuilt1, AppDataIds.Discount0, CategoryNames.PrebuiltPc, 
            "Prebuilt PC 1", "Prebuilt PC 1 Description", 
            "https://content.ibuypower.com/Images/Components/21858/gaming-pc-01-invader-main-175.png");
        
        AddPcBuild(AppDataIds.PcPrebuilt2, AppDataIds.Discount10, CategoryNames.PrebuiltPc, 
            "Prebuilt PC 2", "Prebuilt PC 2 Description", 
            "https://content.ibuypower.com/Images/Components/21909/gaming-pc-01-invader-main-175-.png");
        
        AddPcBuild(AppDataIds.PcPrebuilt3, AppDataIds.Discount0, CategoryNames.PrebuiltPc, 
            "Prebuilt PC 3", "Prebuilt PC 3 Description", 
            "https://content.ibuypower.com/Images/Components/21839/gaming-pc-01-500DX-main-175.png");
        
        AddPcBuild(AppDataIds.PcPrebuilt4, AppDataIds.Discount20, CategoryNames.PrebuiltPc, 
            "Prebuilt PC 4", "Prebuilt PC 4 Description", 
            "https://content.ibuypower.com/Images/Components/22087/gaming-pc-01-500DX-main-175-v3.png");
        
        AddPcBuild(AppDataIds.PcPrebuilt5, AppDataIds.Discount0, CategoryNames.PrebuiltPc, 
            "Prebuilt PC 5", "Prebuilt PC 5 Description", 
            "https://content.ibuypower.com/Images/Components/25755/gaming-pc-01-H7-Flow-Black-main-solo-175.png");
        
        AddPcBuild(AppDataIds.PcPrebuilt6, AppDataIds.Discount10, CategoryNames.PrebuiltPc, 
            "Prebuilt PC 6", "Prebuilt PC 6 Description", 
            "https://content.ibuypower.com/Images/Components/25756/gaming-pc-01-H7-Flow-white-main-solo-175.png");
        
        AddPcBuild(AppDataIds.PcPrebuilt7, AppDataIds.Discount0, CategoryNames.PrebuiltPc, 
            "Prebuilt PC 7", "Prebuilt PC 7 Description", 
            "https://www.cyberpowerpc.com/images/cs/corsair4000d/cs-429-143_220.png?v1");
        
        AddPcBuild(AppDataIds.PcPrebuilt8, AppDataIds.Discount10, CategoryNames.PrebuiltPc, 
            "Prebuilt PC 8", "Prebuilt PC 8 Description", 
            "https://www.cyberpowerpc.com/images/cs/corsair4000dair/cs-429-144_220.png?v1");
        
        AddPcBuild(AppDataIds.PcPrebuilt9, AppDataIds.Discount0, CategoryNames.PrebuiltPc, 
            "Prebuilt PC 9", "Prebuilt PC 9 Description", 
            "https://www.cyberpowerpc.com/images/cs/H510/CS-211-222_220.png");
        
        AddPcBuild(AppDataIds.PcPrebuilt10, AppDataIds.Discount20, CategoryNames.PrebuiltPc, 
            "Prebuilt PC 10", "Prebuilt PC 10 Description", 
            "https://www.cyberpowerpc.com/images/cs/DESTINYFLOW/CS-428-310_220.png");
        
        AddPcBuild(AppDataIds.PcPrebuilt11, AppDataIds.Discount10, CategoryNames.PrebuiltPc, 
            "Prebuilt PC 11", 
            "A powerful computer designed for playing video games. " +
            "It has high-end components, including a fast processor, a high-quality graphics card, plenty of RAM, " +
            "and ample storage space. The case also features LED lighting and other aesthetic features to create " +
            "a visually appealing setup. With this PC, you can experience superior graphics and performance, " +
            "allowing you to play the latest games with ease.", 
            "https://www.cyberpowerpc.com/images/cs/corsair7000x/cs-429-154_400.png");
        
        AddPcBuild(AppDataIds.PcTemplate1, AppDataIds.Discount0, CategoryNames.TemplatePc, 
            "Intel Bronze Configurator", 
            "Intel Core i3-13100F\nGeForce GTX 1660 SUPER\n16GB of DDR4 RAM\n1TB of SSD Storage", 
            "https://content.ibuypower.com/Images/Components/21839/gaming-pc-01-500DX-main-175.png");
        
        AddPcBuild(AppDataIds.PcTemplate2, AppDataIds.Discount0, CategoryNames.TemplatePc, 
            "Intel Silver Configurator", 
            "Intel Core i5-13600K\nGeForce RTX 3060\n16GB of DDR5 Memory\n2TB of SSD Storage\n2TB of HDD Storage", 
            "https://www.cyberpowerpc.com/images/cs/corsair4000d/cs-429-143_220.png?v1");
        
        AddPcBuild(AppDataIds.PcTemplate3, AppDataIds.Discount0, CategoryNames.TemplatePc, 
            "Intel Gold Configurator", 
            "Intel i7-13700K\nGeForce RTX 4070\n32GB of DDR5 Memory\n2TB of SSD Storage\n4TB of HDD Storage", 
            "https://www.cyberpowerpc.com/images/cs/corsair4000dair/cs-429-144_220.png?v1");
        
        AddPcBuild(AppDataIds.PcTemplate4, AppDataIds.Discount0, CategoryNames.TemplatePc, 
            "AMD Bronze Configurator", 
            "AMD Ryzen 5 5600\nRadeon RX 6600\n16GB of DDR4 Memory\n1TB of SSD Storage", 
            "https://content.ibuypower.com/Images/Components/21909/gaming-pc-01-invader-main-175-.png");
        
        AddPcBuild(AppDataIds.PcTemplate5, AppDataIds.Discount0, CategoryNames.TemplatePc, 
            "AMD Silver Configurator", 
            "AMD Ryzen 7 5700X\nRadeon RX 6800 XT\n32GB of DDR4 Memory\n1TB of SSD Storage\n4TB of HDD Storage", 
            "https://www.cyberpowerpc.com/images/cs/H510/CS-211-222_220.png");
        
        AddPcBuild(AppDataIds.PcTemplate6, AppDataIds.Discount0, CategoryNames.TemplatePc, 
            "AMD Gold Configurator", 
            "Ryzen 7 7700X\nRadeon RX 7900 XTX\n32GB of DDR5 Memory\n2TB of SSD Storage\n6TB of HDD Storage", 
            "https://www.cyberpowerpc.com/images/cs/DESTINYFLOW/CS-428-310_220.png");
    }

    private void AddPcBuild(Guid id, Guid discountId, 
        string category, string pcName, string description, string imageSrc)
    {
        _context.PcBuilds.Add(new PcBuild
        {
            Id = id,
            CategoryId = AppDataIds.CategoryIds[category],
            DiscountId = discountId,
            PcName = pcName,
            Description = description,
            Stock = category == CategoryNames.PrebuiltPc ? 100 : 0,
            ImageSrc = imageSrc
        });
    }
}