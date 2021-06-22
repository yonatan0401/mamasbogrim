using System;
using System.Collections.Generic;
using System.Text;

namespace mamasbogrim
{
    class Maternityward
    {
        public string MaternityName { get; set; }
        public List<Employee> EmployeeList { get; }
        public Maternityward(string Name)
        {
            MaternityName = Name;
            EmployeeList = new List<Employee>();
        }
        /// <summary>
        /// This Method adds employess to the list from a given employee list.
        /// </summary>
        /// <param name="Employees">A list of employees to add.</param>
        public void addEmployees(List<Employee> Employees)
        {
            for (int i = 0; i < Employees.Count; i++)
            {
                EmployeeList.Add(Employees[i]);
            }
        }
        /// <summary>
        /// This Method add's a single employee to the employee list.
        /// </summary>
        /// <param name="Employee">The employee to add.</param>
        public void addEmployees(Employee Employee)
        {
            EmployeeList.Add(Employee);
        }
    }
}
