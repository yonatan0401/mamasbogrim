using System;
using System.Collections.Generic;
using System.Configuration;
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
            string query = string.Format(ConfigurationManager.AppSettings.Get("getRankByID"), _rankID);
            Dictionary<string, List<Dictionary<string, string>>> result = DatabaseConnection.Query(query);

            //DatabaseConnection.printQueryResults(result);

            rankID = Int32.Parse(result["0"][0]["rankID"]);
            rankName = result["0"][1]["rankName"];
            rankPercentageBonus = Int32.Parse(result["0"][2]["rankPercentageBonus"]);
        }

        public override string ToString()
        {
            return $"Rank object. rankID: {rankID} rankName: {rankName} rankPercentageBonus: {rankPercentageBonus}";
        }
    }
}
