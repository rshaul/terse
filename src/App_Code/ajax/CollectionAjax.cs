using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Terse
{
	public abstract class CollectionAjax : BaseAjax
	{
		protected override string AjaxResponse() {
			Collection collection;
			if (CollectionManager.TryGetCollection(out collection)) {
				return CollectionResponse(collection);
			} else {
				return CollectionLoadingResponse();
			}
		}

		protected abstract string CollectionResponse(Collection collection);
	
		protected virtual string CollectionLoadingResponse() {
			return null;
		}
	}
}