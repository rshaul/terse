using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace Terse
{
	class FilePathComparer : IComparer<Song>
	{
		public int Compare(Song x, Song y) {
			return x.Path.CompareTo(y.Path);
		}
	}

	public class Library
	{
		IEnumerable<Song> files;

		public Library(IEnumerable<Song> files) {
			this.files = files;
		}

		void CheckArt(Artist a1, Artist a2) {
			if (a1.Art == null && a2.Art != null) {
				a1.Art = a2.Art;
			}
		}

		public IEnumerable<Artist> GetArtists() {
			List<Artist> artists = new List<Artist>();
			foreach (Song file in files) {
				Artist artist = new Artist(file);
				int pos = artists.IndexOf(artist);
				if (pos >= 0) {
					CheckArt(artists[pos], artist);
				} else {
					artists.Add(artist);
				}
			}
			artists.Sort();
			return artists;
		}

		public IEnumerable<Song> GetSongs() {
			return files;
		}
	}
}