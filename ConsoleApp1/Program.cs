// See https://aka.ms/new-console-template for more information
using ConsoleApp1.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;


var defaultFilePath = "C:\\Users\\angad\\OneDrive\\Documents\\FTOPtix Alarm Export\\16797_Equipment List V1.xlsx";
Console.Write($"Enter the Excel file path (press Enter to use default: {defaultFilePath}): ");
var inputPath = Console.ReadLine();
var FilePath = string.IsNullOrWhiteSpace(inputPath) ? defaultFilePath : inputPath.Trim();
if (!File.Exists(FilePath))
{
    Console.WriteLine($"File not found: {FilePath}");
    return;
}

var Reader = new ExcelReader();
var dataSet = Reader.ReadExcelFile(FilePath);
var Cons = new Constants();
List<TagEntry> tags = new List<TagEntry>();
List<AlarmEntry> alarms = new List<AlarmEntry>();
foreach(DataRow row in dataSet.Tables[3].Rows)
{
    //Console.WriteLine($"{row[0]} - {row[7]}");
    if (!string.IsNullOrEmpty(row[9].ToString())){
        tags.Add( new TagEntry
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
        if (msg.Contains("Manual")||msg.Contains("Mute") )
        {
            name = u.Name+"_MAN"+j.ToString();
            j++;
        }
        else
        {
            name = u.Name+"_ALM"+i.ToString();
            i++;
        }
        alarms.Add( new AlarmEntry
        {
            Name = name,
            Type = "OffNormalAlarmController",
            Path = "Alarms", // Placeholder for path
            Message = msg,
            LocalizedMessage = msg, // Placeholder for localized message
            Enabled = "TRUE",
            AutoAcknowledge = "FALSE",
            AutoConfirm = "TRUE",
            InputValue = "",
            Severity = "1",
            MaxTimeShelved = "0.00:00:00.000",
            PresetTimeShelved = "0.00:00:00.000",
            NormalStateValue = "0"
        });
    }


}
Console.WriteLine("parsed tags: " + tags.Count );

// Export alarms to CSV
var outputPath = Path.Combine(Directory.GetCurrentDirectory(), "alarms.csv");
CsvExporter.ExportAlarmsToCsv(alarms, outputPath);
Console.WriteLine($"Exported {alarms.Count} alarms to: {outputPath}");
