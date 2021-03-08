using System;

namespace MvcSportsClub.Models {
    public class Workout {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Instructor { get; set; }
        public string Location { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int CapacityLeft { get; set; }
    }
}
