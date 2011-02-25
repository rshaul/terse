$(function () {
	CheckLoading();

	ResizePlaylist();
	$(window).resize(ResizePlaylist);
});

var resizeOffset = 0;
function ResizePlaylist() {
	if (resizeOffset == 0) {
		resizeOffset += $("#playing").height();
		resizeOffset += $("#nav").height();
	}
	var height = $(window).height() - resizeOffset;
	$("#playlist").height(height);
}

function CheckLoading() {
	$.get("/ajax/loading.aspx", function (data) {
		if (data == "false") {
			$("#loading").hide();
			Startup();
		} else {
			$("#loading").show();
			setTimeout(CheckWorking, 1000);
		}
	});
}

function Startup() {
	RefreshPlaylist();
}

function RefreshPlaylist() {
	$.getJSON("/ajax/songs.aspx", function (songs) {
		$("#playlist").html(GetPlaylistHtml(songs));
	}).error(function (obj, status) {
		alert("Could not get files: " + status);
	});
}

function GetPlaylistHtml(songs) {
	var items = [];
	for (var i = 0; i < songs.length; i++) {
		items.push('<a href="#" class="file"><span class="path">'
			+ songs[i].path + '</span><span class="duration">'
			+ songs[i].duration + '</span></div>');
	}
	return items.join('\n');
}