using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;

namespace mamasbogrim.classes
{
    public static class DatabaseConnection
    {
        public static SQLiteConnection myConnection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["dbAdress"].ConnectionString);
        /// <summary>
        /// opens a connection to the db.
        /// </summary>
        public static void openConnection()
        {
            try
            {
                if (myConnection.State != System.Data.ConnectionState.Open)
                {
                    myConnection.Open();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Faild to open connection to the database.");
                throw;
            }
        }
        /// <summary>
        /// closes the connection to the DB.
        /// </summary>
        public static void closeConnection()
        {
            try
            {
                if (myConnection.State != System.Data.ConnectionState.Closed)
                {
                    myConnection.Close();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Faild to close connection to the database.");
                throw;
            }
        }

        /// <summary>
        /// This method query's the db.
        /// Parses the data and returns a dict.
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
                List<Dictionary<string, string>> tempList = new List<Dictionary<string, string>>();
                for (int i = 0; i < keyNameList.Count; i++)
                {
                    Dictionary<string, string> tempDict = new Dictionary<string, string>();
                    tempDict.Add(keyNameList[i], $"{queryResult[i]}");
                    tempList.Add(tempDict);
                }
                results.Add($"{count}", tempList);
                count++;
            }
            closeConnection();
            return results;
        }

        /// <summary>
        /// use this to insert / update the db.
        /// </summary>
        /// <param name="query">query string</param>
        /// <returns>the number of effected rows</returns>
        public static int insert(string query)
        {
            SQLiteCommand sql = new SQLiteCommand(query, myConnection);
            openConnection();
            int result = sql.ExecuteNonQuery();
            closeConnection();
            return result;
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
