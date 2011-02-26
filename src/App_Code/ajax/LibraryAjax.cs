using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Terse
{
	public abstract class LibraryAjax : BaseAjax
	{
		protected override IJsonToken AjaxResponse() {
			Library library;
			if (LibraryManager.TryGetLibrary(out library)) {
				return LibraryResponse(library);
			} else {
				return LibraryIsLoadingResponse();
			}
		}

		protected abstract IJsonToken LibraryResponse(Library library);
	
		protected virtual IJsonToken LibraryIsLoadingResponse() {
			return null;
		}
	}
}