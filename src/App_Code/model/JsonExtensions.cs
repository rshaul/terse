using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Terse
{
	public static class JsonExtensions
	{
		public static JsonDictionary ToJson(this Song song) {
			JsonDictionary dict = new JsonDictionary();
			if (string.IsNullOrEmpty(song.Title)) {
				dict.Add("path", song.FormatPath());
			} else {
				dict.Add("artist", song.Artist);
				dict.Add("album", song.Album);
				dict.Add("title", song.Title);
			}
			dict.Add("year", song.Year);
			dict.Add("duration", song.FormatDuration());
			dict.Add("id", song.Id.ToString());
			return dict;
		}

		public static JsonDictionary ToJson(this Artist artist) {
			JsonDictionary dict = new JsonDictionary();
			dict.Add("artist", artist.Name);
			return dict;
		}

		public static JsonDictionary ToJson(this Log.Item item) {
			JsonDictionary dict = new JsonDictionary();
			dict.Add("message", item.Message);
			dict.Add("type", item.Type.ToString());
			dict.Add("time", item.Time.ToString());
			return dict;
		}
	}
}