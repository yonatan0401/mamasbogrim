using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace mamasbogrim
{
    class Employee
    {
        public static double MinimumWage = 29.19;
        public string EmployeeName { get; set; }
        // public bool isManagement { get; set; } // is the employee in management or professional (cleaner, cooker / nurse, doctor  . . . )
        public string roleName { get; set; } 
        public int Id { get; set; }
        public double EmployeehourlyRate { get; set; }

        public Employee(string _EmployeeName, string roleID)
        {
            EmployeeName = _EmployeeName;
            EmployeehourlyRate = MinimumWage;
            // roleName = _professionName;
            // Id = _Id;
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
            return $"{EmployeeName} is a common Employee.\nwith the profession of \"{roleName}\".\nhis current monthly wage is: getCurrentIncome(change this to tamplate string when  the function is done) shekels.";
        }
        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Employee employee = (Employee)obj;
                return Id == employee.Id;
            }
        }
    }
}
