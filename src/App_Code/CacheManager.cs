using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace Terse
{
	public static class CacheManager
	{
		public static bool Exists(string key) {
			return HttpRuntime.Cache[key] != null;
		}

		public static void Remove(string key) {
			HttpRuntime.Cache.Remove(key);
		}

		public static void Add(string key, object value) {
			HttpRuntime.Cache.Add(key, value, null, DateTime.Now.AddDays(5), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
		}

		public static bool TryGet<T>(string key, out T value) {
			object cache = HttpRuntime.Cache[key];
			if (cache != null) {
				value = (T)cache;
				return true;
			}
			value = default(T);
			return false;
		}
	}
}