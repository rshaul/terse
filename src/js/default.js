$(function () {
	WaitForBackend();

	ResizeTabs();
	$(window).resize(ResizeTabs);

	ShowTab(window.location.hash.substring(1)); // Remove #

	$("#artists-link").click(function () {
		ShowTab('artists');
	});
	$("#songs-link").click(function () {
		ShowTab('songs');
	});
	$("#search-link").click(function () {
		ShowTab('search');
	});

	$("#player").bind('ended', PlayNext);

	$("#query").keydown(RunSearch);
});

var searchCounter = 0;
function RunSearch(obj) {
	searchCounter++;
	setTimeout(RunSearchDelayed, 800, obj, searchCounter);
}
function RunSearchDelayed(obj, localCounter) {
	if (localCounter == searchCounter) {
		var q = $("#query").val();
		if (q.length > 1) {
			ShowTab("search");
			LoadTab("search", "/ajax/songs.aspx?q=" + q, SetSongsHtml);
		}
	}
}

var resizeOffset = 0;
function ResizeTabs() {
	if (resizeOffset == 0) {
		resizeOffset += $("#player-container").outerHeight(true);
		resizeOffset += $("#nav").outerHeight(true);
	}
	var height = $(window).height() - resizeOffset;
	$("#tabs").height(height);
}

function ShowLoading() {
	$('#loading').show();
}
function HideLoading() {
	$("#loading").hide();
}

function WaitForBackend() {
	ShowLoading();
	$.get("/ajax/loading.aspx", function (loading) {
		if (loading == "true") {
			setTimeout(WaitForBackend, 1000);
		} else {
			Startup();
			HideLoading();
		}
	});
}

function Startup() {
	LoadTab("artists", "/ajax/artists.aspx", SetArtistsHtml);
	LoadTab("songs", "/ajax/songs.aspx", SetSongsHtml);
}

var currentTab;
function ShowTab(tab) {
	$(".tab-main").css("z-index", "0");
	$(".tab-link").removeClass('selected');
	if (!tab) tab = "songs";
	$("#" + tab + "-tab").css("z-index", "1");
	$("#" + tab + "-link").addClass('selected');
	currentTab = tab;
}

function LoadTab(tab, url, SetHtmlFunc) {
	$.getJSON(url, function (json) {
		var element = $("#" + tab + "-tab");
		element.html("");
		SetHtmlFunc(json, element);
	}).error(function (obj, status) {
		alert("Error getting tab: " + status);
	});
}

function Play(id) {
	PlayOnTab(currentTab, id);
}

var currentPlayingTab;
function PlayOnTab(tab, id) {
	$("#player").attr("src", "/play.aspx?id=" + id);
	$(".song").removeClass("playing");
	$("#" + tab + "-tab .song[data-id=" + id + "]").addClass("playing");
	GetSongInfo(id);
	currentPlayingTab = tab;
}

function PlayNext() {
	var id = $(".playing").next().data("id");
	PlayOnTab(currentPlayingTab, id);
}

function GetSongInfo(id) {
	$.getJSON("/ajax/song.aspx?id=" + id, SetPlayerText);
}

function SetPlayerText(song) {
	var titleTempalte = $.template(
			'<span id="artist">${artist}</span>'
			+ '<span id="album">${album}</span>'
			+ '<span id="title">${title}</span>');

	var pathTemplate = $.template(
		'<span id="path">${path}</span>');

	var element = $("#player-text");

	element.html("");
	if (song.title) {
		element.append(titleTempalte, song);
	} else {
		element.append(pathTemplate, song);
	}

	SetTitle(song);
}


var originalTitle;
function SetTitle(song) {
	var text;
	if (song.title) {
		text = song.artist + " - " + song.title;
	} else {
		text = song.path;
	}

	if (!originalTitle) originalTitle = document.title;
	document.title = text + " " + originalTitle;
}

function SetArtistsHtml(artists, element) {
	var items = [];
	for (var i = 0; i < artists.length; i++) {
		items.push('<a href="#" class="artist"><span class="name">'
			+ artists[i].name + '</span></a>');
	}
	return items.join('\n');
}

function SetSongsHtml(songs, element) {
	var pathTemplate = $.template(
		'<div class="song" data-id="${id}" onclick="Play(\'${id}\')">'
		+ '<span class="path">${path}</span>'
		+ '<span class="duration">${duration}</span></div>');

	var titleTemplate = $.template(
		'<div class="song" data-id="${id}" onclick="Play(\'${id}\')">'
		+ '<span class="artist">${artist}</span>'
		+ '<span class="album">${album}</span>'
		+ '<span class="title">${title}</span>'
		+ '<span class="duration">${duration}</span></div>');

	for (var i = 0; i < songs.length; i++) {
		if (songs[i].title) {
			element.append(titleTemplate, songs[i]);
		} else {
			element.append(pathTemplate, songs[i]);
		}
	}
}
