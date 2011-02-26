$(function () {
	ShowLoading();
	WaitForLoading();
	LoadTab("#artists-tab", "/ajax/artists.aspx", GetArtistsHtml);
	LoadTab("#songs-tab", "/ajax/songs.aspx", GetSongsHtml);
	HideLoading();

	ResizeMain();
	$(window).resize(ResizeMain);
	
	ShowTab(window.location.hash);

	$("#artists-link").click(ShowArtists);
	$("#songs-link").click(ShowSongs);
});

var resizeOffset = 0;
function ResizeMain() {
	if (resizeOffset == 0) {
		resizeOffset += $("#playing").height();
		resizeOffset += $("#nav").height();
	}
	var height = $(window).height() - resizeOffset;
	$(".tab-main").height(height);
	$("#loading").height(height);
}

function ShowLoading() {
	$('#loading').show();
}
function HideLoading() {
	$("#loading").hide();
}

function WaitForLoading() {
	$.get("/ajax/loading.aspx", function (loading) {
		if (loading == "true") {
			setTimeout(WaitForLoading, 1000);
		}
	});
}

function ShowTab(tab) {
	$(".tab-main").css("z-index", "0");
	if (tab == "artists") {
		$("#artists-tab").css("z-index", "1");
	} else {
		$("#songs-tab").css("z-index", "1");
	}
}

function LoadTab(tab, url, GetHtmlFunc) {
	$.getJSON(url, function(json) {
		$(tab).html(GetHtmlFunc(json));
	}).error(function (obj, status) {
		alert("Error getting tab: " + status);
	});
}

function ShowArtists() {
	ShowTab('artists');
}

function ShowSongs() {
	ShowTab('songs');
}

function Play(id) {
	$("#player").attr("src", "/play.aspx?id=" + id);
	$("#player").attr("autoplay", "autoplay");
}

function GetArtistsHtml(artists) {
	var items = [];
	for (var i = 0; i < artists.length; i++) {
		items.push('<a href="#" class="artist"><span class="name">'
			+ artists[i].name + '</span></a>');
	}
	return items.join('\n');
}

function GetSongsHtml(songs) {
	var items = [];
	
	for (var i = 0; i < songs.length; i++) {
		items.push('<a href="javascript:void(0)" onclick="Play(\''
			+ songs[i].id + '\')"  class="song"><span class="path">'
			+ songs[i].path + '</span><span class="duration">'
			+ songs[i].duration + '</span></a>');
	}
	return items.join('\n');
}