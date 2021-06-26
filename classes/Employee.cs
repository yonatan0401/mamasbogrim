using mamasbogrim.classes;
using System;
using System.Collections.Generic;
using System.Configuration;

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
        public bool isInShift;
        public Employee(int _employeeID)
        {
            EmployeehourlyRate = MinimumWage;
            string employeeQuery = string.Format(ConfigurationManager.AppSettings.Get("getEmployeeByID"), _employeeID);
            string isInShiftQuery = string.Format(ConfigurationManager.AppSettings.Get("isInShift"), _employeeID);

            Dictionary<string, List<Dictionary<string, string>>> result = DatabaseConnection.Query(employeeQuery);
            Dictionary<string, List<Dictionary<string, string>>> isInShiftResult = DatabaseConnection.Query(isInShiftQuery);

            //DatabaseConnection.printQueryResults(isInShiftResult);
            isInShift = int.Parse(isInShiftResult["0"][0]["result"]) == 1 ? true : false;
            employeeID = Int32.Parse(result["0"][0]["employeeID"]);
            employeeName = result["0"][1]["employeeName"];
            employeeRole = new Role(Int32.Parse(result["0"][2]["roleID"]));
            EmployeehourlyRate = MinimumWage;
        }
        public override string ToString()
        {
            return $"\n{employeeName} is an Employee.\nwith the profession of \"{employeeRole.roleName}\".\nhis current monthly wage is: {getCurrentMonthSalery()}. \n";
        }

        /// <summary>
        /// insert a full shift with given start and end time
        /// </summary>
        /// <param name="startTime">start time as a string</param>
        /// <param name="endTime">end time as a string</param>
        /// <returns>true if inserted secsessfully false if not</returns>
        public bool insertFullShift(string startTime, string endTime)
        {
            string query = string.Format(ConfigurationManager.AppSettings.Get("newShift"), employeeID, startTime, endTime);
            int quertResult = DatabaseConnection.insert(query);
            if (quertResult > 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Starts the employees shift - inserting a new shift to enteries table.
        /// </summary>
        /// <returns>True if insert succsessfully false if not</returns>
        public bool startShift()
        {
            try
            {
                if (!isInShift)
                {
                    string query = string.Format(ConfigurationManager.AppSettings.Get("startShift"), employeeID);
                    int quertResult = DatabaseConnection.insert(query);
                    if (quertResult > 0)
                    {
                    isInShift = true;
                    return true;
                    }
                    return false;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex); 
                return false;
            }
        }
        /// <summary>
        /// Inserts into the db the current time to finish the employee's shift.
        /// </summary>
        /// <returns>True if update was done false if faild</returns>
        public bool finishShift()
        {
            try
            {
                if (isInShift)
                {
                    string query = string.Format(ConfigurationManager.AppSettings.Get("finishShift"), employeeID, DateTime.Now.ToString());
                    int quertResult = DatabaseConnection.insert(query);
                    if (quertResult > 0)
                    {
                        isInShift = false;
                        return true;
                    }
                    return false;
                }
                else
                {
                    Console.WriteLine("Cannot Finish Shift, employee not in shift");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
        /// <summary>
        /// calculates current month salery with all the needed bonus.
        /// </summary>
        /// <returns>This monthe salery.</returns>
        public double getCurrentMonthSalery()
        {
            if (employeeRole.isRankInRole(5))
            {
                //if role is manager use 200 hours.
                return getMoney(managerHours);
            }
            else if (employeeRole.isRankInRole(4))
            {
                //if role is decition maker use 200 hours if hours is > 50.
                double totalHours = getTotalWorkingHours();
                if (totalHours > 50)
                {
                    return getMoney(managerHours);
                }
                else
                {
                    double totalBonus = getTotalBonusPercentage() - 50; // remove the desition maker bonus.
                    return Math.Round((totalHours * EmployeehourlyRate) * ((totalBonus / 100) + 1), 3);
                }
            }
            return getMoney(getTotalWorkingHours());
        }
        private double getMoney(double hours)
        {
            double basicAmont = hours * EmployeehourlyRate;
            return Math.Round(basicAmont * ((getTotalBonusPercentage() / 100) + 1), 3);
        }

        /// <summary>
        /// calculates the total bonus percentage by the ranks.
        /// </summary>
        /// <returns>the total bonus percentage</returns>
        public double getTotalBonusPercentage()
        {
            double totalPercentage = 0;

            for (int i = 0; i < employeeRole.rankList.Count; i++)
            {
                totalPercentage += (double) employeeRole.rankList[i].rankPercentageBonus;  
            }
            return totalPercentage;
        }
        /// <summary>
        /// gets the total working hours for an employee
        /// </summary>
        /// <returns></returns>
        public double getTotalWorkingHours()
        {
            string workingHoursQuery = string.Format(ConfigurationManager.AppSettings.Get("getCurrentMonthWorkingHours"), employeeID);
            Dictionary<string, List<Dictionary<string, string>>> result = DatabaseConnection.Query(workingHoursQuery);
            
            double TotalHours = double.Parse(result["0"][0]["totalShiftLengthInHours"]);
            
            if (isInShift)
            {
                try
                {
                string startTimeQuery = string.Format(ConfigurationManager.AppSettings.Get("getStartTime"), employeeID);
                Dictionary<string, List<Dictionary<string, string>>> startTimeQueryResult = DatabaseConnection.Query(startTimeQuery);
                DateTime startTime = DateTime.Parse(startTimeQueryResult["0"][0]["startTime"]);
                DateTime endTime = DateTime.Now;
                TimeSpan ts = endTime - startTime;
                TotalHours += ts.TotalHours;
                }
                catch (Exception)
                {
                    return TotalHours;
                }
            }
            return TotalHours;
        }
    }
}
