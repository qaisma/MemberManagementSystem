using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace MemberManagementSystem
{
    public static class Enums
    {
        public enum Statuses
        {
            [EnumMember(Value = "0")]
            ACTIVE,


            [EnumMember(Value = "1")]
            INACTIVE
        }
    }
}
