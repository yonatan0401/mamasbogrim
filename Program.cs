using mamasbogrim.classes;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Data.SQLite;


namespace mamasbogrim
{
    class Program
    {
        static void Main(string[] args)
        {
            //Maternityward Lis = new Maternityward("LIS");

            Rank rank = new Rank(1);
            //Employee yonatan = new Employee("yonatan", "programer", 0.0, 0);

            /*DatabaseConnection dbObj = new DatabaseConnection();
            dbObj.openConnection();
            string query = "select role_name, rank_name, rank_bonus_percenrage from roles join ranks on roles.role_rank == ranks.ID ORDER BY(roles.roleID)";

            SQLiteCommand sql = new SQLiteCommand(query, dbObj.myConnection);
            SQLiteDataReader result = sql.ExecuteReader();
            while (result.Read())
            {
                Console.WriteLine($"role_name: {result["role_name"]}, rank_name: {result["rank_name"]}, rank_bonus_percaentage: {result["rank_bonus_percenrage"]}");
            }

            dbObj.closeConnection();
            */

            /*var value = ConfigurationManager.AppSettings["commonEmployee"];
            Console.WriteLine(value);

            string sAttr;

            // Read a particular key from the config file 
            sAttr = ConfigurationManager.AppSettings.Get("Key0");
            Console.WriteLine("The value of Key0: " + sAttr);

            // Read all the keys from the config file
            NameValueCollection sAll;
            sAll = ConfigurationManager.AppSettings;

            foreach (string s in sAll.AllKeys)
                Console.WriteLine("Key: " + s + " Value: " + sAll.Get(s));
            Console.ReadLine();*/

            /*Lis.addEmployees(yonatan);
            Lis.EmployeeLogIn(yonatan);
            Lis.EmployeeLogOut(yonatan);
            Console.WriteLine(yonatan.getCurrentIncome());
            yonatan.addWorkingHours(DateTime.Parse("7:00 AM"), DateTime.Parse("2:00 PM"));
            Console.WriteLine(yonatan);*/
        }
    }
}
