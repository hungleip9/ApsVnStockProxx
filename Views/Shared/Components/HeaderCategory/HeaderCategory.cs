using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VnStockproxx.Models;

namespace VnStockproxx {
    public class HeaderCategory : ViewComponent
    {
        private readonly CateRepository _cateRepo;
        private readonly int ChungKhoan = 0;
        private readonly int BatDongSan = 0;
        private readonly int TaiChinh = 0;
        private readonly int GiaiTri = 0;

        public HeaderCategory(VnStockproxxDbContext context, IConfiguration configuration)
        {
            this._cateRepo = new CateRepository(context);
            ChungKhoan = Int32.Parse(configuration["IdPost:ChungKhoan"] ?? "0");
            BatDongSan = Int32.Parse(configuration["IdPost:BatDongSan"] ?? "0");
            TaiChinh = Int32.Parse(configuration["IdPost:TaiChinh"] ?? "0");
            GiaiTri = Int32.Parse(configuration["IdPost:GiaiTri"] ?? "0");
        }
        public async Task<IViewComponentResult> InvokeAsync(VnStockproxxDbContext context)
        {
            int[] categoryIds = new int[] { ChungKhoan, BatDongSan, TaiChinh, GiaiTri };
            var categories = await _cateRepo.GetAll().Where(e => categoryIds.Contains(e.Id)).ToListAsync();
            return View(categories); //Default.cshtml
        }
    }
}
