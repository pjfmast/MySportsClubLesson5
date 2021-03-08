using MvcSportsClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcSportsClub.Data {
    public interface IWorkoutRepository : IRepository<Workout> {

        //Task<bool> TryEnroll(Workout workout, Member member);
        // Voor complexere applicaties: SaveChanges in Unit of Work 

    }
}
