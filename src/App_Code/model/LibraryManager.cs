using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Threading;

namespace Terse
{
	public static class LibraryManager
	{
		public static string[] CollectionPaths {
			get { return ConfigurationManager.AppSettings["dir"].Split(','); }
		}


		/*
		 * Get all directories specified in AppSettings that
		 * a) exist
		 * and b) we have read permissions
		 */
		static List<DirectoryInfo> GetDirectoryInfos() {
			string[] paths = ConfigurationManager.AppSettings["dir"].Split(',');
			List<DirectoryInfo> list = new List<DirectoryInfo>();
			foreach (string path in paths) {
				DirectoryInfo info = new DirectoryInfo(path);
				if (info.Exists) {
					try {
						info.GetFiles();
						list.Add(info);
					} catch (UnauthorizedAccessException) {
						// TODO: Alert user
					}
				} else {
					// TODO: Alert directory does not exist
				}
			}
			return list;
		}

		static void AddSongs(List<Song> songs, IEnumerable<DirectoryInfo> dirs) {
			foreach (DirectoryInfo dir in dirs) {
				AddSongs(songs, dir);
			}
		}

		// Get all valid songs
		static void AddSongs(List<Song> songs, DirectoryInfo dir) {
			DirectoryInfo[] children = dir.GetDirectories();
			foreach (DirectoryInfo child in children) {
				AddSongs(songs, child);
			}
			
			FileInfo[] infos = dir.GetFiles();
			foreach (FileInfo info in infos) {
				try {
					TagLib.File file = TagLib.File.Create(info.FullName);
					songs.Add(new Song(file));
				} catch (TagLib.UnsupportedFormatException ex) {
					Log.AddInfo("Unsupported file: " + ex.Message);
					// Expected sometimes, since we are pulling all files
				} catch (TagLib.CorruptFileException ex) {
					Log.AddWarning("Corrupt file: " + ex.Message);
					// TODO: Let user know somewhere
				}
			}
		}


		static void BuildLibrary() {
			List<DirectoryInfo> dirs = GetDirectoryInfos();
			
			List<Song> songs = new List<Song>();
			AddSongs(songs, dirs);
			songs.Sort();
			
			CacheManager.Remove("library");
			CacheManager.Add("library", new Library(songs));
		}

		static Thread libraryBuilder;
		public static void Init() {
			if (libraryBuilder == null) {
				libraryBuilder = new Thread(new ThreadStart(BuildLibrary));
				libraryBuilder.Start();
			}
		}

		public static void Cleanup() {
			CacheManager.Remove("library");
			if (libraryBuilder.IsAlive) {
				libraryBuilder.Abort();
			}
		}

		public static bool TryGetLibrary(out Library library) {
			return CacheManager.TryGet("library", out library);
		}
	}
}