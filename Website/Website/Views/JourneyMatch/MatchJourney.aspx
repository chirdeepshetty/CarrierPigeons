<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<DomainModel.Journey>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	MatchJourney
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>MatchJourney</h2>
<%
    int count = 1;
    string checkbox = "";
    string from = "";
    string to = "";
    foreach (var match in (IEnumerable) ViewData["MatchList"])
    {
        
    checkbox = "<input type='checkbox' name='box'"+count.ToString()+">";
    from = match.Journey.Origin;
    to = match.Journey.Destination;
        string html = "<p>" + checkbox + "&nbsp;&nbsp" + from + "&nbsp;&nbsp" + to + "</p>";
        %>       
        <%=html %>
        
        <%
            
    } %>
    
</asp:Content>
