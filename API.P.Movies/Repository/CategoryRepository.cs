using API.P.Movies.DAL;
using API.P.Movies.DAL.Models;
using API.P.Movies.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace API.P.Movies.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CategoryExistsByIdAsync(int id)
        {
            return await _context.Categories
                .AsNoTracking()
                .AnyAsync(c => c.Id == id);
        }

        public async Task<bool> CategoryExistsByNameAsync(string name)
        {
            return await _context.Categories
                .AsNoTracking()
                .AnyAsync(c => c.Name == name);
        }

        public async Task<bool> CreateCategoryAsync(Category category)
        {
            category.CreatedDate = DateTime.UtcNow;

            _context.Categories.Add(category);
            return await SaveAsync();
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await GetCategoryAsync(id);
            if (category == null)
            {
                return false;
            }
            _context.Categories.Remove(category);
            return await SaveAsync();
        }

        public async Task<ICollection<Category>> GetCategoriesAsync()
        {
            var categories = await _context.Categories
                .AsNoTracking()
                .OrderBy(c => c.Name)
                .ToListAsync();
            return categories;
        }

        public async Task<Category> GetCategoryAsync(int id)
        {
            var category = await _context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            return category;
        }

        public async Task<bool> UpdateCategoryAsync(Category category)
        {
            category.UpdateDate = DateTime.UtcNow;
            _context.Categories.Update(category);
            return await SaveAsync();
        }
        #region Private Methods
        private async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() >= 0 ? true : false;
        }
        #endregion
    }
}
