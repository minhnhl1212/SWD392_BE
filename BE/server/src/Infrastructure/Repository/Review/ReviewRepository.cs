using Database;
using Microsoft.EntityFrameworkCore;
using NShop.src.Application.DTOs.Review;
using NShop.src.Domain.Users;

namespace NShop.src.Infrastructure.Repository.Review
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly NShopDbContext _dbContext;
        public ReviewRepository(NShopDbContext nShopDb)
        {

            _dbContext = nShopDb;
        }
        public  async Task<Domain.Review.Review> AddAsync(Domain.Review.Review review)
        {
            var result = await _dbContext.Reviews.AddAsync(review);

            // Save changes
            await _dbContext.SaveChangesAsync();

            return result.Entity;
        }

        public  async Task<Domain.Review.Review> DeleteAsync(Domain.Review.Review review)
        {
            var result = _dbContext.Reviews.Remove(review);

            // Save changes
            _dbContext.SaveChanges();

            return result.Entity;
        }

        public async Task<IEnumerable<Domain.Review.Review>> GetAllAsync()
        {
            var users = _dbContext.Reviews.AsEnumerable();

            return users;
        }

        public async  Task<Domain.Review.Review> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ReviewList> GetReviewListAsync(ReviewQuery query)
        {
            throw new NotImplementedException();
        }

        public async Task<Domain.Review.Review> UpdateAsync(Guid Id, Domain.Review.Review review)
        {
            var u = await _dbContext.Reviews.FindAsync(Id);

            if (u == null) return null;

            var result = _dbContext.Reviews.Update(review);
            // Save changes
            await _dbContext.SaveChangesAsync();

            return result.Entity;
        }
    }
}
