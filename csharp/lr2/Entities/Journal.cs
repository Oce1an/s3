using System;
using System.Collections.Generic;

namespace GGGGGG_NNN_Lab2.Entities
{
    public class Journal
    {
        private List<string> _logs = new List<string>();

        public void LogEvent(object sender, PayrollEventArgs e)
        {
            string logEntry = $"[{DateTime.Now}] Event: {e.Message} | Entity: {e.EntityName}";
            _logs.Add(logEntry);
            Console.WriteLine(logEntry);
        }

        public void PrintLogs()
        {
            Console.WriteLine("--- Journal Logs ---");
            foreach (var log in _logs)
            {
                Console.WriteLine(log);
            }
            Console.WriteLine("--------------------");
        }
    }
}