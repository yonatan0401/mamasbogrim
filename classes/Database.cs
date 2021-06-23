using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.IO
    ;
namespace mamasbogrim.classes
{
    class Database
    {
        public SQLiteConnection myConnection;
        public Database()
        {
            myConnection = new SQLiteConnection("Data Source=\\mamasbogrim\\Maternityward.db");

            if (File.Exists("\\mamasbogrim\\Maternityward.db"))
            {
                Console.WriteLine("db alredy exists");
            }
            else
            {
                //SQLiteConnection.CreateFile("Maternityward.sqlite3");
                Console.WriteLine("db created");
            }
        }

        public void openConnection()
        {
            if(myConnection.State != System.Data.ConnectionState.Open)
            {
                myConnection.Open();
            }
        }

        public void closeConnection()
        {
            if(myConnection.State != System.Data.ConnectionState.Closed)
            {
                myConnection.Close();
            }
        }

    }
}
