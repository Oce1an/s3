using System;

namespace GGGGGG_NNN_Lab2.Entities
{
    public class PayrollEventArgs : EventArgs
    {
        public string Message { get; set; }
        public string EntityName { get; set; }

        public PayrollEventArgs(string message, string entityName)
        {
            Message = message;
            EntityName = entityName;
        }
    }
}