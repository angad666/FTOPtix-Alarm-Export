using ConsoleApp1.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ConsoleApp1
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Debug: verify Main is executed
            MessageBox.Show("Starting Main");

            var defaultFilePath = "C:\\Users\\angad\\OneDrive\\Documents\\FTOPtix Alarm Export\\16797_Equipment List V1.xlsx";

            using var form = new MainForm(defaultFilePath);
            if (form.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            var FilePath = form.SelectedFilePath;
            var InputFormat = form.SelectedFormatKey;

            if (!File.Exists(FilePath))
            {
                MessageBox.Show($"File not found: {FilePath}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var Reader = new ExcelReader();
            var dataSet = Reader.ReadExcelFile(FilePath);
            var Cons = new Constants();
            List<TagEntry> tags = new List<TagEntry>();
            List<AlarmEntry> alarms = new List<AlarmEntry>();
            foreach (DataRow row in dataSet.Tables[3].Rows)
            {
                if (!string.IsNullOrEmpty(row[9].ToString()))
                {
                    tags.Add(new TagEntry
                    {
                        Name = row[0].ToString() ?? string.Empty,
                        Description = row[7].ToString() ?? string.Empty,
                        PLCStyle = row[9].ToString() ?? string.Empty,
                    });
                }
            }

            foreach (TagEntry u in tags)
            {
                switch (u.PLCStyle.ToLower())
                {
                    case "miscinput":
                        u.Messages = Cons.MiscInputAlarms;
                        u.LocalizedMessages = Cons.MiscInputAlarms; // Placeholder for localized messages
                        break;
                    case "miscoutput":
                        u.Messages = Cons.MiscOutputCylinderAlarms;
                        //u.LocalizedMessages = Cons.MiscOutputCylinderAlarms; // Placeholder for localized messages
                        break;
                    case "motor":
                        u.Messages = Cons.MotorAlarms;
                        // u.LocalizedMessages = Cons.MotorAlarms; // Placeholder for localized messages
                        break;
                    default:
                        u.Messages = Array.Empty<string>();
                        // u.LocalizedMessages = Array.Empty<string>();
                        break;
                }
                var i = 1;
                var j = 1;
                foreach (string msg in u.Messages)
                {
                    var name = "";
                    var inputvalue = "";
                    if (msg.Contains("Manual") || msg.Contains("Mute"))
                    {
                        name = u.Name + "_MAN" + j.ToString();
                        j++;
                    }
                    else
                    {
                        name = u.Name + "_ALM" + i.ToString();
                        i++;
                    }
                    if (InputFormat == "1")
                    {
                        inputvalue = "INSERT LOGIC_FOR_ROCKWELL_HERE";
                    }
                    else
                    {
                        inputvalue = "INSERT LOGIC_FOR_OMRON_HERE";
                    }

                    alarms.Add(new AlarmEntry
                    {
                        Name = name,
                        Type = "OffNormalAlarmController",
                        Path = "Alarms", // Placeholder for path
                        Message = msg,
                        LocalizedMessage = msg, // Placeholder for localized message
                        Enabled = "TRUE",
                        AutoAcknowledge = "FALSE",
                        AutoConfirm = "TRUE",
                        InputValue = inputvalue,
                        Severity = "1",
                        MaxTimeShelved = "0.00:00:00.000",
                        PresetTimeShelved = "0.00:00:00.000",
                        NormalStateValue = "0"
                    });
                }
            }

            // Export alarms to CSV
            var outputPath = Path.Combine(Directory.GetCurrentDirectory(), "alarms.csv");
            CsvExporter.ExportAlarmsToCsv(alarms, outputPath);

            MessageBox.Show($"Exported {alarms.Count} alarms to:\n{outputPath}", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
