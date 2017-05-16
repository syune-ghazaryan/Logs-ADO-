using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logs
{
    /// <summary>
    /// Class that defines log
    /// </summary>
    public class Log
    {
        public int Id { get; set; }// log identification

        public enum SeverityType : byte//  enum type of severities
        {
            Message = 0,
            Info = 1,
            Error = 2,
            Warning = 3
        }
        public SeverityType Severity { get; set; }// log severity

        public string Message { get; set; }// log message

        public DateTime Date { get; set; }// date time of log 
        /// <summary>
        ///Log constructor
        /// </summary>
        /// <param name="mssage">log message</param>
        /// <param name="sev">log severity</param>
        /// <param name="dateTime">log date and time</param>
        public Log(string mssage, SeverityType sev, DateTime dateTime)
        {
            this.Message = mssage;
            this.Severity = sev;
            this.Date = dateTime;

        }
    }
}
