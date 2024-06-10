﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VnStockproxx.Models;

namespace VnStockproxx.Controllers
{
    public class PostsController : Controller
    {
        private readonly PostRepository _postRepo;
        private readonly CateRepository _cateRepo;

        public PostsController(VnStockproxxDbContext context)
        {
            this._postRepo = new PostRepository(context);
            this._cateRepo = new CateRepository(context);
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            var categories = await _cateRepo.GetAll();
            var posts = await _postRepo.GetAll();
            var qr = from post in posts
                     join category in categories on post.CateId equals category.Id
                     select new
                     {
                         Id = post.Id,
                         Title = post.Title,
                         ViewCount = post.ViewCount,
                         CreatedDate = post.CreatedDate,
                         UpdatedDate = post.UpdatedDate,
                         CategoryName = category.CategoryName
                     };
            ViewBag.PostsWithCategory = qr.ToList();
            return View();
        }

        // GET: Posts/Create
        public async Task<IActionResult> Create()
        {
            var category = await _cateRepo.GetAll();
            ViewData["CateId"] = new SelectList(category, "Id", "CategoryName");
            return View();
        }

        // POST: Posts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Post post)
        {
            if (ModelState.IsValid)
            {
                await _postRepo.Add(post);
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
            var category = await _cateRepo.GetAll();
            ViewData["CateId"] = new SelectList(category, "Id", "CategoryName");
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
                    await _postRepo.Update(post);
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
            return RedirectToAction(nameof(Index));
        }

        //GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
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
