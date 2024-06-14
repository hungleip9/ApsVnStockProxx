using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using VnStockproxx.Models;

namespace VnStockproxx.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly CateRepository _cateRepo;

        public CategoriesController(VnStockproxxDbContext context)
        {
            this._cateRepo = new CateRepository(context);
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            return View(await _cateRepo.GetAll().ToListAsync());
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                var data = new Category();
                // copy từ post vào data
                category.CopyPropertiesTo(data);
                await _cateRepo.Add(data);
                return RedirectToAction(nameof(Index));
            } else
            {
                Log.LogError(ModelState);
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _cateRepo.FindById(id.Value);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var data = new Category();
                    // copy từ post vào data
                    category.CopyPropertiesTo(data);
                    data.NameMap = ConvertViToEn.ViToEn(category.Name);

                    await _cateRepo.Update(data);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _cateRepo.FindById(id.Value);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _cateRepo.FindById(id);
            if (category == null)
            {
                return NotFound();
            }
            await _cateRepo.Remove(category);
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _cateRepo.Exist(id);
        }
    }
}
