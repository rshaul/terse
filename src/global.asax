<%@ Application Language="C#" %>
<%@ Import Namespace="Terse" %>
<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
		Log.Clear();
		LibraryManager.Init();
	}

    
    void Application_End(object sender, EventArgs e) 
    {
		LibraryManager.Cleanup();
    }
    
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }
       
</script>