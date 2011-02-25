using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Terse
{
	public abstract class LibraryAjax : BaseAjax
	{
		protected override string AjaxResponse() {
			Library library;
			if (LibraryManager.TryGetLibrary(out library)) {
				return LibraryResponse(library);
			} else {
				return LibraryIsLoadingResponse();
			}
		}

		protected abstract string LibraryResponse(Library library);
	
		protected virtual string LibraryIsLoadingResponse() {
			return null;
		}
	}
}