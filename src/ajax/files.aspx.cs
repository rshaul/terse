using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Terse;
using Terse.Json;

public partial class ajax_files : CollectionAjax
{
	public JsonDictionary FileToJson(File file) {
		JsonDictionary dict = new JsonDictionary();
		dict.Add("path", file.Path);
		dict.Add("duration", file.FormatDuration());
		return dict;
	}

	protected override string CollectionResponse(Collection collection) {
		JsonArray array = new JsonArray();
		foreach (var file in collection.Files) {
			array.Add(FileToJson(file));
		}
		return array.ToJson();
	}
}