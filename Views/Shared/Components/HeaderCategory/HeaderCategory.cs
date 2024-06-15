using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VnStockproxx.Models;

namespace VnStockproxx {
    public class HeaderCategory : ViewComponent
    {
        enum HeaderCategoryList { ChungKhoan = 3, BatDongSan = 4, TaiChinh = 5, GiaiTri = 6 };
        private readonly CateRepository _cateRepo;

        public HeaderCategory(VnStockproxxDbContext context)
        {
            this._cateRepo = new CateRepository(context);
        }
        public async Task<IViewComponentResult> InvokeAsync(VnStockproxxDbContext context)
        {
            int[] categoryIds = new int[] { (int)HeaderCategoryList.ChungKhoan, (int)HeaderCategoryList.BatDongSan, (int)HeaderCategoryList.TaiChinh, (int)HeaderCategoryList.GiaiTri };
            var categories = await _cateRepo.GetAll().Where(e => categoryIds.Contains(e.Id)).ToListAsync();
            return View(categories); //Default.cshtml
        }
    }
}
