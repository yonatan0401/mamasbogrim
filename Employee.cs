using System;
using System.Collections.Generic;
using System.Text;

namespace mamasbogrim
{
    class Employee
    {
        public static double MinimumWage = 29.19;
        public string EmployeeName { get; set; }
        // public bool isManagement { get; set; } // is the employee in management or professional (cleaner, cooker / nurse, doctor  . . . )
        public string professionName { get; set; } 
        public double RiskPercentage { get; set; }
        public double[] MonthlyWorkingHours { get; }
        public DateTime TempStartTime { get; set; }
        public int Id { get; set; }

        public double EmployeehourlyRate { get; set; }

        public Employee(string _EmployeeName, string _professionName, double _RiskPercentage, int _Id)
        {
            EmployeeName = _EmployeeName;
            professionName = _professionName;
            RiskPercentage = _RiskPercentage;
            MonthlyWorkingHours = new double[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
            Id = _Id;
            EmployeehourlyRate = Employee.MinimumWage;
        }

        /// <summary>
        /// This method adds time to the total working hour of the employee for this month.
        /// </summary>
        /// <param name="_startTime">Date time object that represents the start time</param>
        /// <param name="_endTime">Date time object that represents the end time</param>
        public void addWorkingHours(DateTime _startTime, DateTime _endTime)
        {
            DateTime startTime = _startTime;
            DateTime endTime = _endTime;

            Console.WriteLine("Adding working hours from " + startTime + " to " +  endTime + ".");

            TimeSpan duration = endTime.Subtract(startTime);
            
            MonthlyWorkingHours[endTime.Month] += duration.Hours;

        }
      
        /// <summary>
        /// calculate the current monthes salery.
        /// </summary>
        /// <returns>The current months salery.</returns>
        public double getCurrentIncome()
        {
            DateTime dt = DateTime.Now;
            return MonthlyWorkingHours[dt.Month] * EmployeehourlyRate;
        }
        public override string ToString()
        {
            return $"{EmployeeName} is a common Employee.\nwith the profession of \"{professionName}\".\nhis current monthly wage is: {getCurrentIncome()} shekels.";
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
