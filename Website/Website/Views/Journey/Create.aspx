<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Website.Models.JourneyModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>
        Create a Journey</h2>
    <div>
        <%= Html.ValidationSummary("Please correct the following to continue...") %>
        <% if (ViewData["Message"] != null)
           { %>
        <%= ViewData["Message"]%><br />
        <%= Html.ActionLink("Go back to homepage...", "Index", "Home") %>
        <% }
           else
           {%>
        <% using (Html.BeginForm())
           { %>
        <p>
            Origin:
            <%= Html.TextBox("OriginPlace")%></p>
            <p>
            Departure Date:
            <%= Html.TextBox("OriginDate")%></p>
        <p>
            Destination:
            <%= Html.TextBox("DestinationPlace")%></p>
        <p>
            Arrival Date:
            <%= Html.TextBox("DestinationDate")%></p>        
        <input type="submit" value="Create Journey" />
        <% }
           } %>
    </div>

</asp:Content>
