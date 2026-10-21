using System;
using System.Collections.Generic;

namespace Entities
{
    public class Journal
    {
        private List<string> entries;

        public Journal()
        {
            entries = new List<string>();
        }

        public void LogEvent(object? sender, PayrollList e)
        {
            string entry = $"[{DateTime.Now:HH:mm:ss}] {e}";
            entries.Add(entry);
        }

        public void PrintLog()
        {
            Console.WriteLine("--- Журнал событий ---");
            if (entries.Count == 0)
            {
                Console.WriteLine("  Журнал пуст.");
                return;
            }
            foreach (string entry in entries)
            {
                Console.WriteLine($"  {entry}");
            }
            Console.WriteLine($"  Всего записей в журнале: {entries.Count}");
        }
    }
}
