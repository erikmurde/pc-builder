using BLL.DTO.Category;
using DAL.Contracts.Base;

namespace BLL.Contracts.App;

public interface ICategoryService : IBaseRepository<CategoryDTO>
{
}