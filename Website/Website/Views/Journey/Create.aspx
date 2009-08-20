<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Website.Models.JourneyModel>" %>
<%@ Import Namespace="DomainModel"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>
        Create a Journey</h2>
    <div>
        You are logged in as <%=ViewData["EmailId"]%>
        <%= Html.ValidationSummary("Please correct the following to continue...") %>
        <% if (ViewData["Message"] != null){ %>
        <%= ViewData["Message"]%><br />
        <%= Html.ActionLink("Go back to homepage...", "Index", "Home") %>
            <%}
           else
           {%>
        <% using (Html.BeginForm())
           { %>
           <%=Html.Hidden("EmailId",ViewData["EmailId"]) %>
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
           
          <!-- List all the journeys  -->
          <% if (ViewData["MyOtherJourneyDetails"] != null)
        {%>
        <h4>Your Other Catalogued Journeys</h4>
        <table >
        <tr><th>Origin</th><th>Departure Date</th><th>Destination</th><th>Arrival Date</th>
        <%
            IList<DomainModel.Journey> otherJourneys = (IList<DomainModel.Journey>) ViewData["MyOtherJourneyDetails"];
                 
            foreach (Journey journey in otherJourneys)
            {
            %>
              <tr><td><%=journey.Origin.Place%> 
               <td> <%=journey.Origin.Date.ToShortDateString()%> 
               <td><%=journey.Destination.Place %> 
               <td><%=journey.Destination.Date.ToShortDateString()%>
               </tr>
            <%}
            }
           %>    
        </table>
    </div>

</asp:Content>
