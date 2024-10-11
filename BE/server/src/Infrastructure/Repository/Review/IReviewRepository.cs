namespace NShop.src.Infrastructure.Repository.Review;

using NShop.src.Application.DTOs.Review;
using NShop.src.Domain.Review;


public interface IReviewRepository
    {
        Task<Review> GetByIdAsync(Guid id);
        Task<IEnumerable<Review>> GetAllAsync();
        Task<ReviewList> GetReviewListAsync(ReviewQuery query);
        Task<Review> AddAsync(Review review);
        Task<Review> UpdateAsync(Guid Id, Review review);
        Task<Review> DeleteAsync(Review review);
    }

public class ReviewList
{
    public IEnumerable<Review> Reviews { get; set; } = [];
    public int Total { get; set; }
}
