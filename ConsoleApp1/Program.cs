// See https://aka.ms/new-console-template for more information
using ConsoleApp1.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;


var Reader = new ExcelReader();
var dataSet = Reader.ReadExcelFile("C:\\Users\\angad\\OneDrive\\Documents\\FTOPtix Alarm Export\\16797_Equipment List V1.xlsx");
List<TagEntry> tags = new List<TagEntry>();
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
    
}
Console.WriteLine("parsed tags: " + tags.Count );
