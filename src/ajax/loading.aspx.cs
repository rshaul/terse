using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Terse;

public partial class ajax_loading : LibraryAjax
{
	protected override IJsonToken LibraryResponse(Library collection) {
		return new JsonBool(false);
	}
	protected override IJsonToken LibraryIsLoadingResponse() {
		return new JsonBool(true);
	}
}