<%@ Page Language="C#" CodeFile="default.aspx.cs" Inherits="_default" %>
<asp:Content ID="head" ContentPlaceHolderID="head" Runat="Server">
	<link href="/css/default.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="body" ContentPlaceHolderID="body" Runat="Server">

	<div id="playing">
		Next Previous Start Stop
		<audio id="player" controls="controls"></audio>
	</div>

	<div id="nav">
		<ul id="categories">
			<li><a id="artists-link" href="#artists">Artist</a></li>
			<li><a id="songs-link" href="#songs">Song</a></li>
		</ul>

		<div id="search">
			<input type="text" id="query" />
		</div>
	</div>
	
	<div id="loading">
		<span><img src="/images/loading.gif" /></span>
	</div>

	<div class="tab-main" id="artists-tab"></div>
	<div class="tab-main" id="songs-tab"></div>

	<script src="/js/default.js" type="text/javascript"></script>
</asp:Content>