using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace Logs
{
    /// <summary>
    /// Class which contains function for logs processing
    /// </summary>
    public class Logger
    {/// <summary>
    /// Function that add log in DB
    /// </summary>
    /// <param name="log"></param>
        public void Write(Log log)
        {

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Logs.Properties.Settings.LogConnectionString"].ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = connection;
                string commandString = "Insert into Logg (severity, message, date_time) Values(@severity, @message, @date_time)";
                cmd.CommandText = commandString;
                cmd.Parameters.Add(new SqlParameter(@"severity", log.Severity));
                cmd.Parameters.Add(new SqlParameter(@"message", log.Message));
                cmd.Parameters.Add(new SqlParameter(@"date_time", log.Date));
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
               

            }
        }
        /// <summary>
        /// Read data from db using filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>Log</returns>
        public List<Log> Read(Filter filter)
        {
           
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Logs.Properties.Settings.LogConnectionString"].ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                string commandString = "Select * from Logg";
                SqlDataAdapter sda = new SqlDataAdapter(commandString, connection);
                DataSet dataSet = new DataSet();
                sda.Fill(dataSet, "Logg");
                DataTable table = dataSet.Tables["Logg"];

                string conditionMessage = "message like '%"+filter.Message+"%'";
                string conditionDate = "date_time > #"+filter.StartDate +"# AND date_time <  #"+filter.EndDate+"# ";
                string conditionSeverity= "severity ="+(byte)filter.Severity+"";
                try
                {
                    DataRow[] result = table.Select(conditionDate + " AND " + conditionMessage + " AND " + conditionSeverity);
                    return ConvertDataRow(result.ToList());

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return null;
                }



            }

            
        }
        /// <summary>
        /// Convert DataRow object to List of Logs
        /// </summary>
        /// <param name="rows"></param>
        /// <returns></returns>
        private List<Log> ConvertDataRow(List<DataRow> rows)
        {
            List<Log> convertedList = new List<Log>();
            foreach(var row in rows)
            {
                int sevKey = Convert.ToInt32(row["severity"]);
                int id = Convert.ToInt32(row["id"]);
                Log.SeverityType severity;
                severity = (Log.SeverityType)(Convert.ToInt32(row["severity"]));
                string message = Convert.ToString(row["message"]);
                DateTime dateTime = new DateTime();
                bool isParsed= DateTime.TryParse(row["date_time"].ToString(),out dateTime);
                Log log = new Log(message,severity,dateTime);
                log.Severity = severity;
                log.Id = id;
                convertedList.Add(log);
            }
            return convertedList;
        }
        
    }
}
