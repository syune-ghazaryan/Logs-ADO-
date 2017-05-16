using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logs
{
    /// <summary>
    /// Class defining log filter
    /// </summary>
    public class Filter
    {
        
        public int Id { get; set; }
        //public List<Log.SeverityType> Sverities;
        
        public Log.SeverityType Severity { get; set; }
        public string Message { get; set; }
       
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; } // 
    }
}
