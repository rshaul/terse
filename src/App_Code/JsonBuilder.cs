using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace Terse.Json
{
	public interface IJsonToken
	{
		string ToJson();
	}

	public class JsonString : IJsonToken
	{
		string s;
		public JsonString(string s) {
			this.s = s;
		}

		public string ToJson() {
			return "\"" + s.Replace("\\", "\\\\").Replace("\"", "\\\"") + "\"";
		}
	}

	public class JsonBool : IJsonToken
	{
		bool b;
		public JsonBool(bool b) {
			this.b = b;
		}

		public string ToJson() {
			return (b) ? "true" : "false";
		}
	}

	public class JsonNumber : IJsonToken
	{
		string s;
		public JsonNumber(int n) {
			s = n.ToString();
		}
		public JsonNumber(float n) {
			s = n.ToString();
		}
		public JsonNumber(double n) {
			s = n.ToString();
		}

		public string ToJson() {
			return s;
		}
	}

	public class JsonDictionary : IJsonToken
	{
		List<JsonString> keys = new List<JsonString>();
		List<IJsonToken> values = new List<IJsonToken>();

		public void Add(string key, string value) {
			Add(key, new JsonString(value));
		}
		public void Add(string key, IJsonToken value) {
			keys.Add(new JsonString(key));
			values.Add(value);
		}

		public string ToJson() {
			StringBuilder sb = new StringBuilder();
			sb.Append("{\n");
			for (int i=0; i < keys.Count; i++) {
				string comma = (i == keys.Count-1) ? "" : ",";
				sb.Append(keys[i].ToJson() + ":" + values[i].ToJson() + comma + "\n");
			}
			sb.Append("}\n");
			return sb.ToString();
		}
	}

	public class JsonArray : IJsonToken
	{
		List<IJsonToken> list = new List<IJsonToken>();

		public void Add(IJsonToken value) {
			list.Add(value);
		}

		public string ToJson() {
			StringBuilder sb = new StringBuilder();
			sb.Append("[\n");

			for (int i=0; i < list.Count; i++) {
				string comma = (i == list.Count-1) ? "" : ",";
				sb.Append(list[i].ToJson() + comma + "\n");
			}
			sb.Append("]\n");
			return sb.ToString();
		}
	}
}