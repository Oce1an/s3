using System;

namespace Entities
{
    public class PayrollList : EventArgs
    {
        public string Action { get; set; }
        public string EntityType { get; set; }
        public string EntityName { get; set; }

        public PayrollList(string action, string entityType, string entityName)
        {
            Action = action;
            EntityType = entityType;
            EntityName = entityName;
        }

        public override string ToString() =>
            $"[{EntityType}] {Action}: {EntityName}";
    }
}