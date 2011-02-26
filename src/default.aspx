<%@ Page Language="C#" CodeFile="default.aspx.cs" Inherits="_default" %>
<asp:Content ID="head" ContentPlaceHolderID="head" Runat="Server">
	<link href="/css/default.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="body" ContentPlaceHolderID="body" Runat="Server">

	<div id="player-container">
		<div id="player-text"></div>
		<audio id="player" controls="controls" autoplay="autoplay"></audio>
	</div>

	<div id="nav">
		<ul id="tab-links">
			<li><a id="artists-link" class="tab-link" href="#artists">Artist</a></li>
			<li><a id="songs-link" class="tab-link" href="#songs">Song</a></li>
			<li><a id="search-link" class="tab-link" href="#search">Search</a></li>
		</ul>

		<div id="search">
			<input type="text" id="query" />
		</div>
	</div>

	<div id="tabs">
		<div id="loading">
			<span><img src="/images/loading.gif" /></span>
		</div>
		<div class="tab-main" id="artists-tab"></div>
		<div class="tab-main" id="songs-tab"></div>
		<div class="tab-main" id="search-tab"></div>
	</div>

	<script src="/js/jquery.template.js" type="text/javascript"></script>
	<script src="/js/default.js" type="text/javascript"></script>
</asp:Content>