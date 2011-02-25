using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Drawing;

namespace Terse
{
	public class Song : IComparable<Song>
	{
		public Song(TagLib.File file) {
			Path = GetPath(file);
			Artist = GetArtist(file);
			Art = GetArt(file);
			Duration = file.Properties.Duration;
		}

		public Art GetArt(TagLib.File file) {
			TagLib.IPicture[] pics = file.Tag.Pictures;
			if (pics != null && pics.Length > 0) {
				Art art;
				if (Art.TryCreate(pics[0], out art)) {
					return art;
				}
			}
			return null;
		}

		static string GetArtist(TagLib.File file) {
			string artist = file.Tag.FirstAlbumArtist;
			if (string.IsNullOrEmpty(artist)) {
				artist = file.Tag.FirstPerformer;
			}
			if (string.IsNullOrEmpty(artist)) {
				artist = "Unknown Artist";
			}
			return artist;
		}

		static string GetPath(TagLib.File file) {
			string output = file.Name;
			foreach (string path in LibraryManager.CollectionPaths) {
				if (output.StartsWith(path)) {
					output.Remove(0, path.Length);
					break;
				}
			}
			return output;
		}

		public string Artist { get; private set; }
		public string Path { get; private set; }
		public Art Art { get; private set; }
		public TimeSpan Duration { get; private set; }

		public string FormatDuration() {
			return Duration.Minutes + ":" + Duration.Seconds.ToString().PadLeft(2, '0');
		}

		public override bool Equals(object obj) {
			Song other = obj as Song;
			if (other != null) {
				return Path == other.Path;
			}
			return false;
		}

		public override int GetHashCode() {
			return Path.GetHashCode();
		}

		public int CompareTo(Song other) {
			return Path.CompareTo(other.Path);
		}
	}
}