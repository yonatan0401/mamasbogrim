using mamasbogrim.classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace mamasbogrim
{
    class Maternityward
    {
        public string MaternityName { get; set; }
        public List<Employee> EmployeeList { get; set; }
        public Maternityward(string Name)
        {
            MaternityName = Name;
            EmployeeList = new List<Employee>();
        }

        /// <summary>
        /// Loads all available employees from the database.
        /// </summary>
        public void loadEmployees()
        {
            Dictionary<string, List<Dictionary<string, string>>> result = DatabaseConnection.Query(ConfigurationManager.AppSettings.Get("loadEmployees"));
            //DatabaseConnection.printQueryResults(result);
            for (int i = 0; i < result.Count; i++)
            {
                EmployeeList.Add(new Employee(int.Parse(result[i.ToString()][0]["employeeID"])));
            }
        }
    }
}
