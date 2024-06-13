using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using VnStockproxx.Models;

namespace VnStockproxx
{
    public class PostRepository : IRepository<Post>
    {
        private VnStockproxxDbContext context;
        public PostRepository(VnStockproxxDbContext _context)
        {
            this.context = _context;
        }
        public async Task Add(Post entity)
        {
            context.Add(entity);
            await context.SaveChangesAsync();
        }

        public bool Exist(int id)
        {
            Post entity = context.Post.Find(id);
            if (entity != null) return true;
            return false;
        }

        public async Task<Post> FindById(int id)
        {
            var entity = await context.Post.FindAsync(id);
            if (entity == null)
            {
                return entity;
            }
            entity.ViewCount += 1;
            await context.SaveChangesAsync();
            return entity;
        }
        public IQueryable<Post> GetAll()
        {
            return context.Post.AsQueryable<Post>();
        }

        public async Task Remove(Post entity)
        {
            context.Post.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task Update(Post entity)
        {
            if (entity == null) return;
            context.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}
