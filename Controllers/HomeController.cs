using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using VnStockproxx.Models;

namespace VnStockproxx.Controllers
{
    public class HomeController : Controller
    {
        private readonly PostRepository _postRepo;
        private readonly CateRepository _cateRepo;
        private readonly int IdTinMoi;
        private readonly int IdTinNoiBat;
        public HomeController(PostRepository postRepo, CateRepository cateRepo, IConfiguration configuration)
        {
            this._postRepo = postRepo;
            this._cateRepo = cateRepo;

            IdTinMoi = Convert.ToInt32(configuration["IdPost:TinMoi"]);
            IdTinNoiBat = Convert.ToInt32(configuration["IdPost:TinNoiBat"]);
        }
            public async Task<IActionResult> Index()
        {
            var categories = await _cateRepo.GetAll().ToListAsync();
            var posts = await _postRepo.GetAll()
                .Include(p => p.Cate)
                .Where(p => p.CateId == IdTinMoi || p.CateId == IdTinNoiBat)
                .ToListAsync();
            ViewData["IdTinMoi"] = IdTinMoi;
            ViewData["IdTinNoiBat"] = IdTinNoiBat;
            return View(posts);
        }
        [Route("{slug}/{id:int}")]
        public async Task<ViewResult> ViewPost(int id)
        {
            //3 - chứng khoán
            var posts = await _postRepo.GetAll().Include(p => p.Cate).Where(post => post.CateId == id).ToListAsync();
            return View(posts);
        }
        [Route("GioiThieu")]
        public ViewResult GioiThieu()
        {
            return View();
        }
        [Route("LienHe")]
        public ViewResult LienHe()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
