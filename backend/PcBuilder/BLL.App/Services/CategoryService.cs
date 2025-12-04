using BLL.Base;
using BLL.Contracts.App;
using BLL.DTO.Category;
using BLL.DTO.Mappers;
using DAL.Contracts.App;
using Helpers.Base;

namespace BLL.App.Services;

public class CategoryService : 
    BaseEntityService<CategoryDTO, DAL.DTO.Category.CategoryDTO, ICategoryRepository>, ICategoryService
{
    private readonly IAppUOW _uow;
    private readonly CategoryMapper _mapper;
    
    public CategoryService(IAppUOW uow, CategoryMapper mapper) : 
        base(uow.CategoryRepository, mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public override CategoryDTO Update(CategoryDTO category)
    {
        if (!EntityValidationHelper.ValidateCategory(category))
        {
            throw new ArgumentException("");
        }

        var result = _uow.CategoryRepository.Update(_mapper.Map(category));
        return _mapper.Map(result);
    }

    public override CategoryDTO Add(CategoryDTO category)
    {
        if (!EntityValidationHelper.ValidateCategory(category))
        {
            throw new ArgumentException("");
        }

        var result = _uow.CategoryRepository.Add(_mapper.Map(category));
        return _mapper.Map(result);
    }
}