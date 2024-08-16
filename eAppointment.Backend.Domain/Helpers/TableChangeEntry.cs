﻿using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Enums;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;

namespace eAppointment.Backend.Domain.Helpers
{
    public class TableChangeEntry
    {
        public TableChangeEntry(EntityEntry entry)
        {
            Entry = entry;
        }

        public EntityEntry Entry { get; }

        public string TableName { get; set; }

        public Dictionary<string, object> KeyValues { get; } = new();

        public Dictionary<string, object> OldValues { get; } = new();

        public Dictionary<string, object> NewValues { get; } = new();

        public TableChangeType TableChangeType { get; set; }

        public List<string> AffectedColumns { get; set; } 

        public TableLog ToTableLog(int auditLogId)
        {
            var tableLog = new TableLog
            {
                TableChangeType = TableChangeType.ToString(),
                TableName = TableName,
                OldValues = OldValues.Count == 0 ? null : JsonConvert.SerializeObject(OldValues),
                NewValues = NewValues.Count == 0 ? null : JsonConvert.SerializeObject(NewValues),
                KeyValues = JsonConvert.SerializeObject(KeyValues),
                AffectedColumns = AffectedColumns.Count == 0 ? null : JsonConvert.SerializeObject(AffectedColumns),
                AuditLogId = auditLogId
            };

            return tableLog;
        }
    }
}
