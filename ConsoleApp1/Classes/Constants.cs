
using System;

public class Constants
{
    public string[] MiscInputAlarms = 
    {
        "[401] Alarm",
        "[402] Evaluation Error. Contact 1 or 2 failed to switch or switched too late",
        "[403] Error in the test pulse wiring.",
        "[404] Channel 1 Input Test Pulse Error",
        "[405] Channel 2 Input Test Pulse Error",
        "[406] Triggered while machine Running",
        "[410] Manual ON Enabled",
        "[411] Manual OFF Enabled",
        "[412] Alarm Mute Active"
    };
    public string[] MiscOutputCylinderAlarms= 
    {
        "[101] Reed Alarm",
        "[102] Extend Alarm",
        "[103] Retract Alarm",
        "[104] Reed A Alarm",
        "[105] Reed B Alarm",
        "[106] Reed C Alarm",
        "[107] Reed D Alarm",
        "[108] Extend Reed A Alarm",
        "[109] Extend Reed B Alarm",
        "[110] Extend Reed C Alarm",
        "[111] Extend Reed D Alarm",
        "[112] Retract Reed A Alarm",
        "[113] Retract Reed B Alarm",
        "[114] Retract Reed C Alarm",
        "[115] Retract Reed D Alarm",
        "[130] Manual Extend Enabled",
        "[131] Manual Retract Enabled",
        "[132] Manual OFF Enabled",
        "[133] Alarm Mute Active",
        "[134] Cylinder Cycling Enabled"
    };

    public string[] MotorAlarms = 
    {
        "[601] Forward Feedback OFF Alarm",
        "[602] Reverse Feedback OFF Alarm",
        "[603] Feedback ON Alarm",
        "[604] External Feedback Alarm",
        "[605] Drive Fault",
        "[610] Manual ON Enabled",
        "[611] Manual Reverse Enabled",
        "[612] Manual OFF Enabled",
        "[613] Alarm Mute Active"
    };
}