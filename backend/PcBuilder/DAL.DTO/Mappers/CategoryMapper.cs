using AutoMapper;
using DAL.Base;
using DAL.DTO.Category;

namespace DAL.DTO.Mappers;

public class CategoryMapper : BaseMapper<CategoryDTO, Domain.App.Category>
{
    public CategoryMapper(IMapper mapper) : base(mapper)
    {
    }
}