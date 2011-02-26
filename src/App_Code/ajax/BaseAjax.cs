using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Terse
{
	public abstract class BaseAjax : System.Web.UI.Page
	{
		protected override void OnLoad(EventArgs e) {
			base.OnLoad(e);
		
			Response.Clear();

			IJsonToken response = AjaxResponse();
			if (response != null) {
				Response.Write(response.ToString());
			}

			Response.End();
		}

		protected abstract IJsonToken AjaxResponse();
	}
}