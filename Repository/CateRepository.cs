using Microsoft.EntityFrameworkCore;
using VnStockproxx.Models;

namespace VnStockproxx
{
    public class CateRepository : IRepository<Category>
    {
        private VnStockproxxDbContext context;
        public CateRepository(VnStockproxxDbContext _context)
        {
            this.context = _context;
        }
        public async Task Add(Category entity)
        {
            context.Add(entity);
            await context.SaveChangesAsync();
        }

        public bool Exist(int id)
        {
            Category entity = context.Categories.Find(id);
            if (entity != null) return true;
            return false;
        }

        public async Task<Category> FindById(int id)
        {
            Category entity = await context.Categories.FindAsync(id);
            return entity;
        }

        public async Task<List<Category>> GetAll()
        {
            return await context.Categories.ToListAsync();
        }

        public async Task Remove(Category entity)
        {
            context.Categories.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task Update(Category entity)
        {
            var newdata = await context.Categories.FindAsync(entity.Id);
            if (newdata is not null)
            {
                newdata.CategoryName = entity.CategoryName;
                await context.SaveChangesAsync();
            }
        }
    }
}
