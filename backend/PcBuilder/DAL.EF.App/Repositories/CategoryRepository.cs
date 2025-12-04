using Contracts.Base;
using DAL.Contracts.App;
using DAL.DTO.Category;
using DAL.EF.Base;
using Domain.App;

namespace DAL.EF.App.Repositories;

public class CategoryRepository : 
    EFBaseRepository<CategoryDTO, Category, ApplicationDbContext>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext dataContext, IMapper<CategoryDTO, Category> mapper) : 
        base(dataContext, mapper)
    {
    }
}