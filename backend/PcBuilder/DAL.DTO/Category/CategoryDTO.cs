using Domain.Base;

namespace DAL.DTO.Category;

public class CategoryDTO : DomainEntityId
{
    public string CategoryName { get; set; } = default!;
}