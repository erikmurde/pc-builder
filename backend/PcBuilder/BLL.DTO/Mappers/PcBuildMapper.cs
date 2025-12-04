using AutoMapper;
using BLL.DTO.PcBuild;
using DAL.Base;
using DAL.DTO.PcComponent;
using DAL.EF.App.Seeding.Names;

namespace BLL.DTO.Mappers;

public class PcBuildMapper : BaseMapper<PcBuildDTO, DAL.DTO.PcBuild.PcBuildDTO>
{
    public PcBuildMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public PcBuildDetailsDTO MapDetails(DAL.DTO.PcBuild.PcBuildDetailsDTO pcBuild)
    {
        return Mapper.Map<PcBuildDetailsDTO>(pcBuild);
    }

    public DAL.DTO.PcBuild.PcBuildCreateDTO MapCreate(PcBuildCreateDTO pcBuild)
    {
        return Mapper.Map<DAL.DTO.PcBuild.PcBuildCreateDTO>(pcBuild);
    }
    
    public PcBuildCreateDTO MapCreate(DAL.DTO.PcBuild.PcBuildCreateDTO pcBuild)
    {
        return Mapper.Map<PcBuildCreateDTO>(pcBuild);
    }
    
    public DAL.DTO.PcBuild.PcBuildEditDTO MapEdit(PcBuildEditDTO pcBuild)
    {
        return Mapper.Map<DAL.DTO.PcBuild.PcBuildEditDTO>(pcBuild);
    }
    
    public PcBuildEditDTO MapEdit(DAL.DTO.PcBuild.PcBuildEditDTO pcBuild)
    {
        return Mapper.Map<PcBuildEditDTO>(pcBuild);
    }
    
    public PcBuildEditDTO MapEdit(
        DAL.DTO.PcBuild.PcBuildEditDTO pcBuild, List<PcComponentSimpleDTO> components)
    {
        var bllPcBuild = MapEdit(pcBuild);

        bllPcBuild.CaseId = components.First(c => 
            c.CategoryName == CategoryNames.Case).ComponentId;
        bllPcBuild.MotherboardId = components.First(c => 
            c.CategoryName == CategoryNames.Motherboard).ComponentId;
        bllPcBuild.ProcessorId = components.First(c => 
            c.CategoryName == CategoryNames.Processor).ComponentId;
        bllPcBuild.CpuCoolerId = components.First(c => 
            c.CategoryName == CategoryNames.CpuCooler).ComponentId;
        bllPcBuild.MemoryId = components.First(c => 
            c.CategoryName == CategoryNames.Memory).ComponentId;
        bllPcBuild.GraphicsCardId = components.First(c => 
            c.CategoryName == CategoryNames.GraphicsCard).ComponentId;
        bllPcBuild.PrimaryStorageId = components.First(c => 
            c.CategoryName == CategoryNames.SolidStateDrive).ComponentId;
        bllPcBuild.SecondaryStorageId = components.FirstOrDefault(c => 
            c.CategoryName == CategoryNames.HardDrive)?.ComponentId;
        bllPcBuild.PowerSupplyId = components.First(c => 
            c.CategoryName == CategoryNames.PowerSupply).ComponentId;
        bllPcBuild.OperatingSystemId = components.First(c => 
            c.CategoryName == CategoryNames.OperatingSystem).ComponentId;
        
        return bllPcBuild;
    }
    
    public PcBuildCartDTO MapCart(DAL.DTO.PcBuild.PcBuildCartDTO pcBuild)
    {
        return Mapper.Map<PcBuildCartDTO>(pcBuild);
    }
    
    public PcBuildStoreDTO MapStore(DAL.DTO.PcBuild.PcBuildStoreDTO pcBuild)
    {
        return Mapper.Map<PcBuildStoreDTO>(pcBuild);
    }
}