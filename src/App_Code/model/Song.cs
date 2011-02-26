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
		TagLib.File file;
		public Song(TagLib.File file) {
			this.file = file;

			MimeType = file.MimeType;
			Path = file.Name;
			Artist = GetArtist();
			Duration = file.Properties.Duration;
		}

		public Art GetArt() {
			TagLib.IPicture[] pics = file.Tag.Pictures;
			if (pics != null && pics.Length > 0) {
				Art art;
				if (Art.TryCreate(pics[0], out art)) {
					return art;
				}
			}
			return null;
		}

		string GetArtist() {
			string artist = file.Tag.FirstAlbumArtist;
			if (string.IsNullOrEmpty(artist)) {
				artist = file.Tag.FirstPerformer;
			}
			if (string.IsNullOrEmpty(artist)) {
				artist = "Unknown Artist";
			}
			return artist;
		}

		public int Id { get { return GetHashCode(); } }
		public string Artist { get; private set; }
		public string Path { get; private set; }
		public string MimeType { get; private set; }
		public Byte[] Data { get; private set; }
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