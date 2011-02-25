using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace Terse
{
	class FilePathComparer : IComparer<File>
	{
		public int Compare(File x, File y) {
			return x.Path.CompareTo(y.Path);
		}
	}

	public class Collection
	{
		SortedSet<File> files;

		void CheckArt(Artist a1, Artist a2) {
			if (a1.Art == null && a2.Art != null) {
				a1.Art = a2.Art;
			}
		}

		public IEnumerable<Artist> GetArtists() {
			List<Artist> artists = new List<Artist>();
			foreach (File file in files) {
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

		public IEnumerable<File> Files {
			get { return files; }
		}

		public Collection(string path) {
			FileInfo[] infos = GetFiles(new DirectoryInfo(path));
			files = new SortedSet<File>();
			for (int i = 0; i < infos.Length; i++) {
				try {
					files.Add(new File(infos[i]));
				} catch (CorruptFileException) {
					// TODO: Let user know somewhere
				}
			}
		}

		static T[] Merge<T>(T[] a1, T[] a2) {
			T[] output = new T[a1.Length + a2.Length];
			a1.CopyTo(output, 0);
			a2.CopyTo(output, a1.Length);
			return output;
		}

		FileInfo[] GetFiles(DirectoryInfo dir) {
			DirectoryInfo[] children = dir.GetDirectories();
			FileInfo[] files = new FileInfo[0];
			foreach (DirectoryInfo child in children) {
				files = Merge(files, GetFiles(child));
			}
			return Merge(files, dir.GetFiles("*.mp3"));
		}
	}
}