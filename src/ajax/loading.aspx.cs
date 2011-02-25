using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Terse;

public partial class ajax_loading : LibraryAjax
{
	protected override string LibraryResponse(Library collection) {
		return "false";
	}
	protected override string LibraryIsLoadingResponse() {
		return "true";
	}
}