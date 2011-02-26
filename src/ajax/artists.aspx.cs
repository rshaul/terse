using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Terse;
using Terse.Json;

public partial class ajax_artists : LibraryAjax
{
	string Clean(string n) {
		string o = "";
		foreach (char c in n) {
			if ((c >= '0' && c <= '9')
				|| (c >= 'a' && c <= 'z')
				|| (c >= 'A' && c <= 'Z')) {
				o += c;
			}
		}

		// TOMORROW PUT THE ALBUM ART GENERATING CODE SOMEWHERE
		/*
		foreach (Artist artist in library.GetArtists()) {
			if (artist.Art != null) {
				artist.Art.Save(Environment.CurrentDirectory + "\\images\\artists\\", Clean(artist.Name));
			}
		}
		return null;
		*/
		return o;
	}

	protected override string LibraryResponse(Library library) {
		JsonArray artists = new JsonArray();
		foreach (Artist artist in library.GetArtists()) {
			artists.Add(artist.ToJson());
		}
		return artists.ToString();
	}
}