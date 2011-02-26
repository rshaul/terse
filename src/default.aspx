<%@ Page Language="C#" CodeFile="default.aspx.cs" Inherits="_default" %>
<asp:Content ID="head" ContentPlaceHolderID="head" Runat="Server">
	<link href="/css/default.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="body" ContentPlaceHolderID="body" Runat="Server">

	<div id="header">
		<div id="player-container">
			<div id="player-text"></div>
			<audio id="player" controls="controls" autoplay="autoplay"></audio>
		</div>

		<div id="nav">
			<ul id="tab-links">
				<li><div id="artists-link" class="tab-link">Artist</div></li>
				<li><div id="songs-link" class="tab-link">Song</div></li>
				<li><div id="search-link" class="tab-link">Search</div></li>
			</ul>

			<div id="search">
				<input type="text" id="query" />
			</div>
		</div>
	</div>

	<div id="tabs">
		<div class="tab-main" id="loading-tab">
			<span><img src="/images/loading.gif" /></span>
		</div>
		<div class="tab-main" id="artists-tab"></div>
		<div class="tab-main" id="songs-tab"></div>
		<div class="tab-main" id="search-tab"></div>
		<div class="tab-main" id="log-tab"></div>
	</div>

	<div id="footer">
		<div id="log-link">Log</div>
	</div>

	<script src="/js/jquery.template.js" type="text/javascript"></script>
	<script src="/js/default.js" type="text/javascript"></script>
</asp:Content>