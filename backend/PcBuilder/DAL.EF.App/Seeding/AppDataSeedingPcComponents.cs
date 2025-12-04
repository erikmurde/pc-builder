using Domain.App;

namespace DAL.EF.App.Seeding;

public class AppDataSeedingPcComponents
{
    private readonly ApplicationDbContext _context;
    
    public AppDataSeedingPcComponents(ApplicationDbContext context)
    {
        _context = context;
    }
    
    internal void SeedAppDataPcComponents()
    {
        AddComponentsToPc(AppDataIds.PcPrebuilt1, new List<Guid>
        {
            AppDataComponentIds.Case1, AppDataComponentIds.MotherboardIntel1, AppDataComponentIds.ProcessorIntel2,
            AppDataComponentIds.CpuCoolerAir1, AppDataComponentIds.Memory1, AppDataComponentIds.GraphicsCardIntel2,
            AppDataComponentIds.PrimaryStorage2, AppDataComponentIds.SecondaryStorage1, AppDataComponentIds.PowerSupply1
        });
        AddComponentsToPc(AppDataIds.PcPrebuilt2, new List<Guid>
        {
            AppDataComponentIds.Case2, AppDataComponentIds.MotherboardAmd1, AppDataComponentIds.ProcessorAmd1,
            AppDataComponentIds.CpuCoolerAir2, AppDataComponentIds.Memory5, AppDataComponentIds.GraphicsCardAmd1,
            AppDataComponentIds.PrimaryStorage1, AppDataComponentIds.PowerSupply1
        });
        AddComponentsToPc(AppDataIds.PcPrebuilt3, new List<Guid>
        {
            AppDataComponentIds.Case3, AppDataComponentIds.MotherboardIntel5, AppDataComponentIds.ProcessorIntel4,
            AppDataComponentIds.CpuCoolerLiquid1, AppDataComponentIds.Memory7, AppDataComponentIds.GraphicsCardNvidia5,
            AppDataComponentIds.PrimaryStorage3, AppDataComponentIds.SecondaryStorage5, AppDataComponentIds.PowerSupply4
        });
        AddComponentsToPc(AppDataIds.PcPrebuilt4, new List<Guid>
        {
            AppDataComponentIds.Case4, AppDataComponentIds.MotherboardAmd5, AppDataComponentIds.ProcessorAmd4,
            AppDataComponentIds.CpuCoolerLiquid2, AppDataComponentIds.Memory4, AppDataComponentIds.GraphicsCardAmd3,
            AppDataComponentIds.PrimaryStorage6, AppDataComponentIds.SecondaryStorage3, AppDataComponentIds.PowerSupply3
        });
        AddComponentsToPc(AppDataIds.PcPrebuilt5, new List<Guid>
        {
            AppDataComponentIds.Case5, AppDataComponentIds.MotherboardIntel1, AppDataComponentIds.ProcessorIntel3,
            AppDataComponentIds.CpuCoolerLiquid3, AppDataComponentIds.Memory6, AppDataComponentIds.GraphicsCardNvidia6,
            AppDataComponentIds.PrimaryStorage4, AppDataComponentIds.SecondaryStorage2, AppDataComponentIds.PowerSupply2
        });
        AddComponentsToPc(AppDataIds.PcPrebuilt6, new List<Guid>
        {
            AppDataComponentIds.Case6, AppDataComponentIds.MotherboardAmd2, AppDataComponentIds.ProcessorAmd2,
            AppDataComponentIds.CpuCoolerAir1, AppDataComponentIds.Memory2, AppDataComponentIds.GraphicsCardIntel1,
            AppDataComponentIds.PrimaryStorage2, AppDataComponentIds.SecondaryStorage3, AppDataComponentIds.PowerSupply2
        });
        AddComponentsToPc(AppDataIds.PcPrebuilt7, new List<Guid>
        {
            AppDataComponentIds.Case7, AppDataComponentIds.MotherboardIntel1, AppDataComponentIds.ProcessorIntel2,
            AppDataComponentIds.CpuCoolerAir2, AppDataComponentIds.Memory1, AppDataComponentIds.GraphicsCardNvidia3,
            AppDataComponentIds.PrimaryStorage6, AppDataComponentIds.PowerSupply1
        });
        AddComponentsToPc(AppDataIds.PcPrebuilt8, new List<Guid>
        {
            AppDataComponentIds.Case8, AppDataComponentIds.MotherboardIntel3, AppDataComponentIds.ProcessorIntel4,
            AppDataComponentIds.CpuCoolerLiquid4, AppDataComponentIds.Memory8, AppDataComponentIds.GraphicsCardNvidia2,
            AppDataComponentIds.PrimaryStorage5, AppDataComponentIds.SecondaryStorage3, AppDataComponentIds.PowerSupply4
        });
        AddComponentsToPc(AppDataIds.PcPrebuilt9, new List<Guid>
        {
            AppDataComponentIds.Case9, AppDataComponentIds.MotherboardAmd5, AppDataComponentIds.ProcessorAmd5,
            AppDataComponentIds.CpuCoolerLiquid4, AppDataComponentIds.Memory8, AppDataComponentIds.GraphicsCardAmd2,
            AppDataComponentIds.PrimaryStorage5, AppDataComponentIds.SecondaryStorage6, AppDataComponentIds.PowerSupply5
        });
        AddComponentsToPc(AppDataIds.PcPrebuilt10, new List<Guid>
        {
            AppDataComponentIds.Case10, AppDataComponentIds.MotherboardAmd4, AppDataComponentIds.ProcessorAmd3,
            AppDataComponentIds.CpuCoolerLiquid1, AppDataComponentIds.Memory3, AppDataComponentIds.GraphicsCardAmd4,
            AppDataComponentIds.PrimaryStorage3, AppDataComponentIds.SecondaryStorage1, AppDataComponentIds.PowerSupply3
        });
        AddComponentsToPc(AppDataIds.PcPrebuilt11, new List<Guid>
        {
            AppDataComponentIds.Case11, AppDataComponentIds.MotherboardIntel6, AppDataComponentIds.ProcessorIntel5,
            AppDataComponentIds.CpuCoolerLiquid4, AppDataComponentIds.Memory9, AppDataComponentIds.GraphicsCardNvidia1,
            AppDataComponentIds.PrimaryStorage5, AppDataComponentIds.SecondaryStorage4, AppDataComponentIds.PowerSupply6
        });
        
        
        AddComponentsToPc(AppDataIds.PcTemplate1, new List<Guid>
        {
            AppDataComponentIds.Case3, AppDataComponentIds.MotherboardIntel1, AppDataComponentIds.ProcessorIntel1,
            AppDataComponentIds.CpuCoolerAir1, AppDataComponentIds.Memory1, AppDataComponentIds.GraphicsCardNvidia3,
            AppDataComponentIds.PrimaryStorage1, AppDataComponentIds.PowerSupply1
        });
        AddComponentsToPc(AppDataIds.PcTemplate2, new List<Guid>
        {
            AppDataComponentIds.Case7, AppDataComponentIds.MotherboardIntel2, AppDataComponentIds.ProcessorIntel3,
            AppDataComponentIds.CpuCoolerLiquid2, AppDataComponentIds.Memory3, AppDataComponentIds.GraphicsCardNvidia4,
            AppDataComponentIds.PrimaryStorage3, AppDataComponentIds.SecondaryStorage1, AppDataComponentIds.PowerSupply2
        });
        AddComponentsToPc(AppDataIds.PcTemplate3, new List<Guid>
        {
            AppDataComponentIds.Case8, AppDataComponentIds.MotherboardIntel5, AppDataComponentIds.ProcessorIntel4,
            AppDataComponentIds.CpuCoolerLiquid1, AppDataComponentIds.Memory7, AppDataComponentIds.GraphicsCardNvidia5,
            AppDataComponentIds.PrimaryStorage5, AppDataComponentIds.SecondaryStorage3, AppDataComponentIds.PowerSupply4
        });
        AddComponentsToPc(AppDataIds.PcTemplate4, new List<Guid>
        {
            AppDataComponentIds.Case2, AppDataComponentIds.MotherboardAmd1, AppDataComponentIds.ProcessorAmd1,
            AppDataComponentIds.CpuCoolerAir2, AppDataComponentIds.Memory5, AppDataComponentIds.GraphicsCardAmd1,
            AppDataComponentIds.PrimaryStorage2, AppDataComponentIds.PowerSupply1
        });
        AddComponentsToPc(AppDataIds.PcTemplate5, new List<Guid>
        {
            AppDataComponentIds.Case9, AppDataComponentIds.MotherboardAmd2, AppDataComponentIds.ProcessorAmd2,
            AppDataComponentIds.CpuCoolerLiquid2, AppDataComponentIds.Memory2, AppDataComponentIds.GraphicsCardAmd3,
            AppDataComponentIds.PrimaryStorage4, AppDataComponentIds.SecondaryStorage2, AppDataComponentIds.PowerSupply2
        });
        AddComponentsToPc(AppDataIds.PcTemplate6, new List<Guid>
        {
            AppDataComponentIds.Case10, AppDataComponentIds.MotherboardAmd5, AppDataComponentIds.ProcessorAmd4,
            AppDataComponentIds.CpuCoolerLiquid3, AppDataComponentIds.Memory7, AppDataComponentIds.GraphicsCardAmd2,
            AppDataComponentIds.PrimaryStorage5, AppDataComponentIds.SecondaryStorage6, AppDataComponentIds.PowerSupply4
        });
    }

    private void AddComponentsToPc(Guid pcBuildId, List<Guid> componentIds)
    {
        foreach (var componentId in componentIds)
        {
            _context.PcComponents.Add(new PcComponent
            {
                ComponentId = componentId,
                PcBuildId = pcBuildId,
                Qty = 1
            });
        }
        _context.PcComponents.Add(new PcComponent
        {
            ComponentId = AppDataComponentIds.OperatingSystemId,
            PcBuildId = pcBuildId,
            Qty = 1
        });
    }
}