using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

namespace Terse
{
	public static class Logger
	{
		const string Source = "Terse Player";
		const string Log = "Application";
	
		public static void LogInfo(string message) {
			if (!EventLog.SourceExists(Source)) {
				EventLog.CreateEventSource(Source, Log);
			}
			EventLog.WriteEntry(Source, message, EventLogEntryType.Information);
		}
	}
}