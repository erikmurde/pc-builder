using Contracts.Base;
using DAL.Contracts.App;
using DAL.DTO.PackageSize;
using DAL.EF.Base;
using Domain.App;

namespace DAL.EF.App.Repositories;

public class PackageSizeRepository :
    EFBaseRepository<PackageSizeDTO, PackageSize, ApplicationDbContext>, IPackageSizeRepository
{
    public PackageSizeRepository(ApplicationDbContext dataContext, IMapper<PackageSizeDTO, PackageSize> mapper) : 
        base(dataContext, mapper)
    {
    }
}