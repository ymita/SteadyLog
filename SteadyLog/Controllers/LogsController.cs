using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SteadyLog.Data;
using SteadyLog.Models;
using SteadyLog.Models.LogViewModels;
using SteadyLog.Services;

namespace SteadyLog.Controllers
{
    public class LogsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly LogService _service;

        public LogsController(
            ApplicationDbContext context,
            LogService service)
        {
            _context = context;
            _service = service;
        }

        // GET: Logs
        public async Task<IActionResult> Index()
        {
            var vms = _service.GetLogViewModels();
            return View(vms);
            //return View(await _context.Logs.ToListAsync());
        }

        // GET: Logs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var logViewmodel = _service.GetLogViewModelById(id);
            //var log = await _context.Logs
            //    .FirstOrDefaultAsync(m => m.Id == id);
            if (logViewmodel == null)
            {
                return NotFound();
            }

            return View(logViewmodel);
        }

        // GET: Logs/Create
        public IActionResult Create()
        {
            var vm = _service.GetNewLogSaveViewModel();
            
            return View(vm);
        }

        // POST: Logs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ActivityId,Amount")] LogSaveViewModel logSaveViewModel)
        {
            if (ModelState.IsValid)
            {
                _service.AddViewModel(logSaveViewModel);
                //_context.Add(log);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(logSaveViewModel);
        }

        // GET: Logs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logSaveViewModel = _service.GetLogSaveViewModelById(id);
            //var log = await _context.Logs.FindAsync(id);
            if (logSaveViewModel == null)
            {
                return NotFound();
            }
            return View(logSaveViewModel);
        }

        // POST: Logs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ActivityId,Amount,Comment")] LogSaveViewModel logSaveViewModel)
        {
            //if (id != log.Id)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                try
                {
                    _service.UpdateViewModel(logSaveViewModel);
                    //_context.Update(log);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                    //if (!LogExists(log.Id))
                    //{
                    //    return NotFound();
                    //}
                    //else
                    //{
                    //    throw;
                    //}
                }
                return RedirectToAction(nameof(Index));
            }
            return View(logSaveViewModel);
        }

        // GET: Logs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var log = await _context.Logs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (log == null)
            {
                return NotFound();
            }

            return View(log);
        }

        // POST: Logs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var log = await _context.Logs.FindAsync(id);
            _context.Logs.Remove(log);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LogExists(int id)
        {
            return _context.Logs.Any(e => e.Id == id);
        }
    }
}
