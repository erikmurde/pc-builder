using AutoMapper;
using BLL.DTO.Category;
using DAL.Base;
using DALCategoryDTO = DAL.DTO.Category.CategoryDTO;

namespace BLL.DTO.Mappers;

public class CategoryMapper : BaseMapper<CategoryDTO, DALCategoryDTO>
{
    public CategoryMapper(IMapper mapper) : base(mapper)
    {
    }
}