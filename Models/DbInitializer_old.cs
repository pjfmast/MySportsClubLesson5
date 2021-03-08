using MvcSportsClub.Models;
using System;
using System.Linq;

namespace MvcSportsClub.Data {
    public class DbInitializer_old {
        public static void Initialize(SportsClubContext context) {
            context.Database.EnsureCreated();

            // Look for any members:
            if (context.Members.Any()) {
                return; // table has been seeded
            }

            var members = new Member[] {
                new Member{Name = "Esther", StartMembership = new DateTime(2014, 10, 8)},
                new Member{Name = "Anton", StartMembership = new DateTime(2018, 1, 5)},
                new Member{Name = "Manon", StartMembership = new DateTime(2016, 6, 1)},
                new Member{Name = "Joke", StartMembership = new DateTime(2019, 1, 10)},
                new Member{Name = "Jeroen", StartMembership = new DateTime(2020, 1, 15)},
                new Member{Name = "Ellen", StartMembership = new DateTime(2010, 5, 8)},
                new Member{Name = "Eva", StartMembership = new DateTime(2012, 9, 1)},
                new Member{Name = "Anke", StartMembership = new DateTime(2015, 12, 10)},
                new Member{Name = "Koen", StartMembership = new DateTime(2015, 4, 16)},
            };

            foreach (var member in members) {
                context.Members.Add(member);
            }

            context.SaveChanges();


            // Look for any Workouts:
            if (context.Workouts.Any()) {
                return; // table has been seeded
            }

            var workouts = new Workout[] {
                new Workout{ Title = "Yin Yoga", Location = "Yoga studio", StartTime = new DateTime(2020,2,5,9,15,0), EndTime = new DateTime(2020,2,5,10,15,0)},
                new Workout{ Title = "Pilates", Location = "Yoga studio", StartTime = new DateTime(2020,2,5,10,15,0), EndTime = new DateTime(2020,2,5,11,15,0)},
                new Workout{ Title = "Hot Yoga", Location ="Yoga studio", StartTime = new DateTime(2020,2,5,15,0,0), EndTime = new DateTime(2020,2,5,16,0,0)},
                new Workout{ Title = "Club Power", Location = "Room 1",StartTime = new DateTime(2020,2,5,9,15,0), EndTime = new DateTime(2020,2,5,10,15,0)},
                new Workout{ Title = "XCO", Location = "Room 2",StartTime = new DateTime(2020,2,4,18,30,0), EndTime = new DateTime(2020,2,4,19,30,0)},
                new Workout{ Title = "B&K Training", Location = "Boxing Area",StartTime = new DateTime(2020,2,4,18,0,0), EndTime = new DateTime(2020,2,4,19,0,0)},
                new Workout{ Title = "Callanetics", Location = "Room 1", StartTime = new DateTime(2020,2,4,16,0,0), EndTime = new DateTime(2020,2,4,17,0,0)},
                new Workout{ Title = "Spinning", Location = "Room 4", StartTime = new DateTime(2020,2,4,16,0,0), EndTime = new DateTime(2020,2,4,17,0,0)},
                new Workout{ Title = "Vinyasa Yoga", Location ="Yoga studio", StartTime = new DateTime(2020,2,4,19,30,0), EndTime = new DateTime(2020,2,4,20,30,0)},
                new Workout{ Title = "TBW", Location = "Room 1", StartTime = new DateTime(2020,2,4,17,0,0), EndTime = new DateTime(2020,2,4,18,0,0)},
                new Workout{ Title = "Shred and Burn", Location = "Room 2", StartTime = new DateTime(2020,2,6,20,30,0), EndTime = new DateTime(2020,2,6,21,30,0)},
                new Workout{ Title = "Cycle Interval", Location = "Cycle Area", StartTime = new DateTime(2020,2,6,18,0,0), EndTime = new DateTime(2020,2,6,19,0,0)},
                new Workout{ Title = "Spinning", Location = "Cycle Area", StartTime = new DateTime(2020,2,6,17,0,0), EndTime = new DateTime(2020,2,6,18,0,0)},
                new Workout{ Title = "Cycle & Abs", Location = "Cycle Area", StartTime = new DateTime(2020,2,6,20,0,0), EndTime = new DateTime(2020,6,4,21,0,0)},
            };

            foreach (var workout in workouts) {
                context.Workouts.Add(workout);
            }

            context.SaveChanges();


            // Look for any Enrollments:
            if (context.Enrollments.Any()) {
                return; // table has been seeded
            }

            var enrollments = new Enrollment[] {
                new Enrollment{MemberID=1, WorkoutID=1},
                new Enrollment{MemberID=1, WorkoutID=4},
                new Enrollment{MemberID=2, WorkoutID=2},
                new Enrollment{MemberID=2, WorkoutID=5},
                new Enrollment{MemberID=2, WorkoutID=14},
                new Enrollment{MemberID=3, WorkoutID=4},
                new Enrollment{MemberID=3, WorkoutID=8},
                new Enrollment{MemberID=4, WorkoutID=1},
                new Enrollment{MemberID=4, WorkoutID=4},
                new Enrollment{MemberID=4, WorkoutID=10},
                new Enrollment{MemberID=4, WorkoutID=13},
            };

            foreach (var enrollment in enrollments) {
                context.Enrollments.Add(enrollment);
            }

            context.SaveChanges();

        }
    }
}
