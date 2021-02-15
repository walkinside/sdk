using Comos.Walkinside.Common.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using vrcontext.walkinside.sdk;

namespace WIExample
{
    class ProjectAccessHistory
    {
        private readonly IEntityManager m_EntityManager;
        // This is a prefix added onto the name of the class to reduce the probability of a name conflict with someone else's entities. 
        private const string classNamePrefix = "{6E7B22A0-A8FB-4B79-AC00-4559685A0556}";
        private const string className = classNamePrefix + "logHistoryRecord";

        public ProjectAccessHistory(IEntityManager entityManager)
        {
            m_EntityManager = entityManager;
        }

        public void AddRecord(ProjectAccessHistoryRecord record)
        {
            var attributes = new Dictionary<string, object>()
            {
                {"date", record.Date.ToString(CultureInfo.InvariantCulture) },
                {"userName", record.UserName },
                {"computerName", record.ComputerName },
            };
            var entity = m_EntityManager.Create(className, attributes);
            record.entityId = entity.Id;
        }

        public void DeleteAllRecords()
        {
            m_EntityManager.DeleteByClass(className);
        }

        public void DeleteRecords(IEnumerable<ProjectAccessHistoryRecord> records)
        {
            m_EntityManager.DeleteByIds(records.Select(record => record.entityId));
        }

        public IEnumerable<ProjectAccessHistoryRecord> GetRecords()
        {
            foreach (var logHistoryRecordEntity in m_EntityManager.GetByClass(className))
            {
                var attributes = logHistoryRecordEntity.GetAttributes().ToDictionary(key => key.Key, val => val.Value);
                var dateAsString = (string)attributes["date"];
                var date = DateTime.Parse(dateAsString, CultureInfo.InvariantCulture);
                var userName = attributes["userName"];
                var entityId = logHistoryRecordEntity.Id;
                var computerName = attributes["computerName"];
                yield return new ProjectAccessHistoryRecord
                {
                    Date = date,
                    UserName = userName.ToString(),
                    entityId = entityId,
                    ComputerName = computerName.ToString(),
                };
            }
        }
    }
}
