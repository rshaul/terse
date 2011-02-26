using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Terse;
using Terse.Json;

public partial class ajax_songs : LibraryAjax
{
	string FormatPath(string input) {
		foreach (string path in LibraryManager.CollectionPaths) {
			string p = path.TrimEnd('\\') + '\\';
			if (input.StartsWith(p)) {
				input = input.Remove(0, p.Length);
				break;
			}
		}
		return input;
	}

	public JsonDictionary SongToJson(Song song) {
		JsonDictionary dict = new JsonDictionary();
		dict.Add("path", FormatPath(song.Path));
		dict.Add("duration", song.FormatDuration());
		dict.Add("id", song.Id.ToString());
		return dict;
	}

	protected override string LibraryResponse(Library library) {
		JsonArray array = new JsonArray();
		foreach (Song song in library.GetSongs()) {
			array.Add(SongToJson(song));
		}
		return array.ToJson();
	}
}