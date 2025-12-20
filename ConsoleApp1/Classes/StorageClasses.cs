using System;

namespace ConsoleApp1.Classes
{
    public class AlarmEntry
    {
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string LocalizedMessage { get; set; } = string.Empty;
        public bool Enabled { get; set; }
        public bool AutoAcknowledge { get; set; }
        public bool AutoConfirm { get; set; }
        public string InputValue { get; set; } = string.Empty;
        public int? Severity { get; set; }
        public int? MaxTimeShelved { get; set; }
        public int? PresetTimeShelved { get; set; }
        public string NormalStateValue { get; set; } = string.Empty;
    }


    public class TagEntry
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string PLCStyle { get; set; } = string.Empty;
        public string[] Messages { get; set; } = Array.Empty<string>();
        public string[] LocalizedMessages { get; set; } = Array.Empty<string>()  ;
    }

    public class AlarmMessages
    {
        public string Type { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string[] Messages { get; set; } = Array.Empty<string>();
        public string[] LocalizedMessages { get; set; } = Array.Empty<string>();
    }
}
