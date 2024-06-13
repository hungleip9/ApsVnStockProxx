using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                await _cateRepo.Add(category);
                return RedirectToAction(nameof(Index));
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
                    await _cateRepo.Update(category);
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
