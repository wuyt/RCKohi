using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RCKohi.Data;
using RCKohi.Models.Demo;
using X.PagedList;

namespace RCKohi.Controllers
{
    [Authorize]
    public class AnimesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnimesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Animes
        public async Task<IActionResult> Index(int? page, string searchString, string sortOrder)
        {
            var animes = from s in _context.Anime
                         select s;

            ViewData["CurrentFilter"] = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                animes = animes.Where(s => s.Name.Contains(searchString));
            }

            ViewData["SortOrder"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["NumberSortParm"] = sortOrder == "Number" ? "number_desc" : "Number";
            switch (sortOrder)
            {
                case "name_desc":
                    animes = animes.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    animes = animes.OrderBy(s => s.BroadcastDate);
                    break;
                case "date_desc":
                    animes = animes.OrderByDescending(s => s.BroadcastDate);
                    break;
                case "Number":
                    animes = animes.OrderBy(s => s.Number);
                    break;
                case "number_desc":
                    animes = animes.OrderByDescending(s => s.Number);
                    break;
                default:
                    animes = animes.OrderBy(s => s.Name);
                    break;
            }

            var pageNumber = page ?? 1;
            return View(await animes.AsNoTracking().ToPagedListAsync(pageNumber, 10));
        }

        // GET: Animes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anime = await _context.Anime
                .SingleOrDefaultAsync(m => m.ID == id);
            if (anime == null)
            {
                return NotFound();
            }

            return View(anime);
        }

        // GET: Animes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Animes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Number,BroadcastDate,IndexShow")] Anime anime)
        {
            if (ModelState.IsValid)
            {
                _context.Add(anime);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(anime);
        }

        // GET: Animes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anime = await _context.Anime.SingleOrDefaultAsync(m => m.ID == id);
            if (anime == null)
            {
                return NotFound();
            }
            return View(anime);
        }

        // POST: Animes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Number,BroadcastDate,IndexShow")] Anime anime)
        {
            if (id != anime.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(anime);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimeExists(anime.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(anime);
        }

        // GET: Animes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anime = await _context.Anime
                .SingleOrDefaultAsync(m => m.ID == id);
            if (anime == null)
            {
                return NotFound();
            }

            return View(anime);
        }

        // POST: Animes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var anime = await _context.Anime.SingleOrDefaultAsync(m => m.ID == id);
            _context.Anime.Remove(anime);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimeExists(int id)
        {
            return _context.Anime.Any(e => e.ID == id);
        }
    }
}
