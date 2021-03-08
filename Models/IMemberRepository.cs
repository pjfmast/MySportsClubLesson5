using MvcSportsClub.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcSportsClub.Models {
    public interface IMemberRepository : IRepository<Member> {

        // specifieke CRUD methoden voor Member
    }
}
