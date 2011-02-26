using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Terse;

public partial class play : System.Web.UI.Page
{
	protected override void OnLoad(EventArgs e) {
		base.OnLoad(e);

		int id = int.Parse(Request.QueryString["id"]);

		Response.Clear();

		Library library;
		Song song;
		if (LibraryManager.TryGetLibrary(out library)
			&& library.TryGetSong(id, out song)) {
			Response.ContentType = song.MimeType;
			Response.WriteFile(song.Path);
		} else {
			Response.StatusCode = 404;
		}

		Response.End();
	}
}