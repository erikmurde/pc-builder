using BLL.Base;
using BLL.Contracts.App;
using BLL.DTO.PackageSize;
using BLL.DTO.Mappers;
using DAL.Contracts.App;
using Helpers.Base;

namespace BLL.App.Services;

public class PackageSizeService : 
    BaseEntityService<PackageSizeDTO, DAL.DTO.PackageSize.PackageSizeDTO, IPackageSizeRepository>, IPackageSizeService
{
    private readonly IAppUOW _uow;
    private readonly PackageSizeMapper _mapper;
    
    public PackageSizeService(IAppUOW uow, PackageSizeMapper mapper) : 
        base(uow.PackageSizeRepository, mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public override PackageSizeDTO Update(PackageSizeDTO packageSize)
    {
        if (!EntityValidationHelper.ValidatePackageSize(packageSize))
        {
            throw new ArgumentException("");
        }

        var result = _uow.PackageSizeRepository.Update(_mapper.Map(packageSize));
        return _mapper.Map(result);
    }

    public override PackageSizeDTO Add(PackageSizeDTO packageSize)
    {
        if (!EntityValidationHelper.ValidatePackageSize(packageSize))
        {
            throw new ArgumentException("");
        }

        var result = _uow.PackageSizeRepository.Add(_mapper.Map(packageSize));
        return _mapper.Map(result);
    }
}