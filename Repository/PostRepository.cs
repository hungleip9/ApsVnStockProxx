using Microsoft.EntityFrameworkCore;
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
            Post entity = context.Posts.Find(id);
            if (entity != null) return true;
            return false;
        }

        public async Task<Post> FindById(int id)
        {
            Post entity = await context.Posts.FindAsync(id);
            return entity;
        }

        public async Task<List<Post>> GetAll()
        {
            return await context.Posts.ToListAsync();
        }

        public async Task Remove(Post entity)
        {
            context.Posts.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task Update(Post entity)
        {
            var newdata = await context.Posts.FindAsync(entity.Id);
            if (newdata is not null)
            {
                newdata.Title = entity.Title;
                newdata.Content = entity.Content;
                newdata.Teaser = entity.Teaser;
                newdata.CateId = entity.CateId;
                newdata.Image = entity.Image;
                await context.SaveChangesAsync();
            }
        }
    }
}
