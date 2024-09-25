using coin_api.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace coin_api.Application.Service
{
    public class CategoryService
    {
        private readonly ConnectionContext _context;

        public CategoryService(ConnectionContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoryModel>> GetAllCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task CreateCategory(CategoryModel categoryModel)
        {
            _context.Categories.Add(categoryModel);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategory(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateCategory(CategoryModel categoryModel)
        {
            var category = await _context.Categories.FindAsync(categoryModel.IdCategory);
            if (category != null)
            {
                // Atualiza os campos conforme necessário
                category.Category = categoryModel.Category;
                category.UserId = categoryModel.UserId;

                _context.Categories.Update(category);
                await _context.SaveChangesAsync();
            }
        }
    }
}
