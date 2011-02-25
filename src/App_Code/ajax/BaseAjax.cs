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
			Response.Write(AjaxResponse());
			Response.End();
		}

		protected abstract string AjaxResponse();
	}
}