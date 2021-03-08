using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MvcSportsClub.Models;
using System.Collections.Generic;

namespace MvcSportsClub.Data {
    // todo lesson 4-01: extend from IdentityDbContext
    public class SportsClubContext:IdentityDbContext {
        public SportsClubContext(DbContextOptions<SportsClubContext> options)
            // todo lesson 4-01b. call base class constructor (to solve error message when adding a migration)
            : base(options){
        }



        // This code creates a DbSet property for each entity set.
        // In Entity Framework terminology;
        //   - an entity set typically corresponds to a database table, 
        //   - and an entity corresponds to a row in the table.
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        /*        Property names for collections are typically plural(Students rather than Student),
         *        but developers disagree about whether table names should be pluralized or not.
         *        Here we override the default behavior by specifying singular table names in the DbContext. 
         *        To do that, add the following highlighted code after the last DbSet property.
        */
        // OnModelCreating 'stuurt' de creatie van migrations
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            // todo lesson 4-04a in Package Manager Console: add-migration
            // todo lesson 4-04b in Package Manager Console: update-database
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Member>().ToTable("Member");
            modelBuilder.Entity<Workout>().ToTable("Workout");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");

            modelBuilder.Entity<Member>().HasData(FakeData.FakeMembers);
            modelBuilder.Entity<Workout>().HasData(FakeData.FakeWorkouts);
            modelBuilder.Entity<Enrollment>().HasData(FakeData.FakeEnrollments);

        }
    }
}
