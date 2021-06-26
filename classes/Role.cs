using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace mamasbogrim.classes
{
    class Role
    {
        /// <summary>
        /// This class represents a Role in this exercise.
        /// </summary>
        public int roleID { get; set; }
        public string roleName { get; set; }
        public List<Rank> rankList { get; }
        public Role(int _roleID)
        {
            rankList = new List<Rank>();

            string roleQuery = string.Format(ConfigurationManager.AppSettings.Get("getRoleByID"), _roleID);
            string rankQuery = string.Format(ConfigurationManager.AppSettings.Get("getRanksByRoleID"), _roleID);
            
            Dictionary<string, List<Dictionary<string, string>>> result = DatabaseConnection.Query(roleQuery);
            Dictionary<string, List<Dictionary<string, string>>> rankQueryResult = DatabaseConnection.Query(rankQuery);
           
            // DatabaseConnection.printQueryResults(result);
            // DatabaseConnection.printQueryResults(rankQueryResult);

            roleID = Int32.Parse(result["0"][0]["roleID"]);
            roleName = result["0"][1]["roleName"];

            for (int i = 0; i < rankQueryResult.Count; i++)
            {
                for (int j = 0; j < rankQueryResult[i.ToString()].Count; j++)
                {
                    rankList.Add(new Rank(Int32.Parse(rankQueryResult[i.ToString()][j]["rankID"])));
                }
            }
        }
        public override string ToString()
        {
            string temp = $"Role object, roleID: {roleID}, RoleName: {roleName}, roleRanks: ";
            for (int i = 0; i < rankList.Count; i++)
            {
                temp += $"{rankList[i].rankName} ";
            }
            return temp;
        }

        public bool isRankInRole(int rankID)
        {
            for (int i = 0; i < rankList.Count; i++)
            {
                if(rankList[i].rankID == rankID)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
