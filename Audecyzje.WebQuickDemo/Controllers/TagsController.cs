using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Audecyzje.WebQuickDemo.Data;
using Audecyzje.WebQuickDemo.Models;
using System.Text.RegularExpressions;

namespace Audecyzje.WebQuickDemo.Controllers
{
    public class TagsController : Controller
    {
        private readonly WarsawContext _context;

        public TagsController(WarsawContext context)
        {
            _context = context;
        }

        // GET: Tags
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tags.Include(t=> t.LinkedDecisions).ToListAsync());
        }

        public async Task<IActionResult> ClearAllTagRelations()
        {
            var dgs = _context.DecisionTags.ToList();
            foreach (var dg in dgs)
            {
                _context.DecisionTags.Remove(dg);
            }
            await _context.SaveChangesAsync();
            return View("Index");
        }
        public async Task<IActionResult> CheckAllDecisionsContentWithTagsRegexp()
        {
            var tags = await _context.Tags.Where(x => x.RegExp != null).ToListAsync();
            var decisions = await _context.Descisions.ToListAsync();
            foreach (var tag in tags)
            {
                foreach (var dec in decisions)
                {
                    var regexp = tag.RegExp;
                    if (Regex.IsMatch(dec.Content, regexp))
                    {
                        DecisionTag dt = new DecisionTag()
                        {
                            DecisionID = dec.ID,
                            TagID = tag.ID
                        };
                        _context.DecisionTags.Add(dt);
                    }
                }
                await _context.SaveChangesAsync();
            }
            return View("Index");
        }
        // GET: Tags/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await _context.Tags
                .SingleOrDefaultAsync(m => m.ID == id);
            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }

        // GET: Tags/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tags/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,TagName,RegExp")] Tag tag)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tag);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tag);
        }

        // GET: Tags/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await _context.Tags.SingleOrDefaultAsync(m => m.ID == id);
            if (tag == null)
            {
                return NotFound();
            }
            return View(tag);
        }

        // POST: Tags/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,TagName,RegExp")] Tag tag)
        {
            if (id != tag.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tag);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TagExists(tag.ID))
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
            return View(tag);
        }

        // GET: Tags/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await _context.Tags
                .SingleOrDefaultAsync(m => m.ID == id);
            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }

        // POST: Tags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tag = await _context.Tags.SingleOrDefaultAsync(m => m.ID == id);
            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TagExists(int id)
        {
            return _context.Tags.Any(e => e.ID == id);
        }
    }
}
