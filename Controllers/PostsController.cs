using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VnStockproxx.Models;

namespace VnStockproxx.Controllers
{
    public class PostsController : Controller
    {
        private readonly PostRepository _postRepo;
        private readonly CateRepository _cateRepo;
        private readonly TagRepository _tagRepo;

        public PostsController(PostRepository postRepo, CateRepository cateRepo, TagRepository tagRepo)
        {
            this._postRepo = postRepo;
            this._cateRepo = cateRepo;
            this._tagRepo = tagRepo;
        }

        // GET: Posts
        public async Task<IActionResult> Index(string IdCategory, int page = 1, int pageSize = 5)
        {
            ViewBag.IdCategory = "";
            // lấy dữ liệu đầy đủ Posts
            var posts = await _postRepo.GetAll()
                .Include(p => p.Cate)
                .OrderByDescending(x => x.UpdatedDate)
                .ToListAsync();

            // filter theo category
            var Categories = await _cateRepo.GetAll().ToListAsync();

            if (IdCategory != null && IdCategory != "All")
            {
                posts = posts.Where(p => p.CateId == Int32.Parse(IdCategory)).ToList();
                ViewBag.IdCategory = IdCategory;
            }

            // phân trang
            int totalItems = posts.Count;
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            posts = posts.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.Categories = Categories;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            return View(posts);
        }

        // GET: Posts/Create
        public async Task<IActionResult> Create(string searching)
        {
            var strSearch = searching == null ? "" : searching.Replace("+", " ");
            var category = await _cateRepo.GetAll().ToListAsync();
            var tag = await _tagRepo.GetAll()
                .Where(t => t.Name.Contains(strSearch) )
                .OrderBy(x => x.Name)
                .Take(searching != null ? 10 :5)
                .ToListAsync();
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
                    var data = await _postRepo.GetAll().Include(p => p.IdTag)
                        .Where(e => e.Id == id)
                        .FirstOrDefaultAsync();
                    if (data == null)
                    {
                        return NotFound();
                    }
                    data.IdTag.Clear();

                    var ArrIdTagString = Request.Form["ListIdTagInt"];
                    int[] ArrIdTagInt = ArrIdTagString.Select(n => Convert.ToInt32(n)).ToArray();
                    var IdTag = await _tagRepo.GetAll().Where(tag => ArrIdTagInt.Contains(tag.Id)).ToListAsync();

                    data.Title = post.Title;
                    data.Content = post.Content;
                    data.Image = post.Image;
                    data.CateId = post.CateId;
                    data.CreatedBy = post.CreatedBy;
                    data.IdTag = IdTag;
                    data.UpdatedDate = DateTime.Now;

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
            var post = await _postRepo.FindById(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        //GET: Posts/DetailTags/5
        public async Task<IActionResult> DetailTags(int id)
        {
            var Posts = await _postRepo.GetAll().Include(p => p.IdTag)
                .Where(p => p.IdTag.Select(tag => tag.Id).Contains(id))
                .ToArrayAsync();
            var NameTag = await _tagRepo.GetAll().Where(e => e.Id == id).Select(tag => tag.Name).FirstOrDefaultAsync();
            if (Posts == null)
            {
                return NotFound();
            }
            ViewBag.NameTag = NameTag;
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
