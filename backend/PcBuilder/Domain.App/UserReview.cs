using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App;

public class UserReview : DomainEntityId
{
    public Guid ApplicationUserId { get; set; }
    public ApplicationUser? ApplicationUser { get; set; }

    public Guid PcBuildId { get; set; }
    public PcBuild? PcBuild { get; set; }

    [Range(0, 5)]
    public int Rating { get; set; }
    public DateTime ReviewDate { get; set; } = DateTime.UtcNow;
    
    [MaxLength(2048)]
    public string ReviewContent { get; set; } = default!;
}