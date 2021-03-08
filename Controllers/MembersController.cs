using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcSportsClub.Models;

namespace MvcSportsClub.Controllers {
    // todo lesson 4-17: authorisatie met rollen
    [Authorize(Roles = "Admin,Desk")]
    public class MembersController : Controller
    {
        private readonly IMemberRepository repository;


        public MembersController(IMemberRepository context)
        {
            repository = context;
        }

        // GET: Members
        public async Task<IActionResult> Index()
        {
            return View(await repository.FindAllAsync());
        }

        // GET: Members/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await repository.FindAsync(id.Value);

            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // GET: Members/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,StartMembership")] Member member)
        {
            if (member.StartMembership.Date < DateTime.Now.Date) {
                ModelState.AddModelError("", "Date cannot be in the past.");
            }
    
            if (ModelState.IsValid)
            {
                await repository.InsertAsync(member);
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        // GET: Members/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await repository.FindAsync(id.Value);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,StartMembership")] Member member)
        {
            if (id != member.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await repository.UpdateAsync(member);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await MemberExists(member.ID))
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
            return View(member);
        }

        // GET: Members/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await repository.FindAsync(id.Value);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var member = await repository.FindAsync(id);
            await repository.DeleteAsync(member);
            return RedirectToAction(nameof(Index));
        }

        // GET: Workouts/Delete/5
        public async Task<IActionResult> DeleteEnrollment(int? id) {
            if (id == null) {
                return NotFound();
            }

            var workout = await repository.FindAsync(id.Value);
            if (workout == null) {
                return NotFound();
            }

            return View(workout);
        }

 
        private async Task<bool> MemberExists(int id)
        {
            Member member = await repository.FindAsync(id);
            return member != null;
        }
    }
}
