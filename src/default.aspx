<%@ Page Language="C#" CodeFile="default.aspx.cs" Inherits="_default" %>
<asp:Content ID="head" ContentPlaceHolderID="head" Runat="Server">
	<link href="/css/default.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="body" ContentPlaceHolderID="body" Runat="Server">

	<div id="loading">
		<span><img src="/images/loading.gif" /></span>
	</div>

	<div id="playing">
		Next Previous Start Stop
	</div>

	<div id="nav">
		<ul id="categories">
			<li><a href="#artist">Artist</a></li>
			<li><a href="#song">Song</a></li>
		</ul>

		<div id="search">
			<input type="text" id="query" />
		</div>
	</div>

	<div id="playlist"></div>
	<div id="main"></div>

	<script src="/js/default.js" type="text/javascript"></script>
</asp:Content>