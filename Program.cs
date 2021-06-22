using System;

namespace mamasbogrim
{
    class Program
    {
        static void Main(string[] args)
        {
            Employee yonatan = new Employee("yonatan", "programer", 0.0, 0);
            Maternityward Lis = new Maternityward("LIS");

            Lis.addEmployees(yonatan);
            Lis.EmployeeLogIn(yonatan);
            Lis.EmployeeLogOut(yonatan);
            Console.WriteLine(yonatan.getCurrentIncome());
            yonatan.addWorkingHours(DateTime.Parse("7:00 AM"), DateTime.Parse("2:00 PM"));
            Console.WriteLine(yonatan);
        }
    }
}
