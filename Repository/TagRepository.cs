using Microsoft.EntityFrameworkCore;
using VnStockproxx.Models;

namespace VnStockproxx
{
    public class TagRepository : IRepository<Tag>
    {
        private VnStockproxxDbContext context;
        public TagRepository(VnStockproxxDbContext _context)
        {
            this.context = _context;
        }
        public async Task Add(Tag entity)
        {
            context.Add(entity);
            await context.SaveChangesAsync();
        }

        public bool Exist(int id)
        {
            var entity = context.Tag.Find(id);
            if (entity != null) return true;
            return false;
        }

        public async Task<Tag?> FindById(int id)
        {
            var entity = await context.Tag.AsQueryable()
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();
            return entity;
        }
        public IQueryable<Tag> GetAll()
        {
            return context.Tag.AsQueryable<Tag>();
        }

        public async Task Remove(Tag entity)
        {
            context.Tag.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task Update(Tag entity)
        {
            var newdata = await context.Tag.FindAsync(entity.Id);
            if (newdata is not null)
            {
                newdata.Name = entity.Name;
                await context.SaveChangesAsync();
            }
        }
    }
}
