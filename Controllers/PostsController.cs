using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using VnStockproxx.Models;

namespace VnStockproxx.Controllers
{
    public class PostsController : Controller
    {
        private readonly PostRepository _postRepo;
        private readonly CateRepository _cateRepo;
        private readonly TagRepository _tagRepo;
        private readonly VnStockproxxDbContext _context;

        public PostsController(VnStockproxxDbContext context)
        {
            this._postRepo = new PostRepository(context);
            this._cateRepo = new CateRepository(context);
            this._tagRepo = new TagRepository(context);
            this._context = context;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            var categories = await _cateRepo.GetAll().ToListAsync();
            var posts = await _postRepo.GetAll().ToListAsync();
            var qr = from post in posts
                     join category in categories on post.CateId equals category.Id
                     select new Post
                     {
                         Id = post.Id,
                         Title = post.Title,
                         Image = post.Image,
                         ViewCount = post.ViewCount,
                         CreatedDate = post.CreatedDate,
                         UpdatedDate = post.UpdatedDate,
                         CategoryName = category.Name
                     };
            return View(qr.Reverse().ToList());
        }

        // GET: Posts/Create
        public async Task<IActionResult> Create()
        {
            var category = await _cateRepo.GetAll().ToListAsync();
            var tag = await _tagRepo.GetAll().ToListAsync();
            ViewData["CateId"] = new SelectList(category, "Id", "Name");
            ViewData["IdTag"] = new SelectList(tag, "Id", "Name");
            return View();
        }

        // POST: Posts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Post post)
        {
            if (ModelState.IsValid)
            {
                var ArrIdTagString = Request.Form["IdTag"];
                int[] ArrIdTagInt = ArrIdTagString.Select(n => Convert.ToInt32(n)).ToArray();
                var IdTag = await _tagRepo.GetAll().Where(tag => ArrIdTagInt.Contains(tag.Id)).ToListAsync();
                var data = new Post();

                // copy từ post vào data
                post.CopyPropertiesTo(data);
                data.IdTag = IdTag;
                await _postRepo.Add(data);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                Log.LogError(ModelState);
            }
            return View(post);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var post = await _postRepo.GetAll().Include(p => p.IdTag)
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();
            if (post == null)
            {
                return NotFound();
            }
            var IdTag = post.IdTag.Select(p => p.Id).ToList();
            post.ListIdTagInt = IdTag;
            var category = await _cateRepo.GetAll().ToListAsync();
            var tag = await _tagRepo.GetAll().ToListAsync();
            ViewData["CateId"] = new SelectList(category, "Id", "Name");
            ViewData["IdTag"] = new SelectList(tag, "Id", "Name");
            return View(post);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var idtag = Request.Form["IdTag"];
                    int[] idtagInt = idtag.Select(n => Convert.ToInt32(n)).ToArray();
                    var IdTag = await _tagRepo.GetAll().Where(tag => idtagInt.Contains(tag.Id)).ToListAsync();

                    var data = new Post();
                    data.IdTag.Clear();
                    data.Title = post.Title;
                    data.Content = post.Content;
                    data.ImageContent = post.ImageContent;
                    data.CateId = post.CateId;
                    data.Image = post.Image;
                    data.CreatedBy = post.CreatedBy;
                    data.IdTag = IdTag;

                    await _postRepo.Update(data);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else
            {
                Log.LogError(ModelState);
            }
            return RedirectToAction(nameof(Index));
        }

        //GET: Posts/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var post = await _postRepo.GetAll().Include(p => p.IdTag)
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        //GET: Posts/DetailTags/5
        public async Task<IActionResult> DetailTags(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Posts = await _postRepo.GetAll().Include(p => p.IdTag)
                .Where(p => p.IdTag.Select(tag => tag.Id).Contains(id))
                .ToArrayAsync();
            if (Posts == null)
            {
                return NotFound();
            }
            return View(Posts);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _postRepo.FindById(id.Value);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _postRepo.FindById(id);
            if (post == null)
            {
                return NotFound();
            }
            await _postRepo.Remove(post);
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _postRepo.Exist(id);
        }
    }
}
