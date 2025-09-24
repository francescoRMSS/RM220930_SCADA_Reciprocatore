using RMLib.Logger;
using System;
using System.Data;
using System.Data.SQLite;

namespace RM.src.RM220930
{
    /// <summary>
    /// DAO contenente procedure database
    /// </summary>
    public class DAOSqlite
    {
        #region Parametri di RobotDAOSqlite

        /// <summary>
        /// Logger
        /// </summary>
        private static readonly log4net.ILog log = LogHelper.GetLogger();

        #region Variabili tabella DB alarm_history

        public const String ALARM_HISTORY_TABLE_NAME = "alarm_history";
        public const String ALARM_HISTORY_ID_COLUMN_NAME = "id";
        public const String ALARM_HISTORY_DESCRIPTION_COLUMN_NAME = "description";
        public const String ALARM_HISTORY_TIMESTAMP_COLUMN_NAME = "timestamp";
        public const String ALARM_HISTORY_DEVICE_COLUMN_NAME = "device";
        public const String ALARM_HISTORY_STATE_COLUMN_NAME = "state";

        #endregion

        #endregion

        #region Metodi di RobotDAOSqlite

        /// <summary>
        /// Salva sul db l'allarme
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="id"></param>
        /// <param name="description"></param>
        /// <param name="timpestamp"></param>
        /// <param name="device"></param>
        /// <param name="state"></param>
        public void SaveAlarm(String connectionString, int id, string description, string timpestamp, 
            string device, string state)
        {
            try
            {
                using (var con = new SQLiteConnection(connectionString))
                {
                    con.Open();

                    // Aggiungo il nuovo utente
                    string insertQuery = "INSERT INTO " + ALARM_HISTORY_TABLE_NAME + "" +
                        " (id, description, timestamp, device, state) " +
                        "VALUES (@id, @Description, @Timestamp, @Device, @State)";

                    using (var cmd = new SQLiteCommand(insertQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@Description", description);
                        cmd.Parameters.AddWithValue("@Timestamp", timpestamp);
                        cmd.Parameters.AddWithValue("@Device", device);
                        cmd.Parameters.AddWithValue("@State", state);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Si è verificata un'eccezione SQL durante l'aggiunta dell'azione: " + ex.ToString());
            }
        }

        #endregion
    }
}
