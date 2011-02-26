using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Terse;

public partial class ajax_search_index : LibraryAjax
{
	JsonArray GetJson(IEnumerable<Song> songs) {
		JsonArray array = new JsonArray();
		foreach (Song song in songs) {
			array.Add(new JsonNumber(song.Id));
		}
		return array;
	}

	JsonDictionary GetJson(string token, List<Song> songs) {
		JsonDictionary dict = new JsonDictionary();
		dict.Add(token, GetJson(songs));
		return dict;
	}

	protected override IJsonToken LibraryResponse(Library library) {
		JsonArray array = new JsonArray();
		SearchIndex index = new SearchIndex(library.GetSongs());
		foreach (KeyValuePair<string, List<Song>> kvp in index) {
			array.Add(GetJson(kvp.Key, kvp.Value));
		}
		return array;
	}
}
