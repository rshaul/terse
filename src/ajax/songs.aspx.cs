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
	bool IndexChar(char c) {
		return ((c >= 'a' && c <= 'z')
				|| (c >= 'A' && c <= 'Z')
				|| (c >= '0' && c <= '9')
				|| c == '\'');
	}

	IEnumerable<string> GetTokens(string s) {
		List<string> tokens = new List<string>();

		if (s != null) {
			s = s.ToLower();
			StringBuilder sb = new StringBuilder();
			foreach (char c in s) {
				if (IndexChar(c)) {
					sb.Append(c);
				} else if (sb.Length > 0) {
					if (sb.Length > 1) {
						tokens.Add(sb.ToString());
					}
					sb.Clear();
				}
			}

			if (sb.Length > 0) {
				tokens.Add(sb.ToString());
			}
		}

		return tokens;
	}

	bool IsMatch(string q, IEnumerable<string> tokens) {
		foreach (string token in tokens) {
			if (token.StartsWith(q, StringComparison.CurrentCultureIgnoreCase)) {
				return true;
			}
		}
		return false;
	}

	bool SongIsMatch(string[] qs, Song song) {
		List<string> tokens = new List<string>();
		tokens.AddRange(GetTokens(song.Artist));
		tokens.AddRange(GetTokens(song.Album));
		tokens.AddRange(GetTokens(song.Title));
		tokens.AddRange(GetTokens(song.FormatPath()));

		foreach (string q in qs) {
			if (!IsMatch(q, tokens)) {
				return false;
			}
		}
		return true;
	}

	IEnumerable<Song> GetSongs(Library library) {
		string q = Request.QueryString["q"];
		if (string.IsNullOrEmpty(q)) {
			return library.GetSongs();
		} else {
			string[] qs = q.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
			List<Song> output = new List<Song>();
			foreach (Song song in library.GetSongs()) {
				if (SongIsMatch(qs, song)) {
					output.Add(song);
				}
			}
			return output;
		}
	}

	protected override string LibraryResponse(Library library) {
		JsonArray array = new JsonArray();
		foreach (Song song in GetSongs(library)) {
			array.Add(song.ToJson());
		}
		return array.ToString();
	}
}