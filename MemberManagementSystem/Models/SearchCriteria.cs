using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberManagementSystem.Models
{
    public class SearchCriteria
    {
        public int? MinPoints { get; set; }
        public int? MaxPoints { get; set; }
        public int? Status { get; set; }
    }
}
