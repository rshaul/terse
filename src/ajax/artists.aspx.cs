using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Terse;
using Terse.Json;

public partial class ajax_artists : CollectionAjax
{
	JsonDictionary FileToJson(File file) {
		JsonDictionary dict = new JsonDictionary();
		dict.Add("artist", file.Artist);
		return dict;
	}

	bool HasArtist(List<File> files, string artist) {
		foreach (File file in files) {
			if (file.Artist == artist) {
				return true;
			}
		}
		return false;
	}

	string Clean(string n) {
		string o = "";
		foreach (char c in n) {
			if ((c >= '0' && c <= '9')
				|| (c >= 'a' && c <= 'z')
				|| (c >= 'A' && c <= 'Z')) {
				o += c;
			}
		}
		return o;
	}

	protected override string CollectionResponse(Collection collection) {
		// TOMORROW PUT THE ALBUM ART GENERATING CODE SOMEWHERE
		foreach (Artist artist in collection.GetArtists()) {
			if (artist.Art != null) {
				artist.Art.Save(Environment.CurrentDirectory + "\\images\\artists\\", Clean(artist.Name));
			}
		}
		return null;
	}
}