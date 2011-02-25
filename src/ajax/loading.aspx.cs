using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Terse;

public partial class ajax_loading : CollectionAjax
{
	protected override string CollectionResponse(Collection collection) {
		return "false";
	}
	protected override string CollectionLoadingResponse() {
		return "true";
	}
}