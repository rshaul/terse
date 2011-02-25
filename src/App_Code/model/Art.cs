using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Drawing;

namespace Terse
{
	public class Art
	{
		Art() { }

		public static bool TryCreate(TagLib.IPicture pic, out Art art) {
			string type = ParseMimeType(pic.MimeType);
			if (type != null) {
				art = new Art();
				art.Type = type;
				
				art.Stream = new MemoryStream(pic.Data.Data);
				try {
					art.Image = Image.FromStream(art.Stream);
					return true;
				} catch (ArgumentException) {
					// TODO: Alert user
				}
			}
			art = null;
			return false;
		}

		static string ParseMimeType(string mimetype) {
			switch (mimetype) {
				case "image/jpeg":
				case "image/jpg":
					return "jpg";
				case "image/png":
					return "png";
				case "image/bmp":
					return "bmp";
				case "image/gif":
					return "gif";
			}
			return null;
		}
		
		MemoryStream Stream { get; set; } // Has to stay open for Image.Save() for some reason
		Image Image { get; set; }
		string Type { get; set; }

		public void Save(string path, string name) {
			path = path.TrimEnd('\\') + '\\';
			Image.Save("D:\\Devel\\terse\\src\\images\\artists\\" + name + "." + Type);
		}
	}
}