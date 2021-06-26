using System;
using System.Collections.Generic;
using System.Text;

namespace mamasbogrim.classes
{
    static class Roles
    {
        /// <summary>
        /// Finish this thing maybe ? ? ? ? ? ? ? ? 
        /// </summary>
        public static Dictionary<int, Rank> roleDict;
        static Roles()
        {
            Dictionary<int, Rank> RolesDict = new Dictionary<int, Rank>();
            Console.WriteLine("starting query");
            string queryshit = "select roleID from roles";
            Dictionary<string, List<Dictionary<string, string>>> result = DatabaseConnection.Query(queryshit);
            for (int i = 0; i < result.Count; i++)
            {
                int rankID = int.Parse(result[i.ToString()][0]["roleID"]);
                RolesDict.Add(rankID, new Rank(rankID));
            }
            roleDict = RolesDict;
        }

    }
}
