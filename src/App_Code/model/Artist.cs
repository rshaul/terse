using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;

namespace Terse
{
	public class Artist : IComparable<Artist>
	{
		public Artist(Song file) {
			Name = file.Artist;
			Art = file.Art;
		}

		public string Name { get; set; }
		public Art Art { get; set; }

		public override bool Equals(object obj) {
			Artist other = obj as Artist;
			if (other != null) {
				return Name == other.Name;
			}
			return false;
		}

		public override int GetHashCode() {
			return Name.GetHashCode();
		}

		public int CompareTo(Artist other) {
			return Name.CompareTo(other.Name);
		}
	}
}