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
        public List<Employee> LoggedInEmployees { get; set; }
        public Maternityward(string Name)
        {
            MaternityName = Name;
            EmployeeList = new List<Employee>();
            LoggedInEmployees = new List<Employee>();
        }
        /// <summary>
        /// This Method adds employess to the list from a given employee list.
        /// </summary>
        /// <param name="Employees">A list of employees to add.</param>
       /* public void addEmployees(List<Employee> Employees)
        {
            for (int i = 0; i < Employees.Count; i++)
            {
                EmployeeList.Add(Employees[i]);
            }
        }*/
        /// <summary>
        /// This Method add's a single employee to the employee list.
        /// </summary>
        /// <param name="Employee">The employee to add.</param>
        public void addEmployees(Employee Employee)
        {
            EmployeeList.Add(Employee);
        }

        public void EmployeeLogIn(Employee employee)
        {
            //employee.TempStartTime = DateTime.Now;
            Console.WriteLine("Employee logged in sucsessfully at " + DateTime.Now + ".");
        }

        public void EmployeeLogOut(Employee employee)
        {
        }

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
