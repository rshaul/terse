using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Configuration;
using System.IO;

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
		
		// Get all files in the directories and subdirectories
		static List<FileInfo> GetFileInfos(IEnumerable<DirectoryInfo> directories) {
			List<FileInfo> files = new List<FileInfo>();
			foreach (DirectoryInfo dir in directories) {
				files.AddRange(GetFileInfos(dir));
			}
			return files;
		}
		static List<FileInfo> GetFileInfos(DirectoryInfo dir) {
			DirectoryInfo[] children = dir.GetDirectories();
			List<FileInfo> files = new List<FileInfo>();
			foreach (DirectoryInfo child in children) {
				files.AddRange(GetFileInfos(child));
			}
			files.AddRange(dir.GetFiles());
			return files;
		}

		// Get all valid songs
		static List<Song> GetSongs(List<FileInfo> infos) {
			List<Song> songs = new List<Song>(infos.Count);
			foreach (FileInfo info in infos) {
				try {
					TagLib.File file = TagLib.File.Create(info.FullName);
					songs.Add(new Song(file));
				} catch (TagLib.UnsupportedFormatException) {
					// Expected sometimes, since we are pulling all files
				} catch (TagLib.CorruptFileException) {
					// TODO: Let user know somewhere
				}
			}
			return songs;
		}


		static Library BuildLibrary() {
			List<DirectoryInfo> dirs = GetDirectoryInfos();
			List<FileInfo> infos = GetFileInfos(dirs);
			List<Song> songs = GetSongs(infos);
			songs.Sort();
			return new Library(songs);
		}

		public static void Init() {
			CacheManager.Remove("library");
			CacheManager.Add("library", BuildLibrary());
		}

		public static bool TryGetLibrary(out Library library) {
			return CacheManager.TryGet("library", out library);
		}
	}
}