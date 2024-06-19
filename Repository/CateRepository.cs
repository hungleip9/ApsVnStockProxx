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
            var entity = context.Category.Find(id);
            if (entity != null) return true;
            return false;
        }

        public async Task<Category?> FindById(int id)
        {
            var entity = await context.Category.AsQueryable().Where(e => e.Id == id).FirstOrDefaultAsync();
            return entity;
        }
        public IQueryable<Category> GetAll()
        {
            return context.Category.AsQueryable<Category>();
        }

        public async Task Remove(Category entity)
        {
            context.Category.Remove(entity);
            await context.SaveChangesAsync();
        }
        public async Task Update(Category entity)
        {
            if (entity == null) return;
            context.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}
