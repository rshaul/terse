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
		IEnumerable<Song> songs;

		public Library(IEnumerable<Song> songs) {
			this.songs = songs;
		}

		void TryAddArtist(List<Artist> artists, Song song) {
			foreach (Artist artist in artists) {
				if (artist.Name == song.Artist) {
					if (artist.Art == null) {
						//artist.Art = song.GetArt();
					}
				} else {
					artists.Add(new Artist(song));
				}
			}
		}

		public IEnumerable<Artist> GetArtists() {
			List<Artist> artists = new List<Artist>();
			foreach (Song song in songs) {
				TryAddArtist(artists, song);
			}
			artists.Sort();
			return artists;
		}

		public IEnumerable<Song> GetSongs() {
			return songs;
		}

		public bool TryGetSong(int id, out Song output) {
			foreach (Song song in songs) {
				if (song.Id == id) {
					output = song;
					return true;
				}
			}
			output = null;
			return false;
		}
	}
}