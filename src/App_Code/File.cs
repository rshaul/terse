using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Drawing;

namespace Terse
{
	public class CorruptFileException : Exception
	{
		public CorruptFileException() { }
		public CorruptFileException(string message) : base(message) { }
		public CorruptFileException(string message, Exception inner) : base(message, inner) { }

		public CorruptFileException(TagLib.CorruptFileException ex)
			: base(ex.Message) {
		}
	}

	public class File : IComparable<File>
	{
		TagLib.File file;
		public File(FileInfo file) {
			try {
				this.file = TagLib.File.Create(file.FullName);
			} catch (TagLib.CorruptFileException ex) {
				throw new CorruptFileException(ex);
			}
		}

		T GetFirst<T>(T[] a) {
			if (a != null && a.Length > 0) {
				return a[0];
			}
			return default(T);
		}

		TagLib.IPicture Pic {
			get { return GetFirst(file.Tag.Pictures); }
		}

		public Art Art {
			get {
				TagLib.IPicture[] pics = file.Tag.Pictures;
				if (pics != null && pics.Length > 0) {
					Art art;
					if (Art.TryCreate(pics[0], out art)) {
						return art;
					}
				}
				return null;
			}
		}

		public string Artist {
			get {
				string artist = file.Tag.FirstAlbumArtist;
				if (string.IsNullOrEmpty(artist)) {
					artist = file.Tag.FirstPerformer;
				}
				if (string.IsNullOrEmpty(artist)) {
					artist = "Unknown Artist";
				}
				return artist;
			}
		}

		public string Path {
			get {
				return file.Name.Remove(0, CollectionManager.CollectionPath.Length + 1);
			}
		}

		public string FormatDuration() {
			return file.Properties.Duration.Minutes + ":" +
				file.Properties.Duration.Seconds.ToString().PadLeft(2, '0');
		}


		public override bool Equals(object obj) {
			File other = obj as File;
			if (other != null) {
				return Path == other.Path;
			}
			return false;
		}

		public override int GetHashCode() {
			return Path.GetHashCode();
		}

		public int CompareTo(File other) {
			return Path.CompareTo(other.Path);
		}
	}
}