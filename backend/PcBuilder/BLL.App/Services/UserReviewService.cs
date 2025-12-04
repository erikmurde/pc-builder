using BLL.Base;
using BLL.Contracts.App;
using BLL.DTO.UserReview;
using BLL.DTO.Mappers;
using DAL.Contracts.App;
using Helpers.Base;

namespace BLL.App.Services;

public class UserReviewService : 
    BaseEntityService<UserReviewDTO, DAL.DTO.UserReview.UserReviewDTO, IUserReviewRepository>, IUserReviewService
{
    private readonly IAppUOW _uow;
    private readonly UserReviewMapper _mapper;
    
    public UserReviewService(IAppUOW uow, UserReviewMapper mapper) : 
        base(uow.UserReviewRepository, mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserReviewDTO>> AllAsyncPcBuild(Guid pcBuildId)
    {
        return (await _uow.UserReviewRepository.AllAsyncPcBuild(pcBuildId))
            .Select(u => _mapper.Map(u));
    }

    public async Task<IEnumerable<UserReviewDTO>> AllAsync(Guid userId)
    {
        return (await _uow.UserReviewRepository.AllAsync(userId))
            .Select(u => _mapper.Map(u));
    }

    public async Task<UserReviewDTO?> FindAsync(Guid id, Guid userId)
    {
        var dalUserReview = await _uow.UserReviewRepository.FindAsync(id, userId);
        return dalUserReview == null ? null : _mapper.Map(dalUserReview);
    }

    public async Task<UserReviewEditDTO> Update(UserReviewEditDTO userReview)
    {
        if (!EntityValidationHelper.ValidateUserReview(userReview))
        {
            throw new ArgumentException("");
        }

        var dalUserReview = _mapper.MapEdit(userReview);
        return _mapper.MapEdit(await _uow.UserReviewRepository.Update(dalUserReview));
    }

    public UserReviewEditDTO Add(UserReviewEditDTO userReview, Guid userId)
    {
        if (!EntityValidationHelper.ValidateUserReview(userReview))
        {
            throw new ArgumentException("");
        }

        var dalUserReview = _mapper.MapEdit(userReview);
        return _mapper.MapEdit(_uow.UserReviewRepository.Add(dalUserReview, userId));
    }
}