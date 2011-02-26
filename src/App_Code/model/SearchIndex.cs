using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace Terse
{
	public class SearchIndex : IEnumerable<KeyValuePair<string, List<Song>>>
	{
		SortedDictionary<string, List<Song>> index = new SortedDictionary<string,List<Song>>();

		public SortedDictionary<string, List<Song>> Index { get { return index; } }

		public SearchIndex(IEnumerable<Song> songs) {
			PopulateIndex(songs);
		}

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

		IEnumerable<string> GetTokens(Song song) {
			List<string> tokens = new List<string>();
			tokens.AddRange(GetTokens(song.Artist));
			tokens.AddRange(GetTokens(song.Album));
			tokens.AddRange(GetTokens(song.Title));
			return tokens;
		}

		void AddToIndex(string token, Song song) {
			if (!index.ContainsKey(token)) {
				index.Add(token, new List<Song>());
			}
			index[token].Add(song);
		}

		void PopulateIndex(IEnumerable<Song> songs) {
			foreach (Song song in songs) {
				foreach (string token in GetTokens(song)) {
					AddToIndex(token, song);
				}
			}
		}

		public IEnumerator<KeyValuePair<string, List<Song>>> GetEnumerator() {
			return index.GetEnumerator();
		}
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
			return index.GetEnumerator();
		}
	}
}