using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIExample
{
    class ProjectAccessHistoryRecord
    {
        public ProjectAccessHistoryRecord(DateTime date, string userName, long entityId)
        {
            this.Date = date;
            this.UserName = userName;
            this.entityId = entityId;
        }
        public ProjectAccessHistoryRecord()
        {

        }
        public DateTime Date { get; set; }
        public string UserName { get; set; }
        public long entityId { get; set; }
        public string ComputerName { get; set; }

        public override string ToString()
        {
            return Date.ToString() + ": " + UserName + " - " + ComputerName;
        }
    }
}
