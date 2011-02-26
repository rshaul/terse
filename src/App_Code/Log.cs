using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

namespace Terse
{
	public enum LogType
	{
		Info,
		Warning,
		Error
	}

	public static class Log
	{
		public class Item
		{
			public string Message;
			public LogType Type;
			public DateTime Time;
		}

		public static List<Item> Items { get; private set; }
	
		static Log() {
			Items = new List<Item>();
		}

		public static void Clear() {
			Items.Clear();
		}

		public static void AddInfo(string message) {
			Add(message, LogType.Info);
		}
		public static void AddWarning(string message) {
			Add(message, LogType.Warning);
		}
		public static void AddError(string message) {
			Add(message, LogType.Error);
		}
		static void Add(string message, LogType type) {
			Item item = new Item();
			item.Message = message;
			item.Type = type;
			item.Time = DateTime.Now;
			Items.Add(item);
		}
	}
}