using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Configuration;

namespace Terse
{
	public static class CollectionManager
	{
		public static string CollectionPath {
			get { return ConfigurationManager.AppSettings["dir"]; }
		}

		public static void Init() {
			CacheManager.Remove("collection");
			Collection c = new Collection(CollectionPath);
			CacheManager.Add("collection", c);
		}

		public static bool TryGetCollection(out Collection collection) {
			return CacheManager.TryGet("collection", out collection);
		}
	}
}