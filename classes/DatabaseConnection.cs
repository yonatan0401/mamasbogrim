using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO
    ;
namespace mamasbogrim.classes
{
    public static class DatabaseConnection
    {
        public static SQLiteConnection myConnection = new SQLiteConnection("Data Source=C:\\Users\\yonat\\source\\repos\\mamasbogrim\\Maternityward.db");
        public static void openConnection()
        {
            if(myConnection.State != System.Data.ConnectionState.Open)
            {
                myConnection.Open();
            }
        }
        public static void closeConnection()
        {
            if(myConnection.State != System.Data.ConnectionState.Closed)
            {
                myConnection.Close();
            }
        }

        /// <summary>
        /// This method query's the db.
        /// </summary>
        /// <param name="queryString">The SQL statement to execute.</param>
        /// <returns>The query result as a formated dictionery.</returns>
        public static Dictionary<string, List<Dictionary<string, string>>> Query(string queryString)
        {
            Dictionary<string, List<Dictionary<string, string>>> results = new Dictionary<string, List<Dictionary<string, string>>>();
            List<string> keyNameList = new List<string>();
            int count = 0;
            openConnection();
            SQLiteCommand sql = new SQLiteCommand(queryString, myConnection);
            SQLiteDataReader queryResult = sql.ExecuteReader();
            for (int i = 0; i < queryResult.FieldCount; i++)
            {
                keyNameList.Add(queryResult.GetName(i));
            }
            while (queryResult.Read())
            {
                //results.Add($"{count}", "");
                List<Dictionary<string, string>> tempList = new List<Dictionary<string, string>>();
                for (int i = 0; i < keyNameList.Count; i++)
                {
                    Dictionary<string, string> tempDict = new Dictionary<string, string>();
                    tempDict.Add(keyNameList[i], $"{queryResult[i]}");
                    tempList.Add(tempDict);
                }
                results.Add($"{count}", tempList)
            }
            closeConnection();
            return results;
        }

        /// <summary>
        /// prints the given query result dict to the screen.
        /// </summary>
        /// <param name="queryResult">a list with the query results</param>
        public static void printQueryResults(Dictionary<string, List<Dictionary<string, string>>> queryResult)
        {
            foreach (KeyValuePair<string, List<Dictionary<string, string>>> kvp in queryResult)
            {
                Console.WriteLine(string.Format("Key = {0}", kvp.Key));
                for (int i = 0; i < kvp.Value.Count; i++)
                {
                    foreach (KeyValuePair<string, string> kvp2 in kvp.Value[i])
                    {
                        Console.WriteLine(string.Format("Key = {0}, Value = {1}", kvp2.Key, kvp2.Value));
                    }
                }
            }
        }

    }
}
