using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Terse
{
	public class BasePage : System.Web.UI.Page
	{
		protected override void OnPreInit(EventArgs e) {
			base.OnPreInit(e);

			MasterPageFile = "~/site.master";
		}

		protected override void OnLoad(EventArgs e) {
			base.OnLoad(e);
		}
	}
}