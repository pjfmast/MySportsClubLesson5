using Microsoft.EntityFrameworkCore;
using MvcSportsClub.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcSportsClub.Models {
    public class EFMemberRepository : IMemberRepository {

        private readonly SportsClubContext context;

        public EFMemberRepository(SportsClubContext context) {
            this.context = context;
        }
        public async Task DeleteAsync(Member workout) {
            context.Members.Remove(workout);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Member>> FindAllAsync()
              => await context.Members
                .OrderBy(member => member.Name)
                .ToListAsync();

        public async Task<Member> FindAsync(int id) {

            Member member = await
                context.Members
                .Include(member => member.Enrollments)
                .ThenInclude(enrollment => enrollment.Workout)
                .AsNoTracking() //  improves performance in scenarios where the entities returned won't be updated in the current context's lifetime. 
                .FirstOrDefaultAsync(member => member.ID == id);

            member.Enrollments = member.Enrollments.OrderBy(enrollment => enrollment.Workout.StartTime).ToHashSet();

            return member;
        }


        public async Task InsertAsync(Member member) {
            await context.Members.AddAsync(member);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Member member) {
            context.Members.Update(member);
            await context.SaveChangesAsync();
        }
    }
}
