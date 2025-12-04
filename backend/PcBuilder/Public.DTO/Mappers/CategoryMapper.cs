using AutoMapper;
using DAL.Base;
using Public.DTO.V1.Category;

namespace Public.DTO.Mappers;

public class CategoryMapper : BaseMapper<BLL.DTO.Category.CategoryDTO, CategoryDTO>
{
    public CategoryMapper(IMapper mapper) : base(mapper)
    {
    }

    public BLL.DTO.Category.CategoryDTO MapCreate(CategoryCreateDTO category)
    {
        return Mapper.Map<BLL.DTO.Category.CategoryDTO>(category);
    }
}