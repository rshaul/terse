using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Terse;

public partial class ajax_log : BaseAjax
{
	protected override IJsonToken AjaxResponse() {
		JsonArray array = new JsonArray();
		foreach (Log.Item item in Log.Items) {
			array.Add(item.ToJson());
		}
		return array;
	}
}