using System;
using System.Collections.Generic;
using System.Text;

namespace mamasbogrim.classes
{
    class Role
    {
        public int roleID { get; set; }
        public string roleName { get; set; }
        public List<Rank> rankList { get; }
        public Role()
        {

        }
    }
}
