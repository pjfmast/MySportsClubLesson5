using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcSportsClub.Data;
using MvcSportsClub.Models;

namespace MvcSportsClub.Controllers
{
    // todo lesson 4-16a - Apply authorisation without roles. 
    // Here: only allow access to any authenticated user
    [Authorize]
    public class WorkoutsController : Controller {
        private readonly IWorkoutRepository repository;

        // Indien de IWorkoutrepository niet ConfigureServices geregistreerd:
        // InvalidOperationException: Unable to resolve service for type 'MvcSportsClub.Models.IWorkoutRepository' while attempting to activate 'MvcSportsClub.Controllers.WorkoutsController'.
        public WorkoutsController(IWorkoutRepository repository) {
            this.repository = repository;
        }

        // GET: Workouts
        // todo lesson 4-16b: allow acces to non-authenticated users
        [AllowAnonymous]
        public async Task<IActionResult> Index() {
            return View(await repository.FindAllAsync());
        }


        // GET: Workouts/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null) {
                return NotFound();
            }

            var workout = await repository.FindAsync(id.Value);
            if (workout == null) {
                return NotFound();
            }

            return View(workout);
        }

        // GET: Workouts/Create
        public IActionResult Create() {
            return View();
        }

        // POST: Workouts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Location,StartTime,EndTime")] Workout workout) {
            if (ModelState.IsValid) {
                await repository.InsertAsync(workout);
                return RedirectToAction(nameof(Index));
            }
            return View(workout);
        }

        // GET: Workouts/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var workout = await repository.FindAsync(id.Value);
            if (workout == null) {
                return NotFound();
            }
            return View(workout);
        }

        // POST: Workouts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Location,StartTime,EndTime")] Workout workout) {
            if (id != workout.ID) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                await repository.UpdateAsync(workout);
                return RedirectToAction(nameof(Index));
            }
            return View(workout);
        }

        // GET: Workouts/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            Workout workout = await repository.FindAsync(id.Value);
            if (workout == null) {
                return NotFound();
            }

            return View(workout);
        }


        // POST: Workouts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            Workout workout = await repository.FindAsync(id);
            await repository.DeleteAsync(workout);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> WorkoutExists(int id) {
            Workout workout = await repository.FindAsync(id);
            return workout != null;
        }
    }
}
