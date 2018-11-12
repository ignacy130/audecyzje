using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Audecyzje.WebQuickDemo.Data;
using Audecyzje.WebQuickDemo.Models;
using Audecyzje.WebQuickDemo.Helpers;
using System.Globalization;
using Audecyzje.Core.Domain;
using Audecyzje.Infrastructure;

namespace Audecyzje.WebQuickDemo.Controllers
{
    public class DecisionsController : Controller
    {
        private readonly WarsawContext _context;

        public DecisionsController(WarsawContext context)
        {
            _context = context;
        }

        // GET: Decisions
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
			int pageSize = 10;
			if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var decisions = from s in _context.Decisions
                            select s;

            if (!String.IsNullOrEmpty(searchString))
            {
				decisions = decisions.Where(s => new CultureInfo("pl").CompareInfo.IndexOf(s.Content, searchString, CompareOptions.IgnoreCase) >= 0
												|| new CultureInfo("pl").CompareInfo.IndexOf(s.DecisionNumber, searchString, CompareOptions.IgnoreCase) >= 0);
            }

            switch (sortOrder)
            {
                case "name_desc":
                    decisions = decisions.OrderByDescending(s => s.DecisionNumber);
                    break;
                case "Date":
                    decisions = decisions.OrderBy(s => s.SubmissionDate);
                    break;
                case "date_desc":
                    decisions = decisions.OrderByDescending(s => s.SubmissionDate);
                    break;
                default:
                    decisions = decisions.OrderBy(s => s.Id);
                    break;
            }
            
            return View(await PaginatedList<Decision>.CreateAsync(decisions.AsNoTracking(), page ?? 1, pageSize));
        }


		public async Task<IActionResult> Search(string query, int inFirstCharacters, int inLastCharacters, int? page)
		{
			ViewData["query"] = query;
			ViewData["inFirstCharacters"] = inFirstCharacters;
			ViewData["inLastCharacters"] = inLastCharacters;

			
			var decisions = from s in _context.Decisions
							select s;
			if (!string.IsNullOrEmpty(query))
			{
				if (inFirstCharacters > 0)
				{
					decisions = decisions.Where(d => new CultureInfo("pl").CompareInfo.IndexOf(d.Content, query, CompareOptions.IgnoreCase) >= 0);
				}
				if (inLastCharacters > 0)
				{
					decisions = decisions.Where(d => new CultureInfo("pl").CompareInfo.IndexOf(d.Content.Substring(d.Content.Length-inLastCharacters, 999), query, CompareOptions.IgnoreCase) >= 0);
				}
			}
			else
			{
				decisions = decisions.Take(0);
			}
			var pageSize = 10;
			return View("Search", await PaginatedList<Decision>.CreateAsync(decisions.AsNoTracking(), page ?? 1, pageSize));
		}

		// GET: Decisions/Details/5
		public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var decision = await _context.Decisions.Include(d=>d.Localizations)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (decision == null)
            {
                return NotFound();
            }

            return View(decision);
        }

        // GET: Decisions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Decisions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SubmissionDate,Content")] Decision decision)
        {
            if (ModelState.IsValid)
            {
                _context.Add(decision);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(decision);
        }

        // GET: Decisions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var decision = await _context.Decisions.SingleOrDefaultAsync(m => m.Id == id);
            if (decision == null)
            {
                return NotFound();
            }
            return View(decision);
        }

        // POST: Decisions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,SubmissionDate,Content")] Decision decision)
        {
            if (id != decision.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(decision);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DecisionExists(decision.Id))
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
            return View(decision);
        }

        // GET: Decisions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var decision = await _context.Decisions
                .SingleOrDefaultAsync(m => m.Id == id);
            if (decision == null)
            {
                return NotFound();
            }

            return View(decision);
        }

        // POST: Decisions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var decision = await _context.Decisions.SingleOrDefaultAsync(m => m.Id == id);
            _context.Decisions.Remove(decision);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DecisionExists(int id)
        {
            return _context.Decisions.Any(e => e.Id == id);
        }
    }
}
