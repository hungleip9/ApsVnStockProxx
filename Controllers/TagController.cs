using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VnStockproxx.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VnStockproxx.Controllers
{
    public class TagController : Controller
    {
        private readonly TagRepository _tagRepo;

        public TagController(VnStockproxxDbContext context)
        {
            this._tagRepo = new TagRepository(context);
        }

        public async Task<IActionResult> Index()
        {
            var data = await _tagRepo.GetAll();
            return View(data);
        }

        // Get Tag/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // Post Tag/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tag tag)
        {
            if (ModelState.IsValid)
            {
                await _tagRepo.Add(tag);
                return RedirectToAction(nameof(Index));
            }
            return View(tag);
        }

        // GET: Tag/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var tag = await _tagRepo.FindById(id.Value);
            if (tag == null)
            {
                return NotFound();
            }
            return View(tag);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Tag tag)
        {
            if (id != tag.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _tagRepo.Update(tag);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TagExists(tag.Id))
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

        //GET: Tag/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _tagRepo.FindById(id.Value);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Tag/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _tagRepo.FindById(id.Value);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Tag/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _tagRepo.FindById(id);
            if (post == null)
            {
                return NotFound();
            }
            await _tagRepo.Remove(post);
            return RedirectToAction(nameof(Index));
        }

        private bool TagExists(int id)
        {
            return _tagRepo.Exist(id);
        }
    }
}
