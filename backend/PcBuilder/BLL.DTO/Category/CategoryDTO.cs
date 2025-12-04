using Domain.Base;

namespace BLL.DTO.Category;

public class CategoryDTO : DomainEntityId
{
    public string CategoryName { get; set; } = default!;
}