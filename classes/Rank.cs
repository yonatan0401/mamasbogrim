using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace mamasbogrim.classes
{
    class Rank
    {
        public int rankID { get; set; }
        public string rankName { get; set; }
        public int rankPercentageBonus { get; set; }
        public Rank(int _rankID)
        {

            string query = $"select * from ranks where rankID = {_rankID}";
            Dictionary<string, List<Dictionary<string, string>>> result = DatabaseConnection.Query(query);
            DatabaseConnection.printQueryResults(result);
        }
    }
}
