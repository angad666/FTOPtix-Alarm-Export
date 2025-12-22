using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApp1.Classes
{
    public static class CsvExporter
    {
        // Export alarms to CSV, overwriting existing file
        public static void ExportAlarmsToCsv(IEnumerable<AlarmEntry> alarms, string filePath)
        {
            if (alarms == null) throw new ArgumentNullException(nameof(alarms));
            if (string.IsNullOrEmpty(filePath)) throw new ArgumentNullException(nameof(filePath));

            var sb = new StringBuilder();
            // Header
            sb.AppendLine("Name,Type,Path,Message,LocalizedMessage,Enabled,AutoAcknowledge,AutoConfirm,InputValue,Severity,MaxTimeShelved,PresetTimeShelved,NormalStateValue");

            foreach (var a in alarms)
            {
                string[] fields = new string[] {
                    Escape(a?.Name),
                    Escape(a?.Type),
                    Escape(a?.Path),
                    Escape(a?.Message),
                    Escape(a?.LocalizedMessage),
                    a.Enabled.ToString(),
                    a.AutoAcknowledge.ToString(),
                    a.AutoConfirm.ToString(),
                    Escape(a?.InputValue),
                    a.Severity?.ToString() ?? string.Empty,
                    a.MaxTimeShelved?.ToString() ?? string.Empty,
                    a.PresetTimeShelved?.ToString() ?? string.Empty,
                    Escape(a?.NormalStateValue)
                };

                sb.AppendLine(string.Join(',', fields));
            }

            // Ensure target directory exists
            var dir = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir)) Directory.CreateDirectory(dir);
            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
        }

        private static string Escape(string? input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;
            // Double any quotes
            var s = input.Replace("\"", "\"\"");
            // Wrap in quotes if contains comma, quote, or newline
            if (s.Contains(',') || s.Contains('\"') || s.Contains('\n') || s.Contains('\r'))
                return "\"" + s + "\"";
            return s;
        }
    }
}
