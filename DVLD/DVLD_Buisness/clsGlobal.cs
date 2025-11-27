using Microsoft.Win32;
using System;
using System.Diagnostics;

namespace DVLD_Buisness
{
    public class clsGlobal
    {
        public static void LogInformation(string Message)
        {
            const string SourceName = "DVLD";

            if (!EventLog.SourceExists(SourceName))
                EventLog.CreateEventSource(SourceName, "Application");

            EventLog.WriteEntry(SourceName, Message, EventLogEntryType.Error);
        }
    }
}
