using DAL.Contracts.Base;
using DAL.DTO.Category;

namespace DAL.Contracts.App;

public interface ICategoryRepository : IBaseRepository<CategoryDTO>
{
}