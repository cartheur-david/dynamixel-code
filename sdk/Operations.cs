//
// This autonomous intelligent system software is the property of Cartheur Research B.V. Copyright 2021 - 2025, all rights reserved.
//
using Microsoft.Data.Sqlite;

namespace Cartheur.Animals.Robot
{
    public static class Operations
    {
        static string DataBaseTag { get; set; }
        /// <summary>
        /// Stores the name of the positions, the motor name, and the technical positions.
        /// </summary>
        /// <param name="positionType">Type of the position.</param>
        /// <param name="positionData">The position data.</param>
        /// <remarks>For a new set of goal positions.</remarks>
        public static async Task<bool> StorePosition(string positionType, Dictionary<string, int> positionData)
        {
            string directory = MapPath(DataBaseTag);
            SqliteConnection connection = new SqliteConnection(@"Data Source=" + directory);

            using (connection)
            {
                connection.Open();
                foreach (KeyValuePair<string, int> kvp in positionData)
                {
                    SqliteCommand command = new SqliteCommand
                    {
                        Connection = connection,
                        CommandText = "INSERT INTO StablePosition (PositionType, MotorName, PositionValue) VALUES ('" + positionType + "', '" + kvp.Key + "', '" + kvp.Value + "')"
                    };
                    command.ExecuteNonQuery();
                    Logging.WriteLog("Position " + kvp.Key + " written to database", Logging.LogType.Information, Logging.LogCaller.Memory);
                }
                await Task.CompletedTask;
                return true;
            }
        }
        /// <summary>
        /// Clears the table data.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <returns></returns>
        public static async Task<bool> ClearTable(string tableName)
        {
            string directory = MapPath(DataBaseTag);
            SqliteConnection connection = new SqliteConnection(@"Data Source=" + directory);
            using (connection)
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = "DELETE FROM " + tableName + "";
                command.ExecuteNonQuery();
                Logging.WriteLog("Table " + tableName + " truncated", Logging.LogType.Information, Logging.LogCaller.Memory);
                await Task.CompletedTask;
                return true;
            }
        }
        public static async Task<string> SaveDictionary(Dictionary<string, int> dictionary, Limbic.LimbicArea area)
        {
            string filePath = Environment.CurrentDirectory + @"\logs\" + area.ToString() + ".txt";
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                using (TextWriter tw = new StreamWriter(fs))

                    foreach (KeyValuePair<string, int> kvp in dictionary)
                    {
                        tw.WriteLine(string.Format("{0}--{1}", kvp.Key, kvp.Value));
                    }
            }
            await Task.CompletedTask;
            return "File saved.";
        }
        static string MapPath(string path)
        {
            string zebra = AppDomain.CurrentDomain.BaseDirectory.ToString();
            return Path.Combine(zebra, path);
        }
    }
}
