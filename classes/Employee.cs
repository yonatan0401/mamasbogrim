using mamasbogrim.classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Text;

namespace mamasbogrim
{
    class Employee
    {
        public static double MinimumWage = 29.19;
        public string employeeName { get; set; }
        public Role employeeRole { get; set; } 
        public int employeeID { get; set; }
        public double EmployeehourlyRate { get; set; }

        public Employee(int _employeeID)
        {
            EmployeehourlyRate = MinimumWage;
            string employeeQuery = string.Format(ConfigurationManager.AppSettings.Get("getEmployeeByID"), _employeeID);
            Dictionary<string, List<Dictionary<string, string>>> result = DatabaseConnection.Query(employeeQuery);

            // DatabaseConnection.printQueryResults(result);
            
            employeeID = Int32.Parse(result["0"][0]["employeeID"]);
            employeeName = result["0"][1]["employeeName"];
            employeeRole = new Role(Int32.Parse(result["0"][2]["roleID"]));
        }

        /// <summary>
        /// calculate the current monthes salery.
        /// </summary>
        /// <returns>The current months salery.</returns>
       /* public double getCurrentIncome()
        {
            DateTime dt = DateTime.Now;
            return MonthlyWorkingHours[dt.Month] * EmployeehourlyRate;
        }*/
        public override string ToString()
        {
            return $"{employeeName} is an Employee.\nwith the profession of \"{employeeRole.roleName}\".\nhis current monthly wage is: getCurrentIncome(change this to tamplate string when  the function is done) shekels.";
        }
       /* public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Employee employee = (Employee)obj;
                return employeeID == employee.employeeID;
            }
        }*/
    }
}
