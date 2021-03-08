using Microsoft.EntityFrameworkCore;
using MvcSportsClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcSportsClub.Data {
    public class EFWorkoutRepository : IWorkoutRepository {
        private readonly SportsClubContext context;

        public EFWorkoutRepository(SportsClubContext context) {
            this.context = context;
        }

        public async Task<IEnumerable<Workout>> FindAllAsync()
            => await context.Workouts
            .OrderBy(workout => workout.StartTime)
            .ToListAsync();

        public async Task<Workout> FindAsync(int id)
            => await context.Workouts.FindAsync(id);

        public async Task InsertAsync(Workout workout) {
            await context.Workouts.AddAsync(workout);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Workout workout) {
            context.Workouts.Update(workout);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Workout workout) {
            context.Workouts.Remove(workout);
            await context.SaveChangesAsync();
        }

        //public async Task<bool> TryEnroll(Workout workout, Member member) {}
    }
}
