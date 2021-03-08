using Microsoft.AspNetCore.Identity;
using MvcSportsClub.Models;
using MySportsClub.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcSportsClub.Data {
    public static class FakeData {
       
        public static List<Member> FakeMembers
            = new List<Member> {
                new Member{ID= 1, Name = "Esther", StartMembership = new DateTime(2014, 10, 8)},
                new Member{ID= 2, Name = "Anton", StartMembership = new DateTime(2018, 1, 5)},
                new Member{ID= 3, Name = "Manon", StartMembership = new DateTime(2016, 6, 1)},
                new Member{ID= 4, Name = "Joke", StartMembership = new DateTime(2019, 1, 10)},
                new Member{ID= 5, Name = "Jeroen", StartMembership = new DateTime(2020, 1, 15)},
                new Member{ID= 6, Name = "Ellen", StartMembership = new DateTime(2010, 5, 8)},
                new Member{ID= 7, Name = "Eva", StartMembership = new DateTime(2012, 9, 1)},
                new Member{ID= 8, Name = "Anke", StartMembership = new DateTime(2015, 12, 10)},
                new Member{ID= 9, Name = "Koen", StartMembership = new DateTime(2015, 4, 16)},
            };

        private static DateTime today = DateTime.Now;

        public static List<Workout> FakeWorkouts
            = new List<Workout> {
                new Workout{ID= 1, Title = "Yin Yoga", Location = "Yoga studio", StartTime = today.NextDayAt(DayOfWeek.Monday, 10, 15), EndTime = today.NextDayAt(DayOfWeek.Monday, 11, 0), CapacityLeft = 35},
                new Workout{ID= 2, Title = "Pilates", Location = "Yoga studio", StartTime = today.NextDayAt(DayOfWeek.Monday, 17, 0), EndTime = today.NextDayAt(DayOfWeek.Monday, 18, 0), CapacityLeft = 30},
                new Workout{ID= 3, Title = "Hot Yoga", Location ="Yoga studio", StartTime = today.NextDayAt(DayOfWeek.Tuesday, 10, 15), EndTime = today.NextDayAt(DayOfWeek.Tuesday, 11, 15), CapacityLeft=35},
                new Workout{ID= 4, Title = "Club Power", Location = "Room 1",StartTime = today.NextDayAt(DayOfWeek.Tuesday, 19, 15), EndTime = today.NextDayAt(DayOfWeek.Tuesday, 20, 15), CapacityLeft=30},
                new Workout{ID= 5, Title = "XCO", Location = "Room 2",StartTime = today.NextDayAt(DayOfWeek.Wednesday, 9, 15), EndTime = today.NextDayAt(DayOfWeek.Wednesday, 10, 15), CapacityLeft=25},
                new Workout{ID= 6, Title = "B&K Training", Location = "Boxing Area",StartTime = today.NextDayAt(DayOfWeek.Wednesday, 10, 15), EndTime = today.NextDayAt(DayOfWeek.Wednesday, 11, 15), CapacityLeft= 16},
                new Workout{ID= 7, Title = "Callanetics", Location = "Room 1", StartTime = today.NextDayAt(DayOfWeek.Wednesday, 19, 15), EndTime = today.NextDayAt(DayOfWeek.Wednesday, 20, 0), CapacityLeft=35},
                new Workout{ID= 8, Title = "Spinning", Location = "Room 4", StartTime = today.NextDayAt(DayOfWeek.Thursday, 10, 15), EndTime = today.NextDayAt(DayOfWeek.Thursday, 11, 15), CapacityLeft=18},
                new Workout{ID= 9, Title = "Vinyasa Yoga", Location ="Yoga studio", StartTime = today.NextDayAt(DayOfWeek.Thursday, 17, 15), EndTime = today.NextDayAt(DayOfWeek.Thursday, 18, 15), CapacityLeft=30},
                new Workout{ID= 10, Title = "TBW", Location = "Room 1", StartTime = today.NextDayAt(DayOfWeek.Friday, 10, 15), EndTime = today.NextDayAt(DayOfWeek.Friday, 11, 0), CapacityLeft=35},
                new Workout{ID= 11, Title = "Shred and Burn", Location = "Room 2", StartTime = today.NextDayAt(DayOfWeek.Friday, 10, 30), EndTime = today.NextDayAt(DayOfWeek.Friday, 11, 15), CapacityLeft=12},
                new Workout{ID= 12, Title = "Cycle Interval", Location = "Cycle Area", StartTime = today.NextDayAt(DayOfWeek.Friday, 18, 15), EndTime = today.NextDayAt(DayOfWeek.Friday, 19, 15), CapacityLeft=8},
                new Workout{ID= 13, Title = "Spinning", Location = "Cycle Area", StartTime = today.NextDayAt(DayOfWeek.Saturday, 9, 15), EndTime = today.NextDayAt(DayOfWeek.Saturday, 10, 15), CapacityLeft=12},
                new Workout{ID= 14, Title = "Cycle & Abs", Location = "Cycle Area", StartTime = today.NextDayAt(DayOfWeek.Sunday, 10, 15), EndTime = today.NextDayAt(DayOfWeek.Sunday, 11, 15), CapacityLeft=6},
            };


        public static List<Enrollment> FakeEnrollments
            = new List<Enrollment>  {
                new Enrollment{ID= 1, MemberID=1, WorkoutID=1},
                new Enrollment{ID= 2, MemberID=1, WorkoutID=4},
                new Enrollment{ID= 3, MemberID=2, WorkoutID=2},
                new Enrollment{ID= 4, MemberID=2, WorkoutID=5},
                new Enrollment{ID= 5, MemberID=2, WorkoutID=14},
                new Enrollment{ID= 6, MemberID=3, WorkoutID=4},
                new Enrollment{ID= 7, MemberID=3, WorkoutID=8},
                new Enrollment{ID= 8, MemberID=4, WorkoutID=1},
                new Enrollment{ID= 9, MemberID=4, WorkoutID=4},
                new Enrollment{ID= 10,MemberID=4, WorkoutID=10},
                new Enrollment{ID= 11,MemberID=4, WorkoutID=13},
            };

    }
}
