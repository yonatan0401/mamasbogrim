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

        public static double managerHours = 200.0;
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
            EmployeehourlyRate = MinimumWage;
        }
        public override string ToString()
        {
            return $"{employeeName} is an Employee.\nwith the profession of \"{employeeRole.roleName}\".\nhis current monthly wage is: getCurrentIncome(change this to tamplate string when  the function is done) shekels.";
        }

        public double getCurrentMonthSalery()
        {
            string workingHoursQuery = string.Format(ConfigurationManager.AppSettings.Get("getCurrentMonthWorkingHours"), employeeID);
            Dictionary<string, List<Dictionary<string, string>>> result = DatabaseConnection.Query(workingHoursQuery);

            // DatabaseConnection.printQueryResults(result);
            if (employeeRole.isRankInRole(5))
            {
                //if role is manager use 200 hours.
                return getMoney(managerHours);
            }
            else if (employeeRole.isRankInRole(4))
            {
                //if role is decition maker use 200 hours if hours is > 50.
                double totalHours = double.Parse(result["0"][0]["totalShiftLengthInHours"]);
                if (totalHours > 50)
                {
                    return getMoney(managerHours);
                }
                else
                {
                    double totalBonus = getTotalBonusPercentage() - 50; // remove the bonus.
                    return (totalHours * EmployeehourlyRate) * ((totalBonus + 100) / 100);
                }
            }
            return getMoney(double.Parse(result["0"][0]["totalShiftLengthInHours"]));
        }

        private double getMoney(double hours)
        {
            double basicAmont = hours * EmployeehourlyRate;
            return basicAmont * ((getTotalBonusPercentage() / 100) + 1);
        }

        public double getTotalBonusPercentage()
        {
            double totalPercentage = 0;

            for (int i = 0; i < employeeRole.rankList.Count; i++)
            {
                totalPercentage += (double) employeeRole.rankList[i].rankPercentageBonus;  
            }
            return totalPercentage;
        }
    }
}
