using System;
using System.Collections.Generic;
using System.Text;

namespace mamasbogrim.classes
{
    static class Roles
    {
        /// <summary>
        /// All available Roles accsessable in this class.
        /// </summary>
        public static Dictionary<int, Role> roleDict;
        static Roles()
        {
            Dictionary<int, Role> RolesDict = new Dictionary<int, Role>();
            string queryshit = "select roleID from roles";
            Dictionary<string, List<Dictionary<string, string>>> result = DatabaseConnection.Query(queryshit);
            for (int i = 0; i < result.Count; i++)
            {
                int roleID = int.Parse(result[i.ToString()][0]["roleID"]);
                RolesDict.Add(roleID, new Role(roleID));
            }
            roleDict =  RolesDict;
        }

        public static void printRoles()
        {
            for (int i = 1; i <= roleDict.Count; i++)
            {
                Console.WriteLine(roleDict[i].roleName + " has the ranks of:");
                for (int j = 0; j < roleDict[i].rankList.Count; j++)
                {
                    Console.Write(roleDict[i].rankList[j].rankName + " ");
                }
                Console.WriteLine("\n");
            }
        }
    }
}
