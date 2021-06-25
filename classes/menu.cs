using System;
using System.Collections.Generic;
using System.Text;

namespace mamasbogrim.classes
{
    class menu
    {
        public static string smallMergin = "\t";
        public static string bigMergin = "\t\t";
        static int tableWidth = 100;
        public static void manageMaternityward()
        {
            Maternityward Echilov = new Maternityward("Echilov");
            Echilov.loadEmployees();
            int exit = 1;
            while(exit == 1)
            {
                welcomMenu(Echilov);
                string userInput = Console.ReadLine();
                if (isValidInput(userInput, new List<int> { 0, 1, 2}))
                {
                    int navigate = int.Parse(userInput);
                    switch (navigate)
                    {
                        case 0:
                            welcomMenu(Echilov);
                            break;
                        case 1:
                            maternitywardStats(Echilov);
                            break;
                        case 2:
                            employeeMenu(Echilov);
                            break;
                        default:
                            break;
                    }
                }
            }

        }
        public static void welcomMenu(Maternityward maternityward)
        {
            Console.Clear();
            Console.WriteLine($"Welcom to {maternityward.MaternityName}'s Maternityward !  ");
            Console.WriteLine("Plese enter one of the following to navigate throw the system:");
            Console.WriteLine(smallMergin + "1. Total maternityward statistics. ");
            Console.WriteLine(smallMergin + "2. Employee Menu. ");
        }


        static void maternitywardStats(Maternityward maternityward)
        {
            //Console.Clear();
            PrintLine();
            PrintRow( "employee name", "role", "Working Hours", "Monthly salery");
            for (int i = 0; i < maternityward.EmployeeList.Count; i++)
            {
                string employeeTempID = maternityward.EmployeeList[i].employeeID.ToString();
                string employeeTempName = maternityward.EmployeeList[i].employeeName;
                string employeeTempRoleName = maternityward.EmployeeList[i].employeeRole.roleName;
                string employeeTempWorkingHours = maternityward.EmployeeList[i].getTotalWorkingHours().ToString();
                string employeeTempMonthlySalery = maternityward.EmployeeList[i].getCurrentMonthSalery().ToString();
                PrintLine();
                PrintRow("", "", "", "");
                PrintRow(employeeTempName, employeeTempRoleName, employeeTempWorkingHours, employeeTempMonthlySalery);
                PrintRow("", "", "", "");
                PrintLine();
            }
            Console.ReadLine();
        }
        static void PrintLine()
        {
            Console.WriteLine(new string('-', tableWidth));
        }
        static void PrintRow(params string[] columns)
        {
            int width = (tableWidth - columns.Length) / columns.Length;
            string row = "|";

            foreach (string column in columns)
            {
                row += AlignCentre(column, width) + "|";
            }

            Console.WriteLine(row);
        }
        static string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }

        public static void employeeMenu(Maternityward maternityward)
        {
            int userInput = 1;
            Console.Clear();
            while (userInput != 0)
            {
                Console.WriteLine("Welcom to the employee menu.");
                Console.WriteLine("Plese enter one of the following to navigate the menu:");
                Console.WriteLine(smallMergin + "1. Swipe an employee's card to log in and out of shift.");
                Console.WriteLine(smallMergin + "2. Get an employees current income.");
                Console.WriteLine(smallMergin + "3. Save new shift with desierd start and end time.");
                string newInput = Console.ReadLine();
                if (isValidInput(newInput, new List<int> { 0, 1, 2, 3 }))
                {
                    userInput = int.Parse(newInput);
                    switch (userInput)
                    {
                        case 1:
                            employeeCardSwipe(maternityward);
                            Console.Clear();
                            break;
                        case 2:
                            getEmployeeSalery(maternityward );
                            Console.Clear();
                            break;
                        case 3:
                            insertFullEmployeeShift(maternityward);
                            Console.Clear();
                            break;
                        default:
                            Console.WriteLine("something weird has ouccerd");
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// menu to insert a full shift into the database.
        /// </summary>
        /// <param name="maternityward">the maternityward to use</param>
        public static void insertFullEmployeeShift(Maternityward maternityward)
        {
            Console.Clear();
            Console.WriteLine("Please enter one of the following employee ids: (press 0 to exit). ");

            for (int i = 0; i < maternityward.EmployeeList.Count; i++)
            {
                Console.WriteLine(smallMergin + (i + 1) + $" - for {maternityward.EmployeeList[i].employeeName}'s current monthly income.");
            }
            //isValidDateInput();
            int userInput = 1;
            while (userInput != 0)
            {
                string newInput = Console.ReadLine();
                if (isValidInput(newInput, getListFromLength(maternityward.EmployeeList.Count)))
                {
                    userInput = int.Parse(newInput);
                    if (userInput != 0)
                    {
                        Console.WriteLine("Plese enter the shift's full start time in this syntax: 2021-06-25 10:52:10 !");
                        string startTime = Console.ReadLine();
                        if (isValidDateInput(startTime))
                        {
                            Console.WriteLine("Plese enter the shift's full end time in this syntax: 2021-06-25 10:52:10 !");
                            string endTime = Console.ReadLine();
                            if (isValidDateInput(endTime))
                            {
                                if (isStartMoreThenEnd(startTime, endTime))
                                {
                                    if (maternityward.EmployeeList[userInput - 1].insertFullShift(startTime, endTime))
                                    {
                                        Console.WriteLine("Shift saved secsessfully.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Failed to save shift.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Start time must be grater then end time.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Please enter a valid date format (2021-06-25 10:52:10)");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Please enter a valid date format (2021-06-25 10:52:10)");
                        }
                    }
                }
                Console.WriteLine("Enter another id or 0 to exit:");
            }
        }
        /// <summary>
        /// starts the menu to log in and out of shifts for an employee.
        /// it will start a new shift if the employee is not in a shift
        /// and it will end a current shift if an employee is in a shift.
        /// </summary>
        /// <param name="maternityward">The maternityward to act upon.</param>
        public static void employeeCardSwipe(Maternityward maternityward)
        {
            Console.Clear();
            Console.WriteLine("Please enter one of the following employee ids: (press 0 to exit)");
            for (int i = 0; i < maternityward.EmployeeList.Count; i++)
            {
                Console.WriteLine(smallMergin + (i + 1) + $" - for {maternityward.EmployeeList[i].employeeName}'s current monthly income.");
            }
            int userInput = 1;
            while (userInput != 0)
            {
                string newInput = Console.ReadLine();
                if (isValidInput(newInput, getListFromLength(maternityward.EmployeeList.Count)))
                {
                    userInput = int.Parse(newInput);
                    if (userInput != 0)
                    {
                        if (maternityward.EmployeeList[userInput - 1].isInShift == false)
                        {
                            if (maternityward.EmployeeList[userInput - 1].startShift())
                            {
                                Console.WriteLine($"Employee {maternityward.EmployeeList[userInput - 1].employeeName} has started shift sucsessfully.");
                            }
                            else
                            {
                                Console.WriteLine($"{maternityward.EmployeeList[userInput - 1].employeeName} cannot get into the shift, he is alredy in shift.");
                            }
                        }
                        else
                        {
                            if (maternityward.EmployeeList[userInput - 1].finishShift())
                            {
                                Console.WriteLine($"Employee {maternityward.EmployeeList[userInput - 1].employeeName} has finished shift sucsessfully.");
                            }
                            else
                            {
                                Console.WriteLine($"{maternityward.EmployeeList[userInput - 1].employeeName} cannot finish the shift, he is not shift.");
                            }
                        }
                    }
                }
                Console.WriteLine("Enter another id or 0 to exit:");
            }
        }
        public static void getEmployeeSalery(Maternityward maternityward)
        {
            Console.WriteLine("Please enter one of the following employee ids: (press 0 to exit)");
            for (int i = 0; i < maternityward.EmployeeList.Count; i++)
            {
                Console.WriteLine(smallMergin + (i + 1) + $" - for {maternityward.EmployeeList[i].employeeName}'s current monthly income.");
            }
            int userInput = 1;
            while (userInput != 0)
            {
                string newInput = Console.ReadLine();
                if (isValidInput(newInput, getListFromLength(maternityward.EmployeeList.Count)))
                {
                    userInput = int.Parse(newInput);
                    if (userInput != 0)
                    {
                        Console.WriteLine(maternityward.EmployeeList[userInput - 1]);
                    }
                }
                Console.WriteLine("Enter another id or 0 to exit:");
            } 
        }
        public static List<int> getListFromLength(int length)
        {
            List<int> returnList = new List<int>();
            for (int i = 0; i <= length; i++)
            {
                returnList.Add(i);
            }
            return returnList;
        }
        public static bool isValidInput(string input, List<int> allowedNumbers)
        {
            try
            {
                int userInput = int.Parse(input);
                if (allowedNumbers.Contains(userInput))
                {
                    return true;
                }else if (userInput == 0)
                {
                    return true;
                }
                throw new InvalidOperationException();
            }
            catch (Exception)
            {
                Console.WriteLine("Please enter a valid input.");
                string toPrint = "";
                for (int i = 0; i < allowedNumbers.Count; i++)
                {
                    toPrint += allowedNumbers[i].ToString() + " ";
                }
                Console.WriteLine("A valid input is one of the following: " + toPrint);
                return false;
            }
        }
        /// <summary>
        /// checks if a given date string is a valid date.
        /// </summary>
        /// <param name="date">the dates string</param>
        /// <returns></returns>
        public static bool isValidDateInput(string date)
        {
            try
            {
                DateTime dt = DateTime.Parse(date);
                if (date.Length != 19)
                {
                    throw new InvalidOperationException();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
           
        }
        /// <summary>
        /// checks if given start time is grater then given end time.
        /// </summary>
        /// <param name="_startTime">the start time as a string</param>
        /// <param name="_endTime">the end time as a string</param>
        /// <returns>true if startTime > endTime</returns>
        public static bool isStartMoreThenEnd(string _startTime, string _endTime)
        {
            DateTime startTime = DateTime.Parse(_startTime);
            DateTime endTime = DateTime.Parse(_endTime);

            if (DateTime.Compare(startTime, endTime) < 0)
            {
                Console.WriteLine(startTime + " " + endTime);
                return true;
            }
            return false;
        }
    }
}
