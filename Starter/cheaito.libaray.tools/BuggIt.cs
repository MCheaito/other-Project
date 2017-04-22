using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Odbc;

namespace cheaito.libaray.tools
{
    public class BuggIt
    {

        private OdbcConnection connection;
        private string queryString;
        const string connectionString = "Driver=SQL Server;Server=ElfcStj-DevS02;Trusted_Connection=Yes;Database=BugTracker;";

        private void Connect()
        {
            connection = new OdbcConnection(connectionString);
            connection.Open();
        }
        private void Disconnect()
        {
            if (connection != null)
            {
                connection.Close();
            }
            connection = null;
        }
        public string GetNewAssignedBugs(string username)
        {
            queryString = " SELECT count(*) as nb " +
             " FROM bugs A Right join  users U on A.bg_assigned_to_user = U.us_id  INNER JOIN statuses S on A.bg_status = S.st_id" +
             " WHERE u.us_username='" + username + "' and S.ST_id=1 " +
             " GROUP BY st_name";

            Connect();
            OdbcCommand command = new OdbcCommand(queryString, connection);

            // Execute the DataReader and access the data.
            OdbcDataReader reader = command.ExecuteReader();
            int result = 0;
            if (reader.Read())
            {
                result = Convert.ToInt32(reader[0]);
            }
            return String.Format("New assigned bugs [{0}]", result);
        }
        public string GetAssignedBug(string username)
        {
            queryString = " SELECT count(*) as nb " +
                         " FROM bugs A Right join  users U on A.bg_assigned_to_user = U.us_id  INNER JOIN statuses S on A.bg_status = S.st_id" +
                         " WHERE u.us_username='" + username + "'" +
                         " GROUP BY st_name";
            
            Connect();
            OdbcCommand command = new OdbcCommand(queryString, connection);

            // Execute the DataReader and access the data.
            OdbcDataReader reader = command.ExecuteReader();
            int result = 0;
            if (reader.Read())
            {
                result = Convert.ToInt32(reader[0]);
            }
            return String.Format("Assigned bugs [{0}]", result);
        }
    }
}
