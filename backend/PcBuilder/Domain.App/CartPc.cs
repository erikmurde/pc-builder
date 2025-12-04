using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App;

public class CartPc : DomainEntityId
{
    public Guid PcBuildId { get; set; }
    public PcBuild? PcBuild { get; set; }

    public Guid ApplicationUserId { get; set; }
    public ApplicationUser? ApplicationUser { get; set; }

    [Range(1, 1000)]
    public int Qty { get; set; }
}